#include <Windows.h>
#include <cstdio>
#include <vector>

static bool IsProcessElevated()
{
	SID_IDENTIFIER_AUTHORITY NtAuthority = SECURITY_NT_AUTHORITY;
	PSID SecurityIdentifier;
	if (!AllocateAndInitializeSid(&NtAuthority, 2, SECURITY_BUILTIN_DOMAIN_RID, DOMAIN_ALIAS_RID_ADMINS, 0, 0, 0, 0, 0, 0, &SecurityIdentifier))
		return 0;

	BOOL IsAdminMember;
	if (!CheckTokenMembership(NULL, SecurityIdentifier, &IsAdminMember))
		IsAdminMember = FALSE;

	FreeSid(SecurityIdentifier);
	return !!IsAdminMember;
}

class RedirectWow
{
	PVOID oldValue = NULL;
	BOOL(WINAPI* wow64DisableRedirection)(PVOID);
	BOOL(WINAPI* wow64RevertRedirection)(PVOID);

public:
	RedirectWow()
	{
		wow64DisableRedirection = (decltype(wow64DisableRedirection))GetProcAddress(GetModuleHandleW(L"kernel32"), "Wow64DisableWow64FsRedirection");
		wow64RevertRedirection = (decltype(wow64RevertRedirection))GetProcAddress(GetModuleHandleW(L"kernel32"), "Wow64RevertWow64FsRedirection");
		if (wow64DisableRedirection)
			wow64DisableRedirection(&oldValue);
	}

	RedirectWow(const RedirectWow&) = delete;
	RedirectWow(RedirectWow&&) = delete;
	RedirectWow& operator=(const RedirectWow&) = delete;

	~RedirectWow()
	{
		if (oldValue)
			wow64RevertRedirection(oldValue);
	}
};

int wmain(int argc, wchar_t* argv[])
{
	RedirectWow redirectWow;

	// Helper functions for functioning printf
	auto printf = [](const char* fmt, auto... args)
	{
		char msg[1024] = "";
		sprintf_s(msg, fmt, args...);
		DWORD w = 0;
		WriteFile(GetStdHandle(STD_OUTPUT_HANDLE), msg, strlen(msg), &w, nullptr);
	};
	auto puts = [&printf](const char* str)
	{
		printf("%s\n", str);
	};

	// Get rid of application from command line
	auto commandLine = GetCommandLineW();
	{
		if (*commandLine == L'\"')
		{
			commandLine++;
			while (*commandLine++ != L'\"');
			commandLine++;
		}
		else
		{
			while (*commandLine != L' ' && *commandLine != L'\0')
				commandLine++;
		}
		while (*commandLine == L' ')
			commandLine++;
		if (!*commandLine)
			commandLine = nullptr;
	}

	if (!IsProcessElevated())
	{
		// Restart sudo elevated
		SHELLEXECUTEINFOW ShExecInfo = { 0 };
		ShExecInfo.cbSize = sizeof(SHELLEXECUTEINFOW);
		ShExecInfo.fMask = SEE_MASK_NOCLOSEPROCESS;
		ShExecInfo.hwnd = GetConsoleWindow();
		ShExecInfo.lpVerb = L"runas";
		ShExecInfo.lpFile = argv[0];
		ShExecInfo.lpParameters = commandLine;
		ShExecInfo.lpDirectory = NULL;
		ShExecInfo.nShow = SW_HIDE;
		ShExecInfo.hInstApp = NULL;
		if (!ShellExecuteExW(&ShExecInfo))
		{
			auto lastError = GetLastError();
			if (lastError == ERROR_CANCELLED)
				puts("Elevation canceled by user");
			else
				printf("ShellExecuteExW failed (LastError: %u)\n", GetLastError());
			return EXIT_FAILURE;
		}
		WaitForSingleObject(ShExecInfo.hProcess, INFINITE);
		DWORD ExitCode = 0;
		if (!GetExitCodeProcess(ShExecInfo.hProcess, &ExitCode))
			ExitCode = 1;
		CloseHandle(ShExecInfo.hProcess);
		return ExitCode;
	}
	else
	{
		// Get rid of the hidden console and attach to the parent sudo's console
		FreeConsole();
		AttachConsole(ATTACH_PARENT_PROCESS);

		// Construct final command line for cmd
		std::vector<wchar_t> finalCommandLine;
		auto append = [&finalCommandLine](const wchar_t* str)
		{
			auto length = wcslen(str);
			for (size_t i = 0; i < length; i++)
				finalCommandLine.push_back(str[i]);
		};
		if (commandLine)
		{
			append(L"cmd.exe /C ");
			append(commandLine);
		}
		else
		{
			append(L"cmd.exe");
		}
		finalCommandLine.push_back(L'\0');

		// Create cmd instance
		STARTUPINFOW si = { sizeof(si) };
		PROCESS_INFORMATION pi = { 0 };
		if (!CreateProcessW(nullptr, finalCommandLine.data(), nullptr, nullptr, FALSE, 0, nullptr, nullptr, &si, &pi))
		{
			printf("CreateProcessW failed (LastError: %u)\n", GetLastError());
			return EXIT_FAILURE;
		}
		WaitForSingleObject(pi.hProcess, INFINITE);
		DWORD ExitCode = 0;
		if (!GetExitCodeProcess(pi.hProcess, &ExitCode))
			ExitCode = 1;
		CloseHandle(pi.hProcess);
		CloseHandle(pi.hThread);
		return ExitCode;
	}
}