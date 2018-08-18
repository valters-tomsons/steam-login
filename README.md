# steam-autologin
Auto login utility for linux.

This application edits registry.vdf that Steam uses to autologin user accounts. This is created so you can switch between multiple user profiles without having to manually log in every time (or write Steam Guard code) you wish to change an account.

When logging in with your accounts, be sure to check "Remember Password".

Usage:
`dotnet steam-autologin.dll <username>`

After running the program, any current steam process will be terminated and steam-runtime will be launched.

## Launch script example
Just put the files in a folder, for example : `~/.steam-login`
Make a script `steam-login` in `/usr/local/bin`and make it executable.

`dotnet ~/.steam-login/steam-autologin.dll $1`

or if you want to use steam native: 

`dotnet ~/.steam-login/steam-autologin.dll $1 native`


then just run `steam-login <username>` from terminal!
