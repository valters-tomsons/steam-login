# steam-autologin
Auto login utility for linux.

This application edits registry.vdf that Steam uses to autologin user accounts. This is created so you can switch between multiple user profiles without having to manually log in every time (or write Steam Guard code) you wish to change an account.

When logging in with your accounts, be sure to check "Remember Password".

Usage:
`dotnet steam-autologin.dll <username>`

After running the program, any current steam process will be terminated and steam-runtime will be launched.
