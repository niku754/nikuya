# NikuYa

**NikuYa** is a lightweight Windows utility written in **C# / WinForms** for blocking Yandex Browser and Yandex network addresses.

> ⚠️ NikuYa is currently an experimental project and is under active development.

## ✨ Features

- 🚫 Block Yandex Browser processes
- 🌐 Block Yandex network addresses using Windows Firewall
- 🔓 Unblock previously created NoxYan firewall rules
- 🖥️ Simple and minimalist WinForms interface
- ⚡ Automatically resolves domain IP addresses
- 🛡️ Uses Windows Firewall instead of constantly running in the background

## 🛠️ Technologies

- **C#**
- **.NET**
- **Windows Forms**
- **Windows Registry**
- **Windows Defender Firewall**
- **DNS resolution**

## ⚙️ How it works

### Browser blocking

NikuYa uses the Windows Registry `DisallowRun` policy to prevent selected browser processes from launching.

Currently, the project can block:

```text
Yandex.exe
browser.exe
