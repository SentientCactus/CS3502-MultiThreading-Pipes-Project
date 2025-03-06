# CS3502 Multi-Threaded Programming and Inter-Process Communication Project

This repository contains the files for **CS3502 Project 1**, which demonstrates:

- **Part A: Multi-Threaded Programming (Threads, Deadlock Prevention & Resolution)**
- **Part B: Inter-Process Communication (Pipes)**

# Part A - Multi-Threaded Programming

Part A demonstrates:

- **Thread creation and synchronization**.
- **Shared resource protection using locks/mutexes**.
- **Deadlock scenarios and multiple techniques to prevent or resolve deadlock**.

### Phases Overview

| Phase   | Description                                                 |
| ------- | ----------------------------------------------------------- |
| Phase 1 | Basic thread creation and joining                           |
| Phase 2 | Resource protection using `lock` (per-account mutex)        |
| Phase 3 | Intentional deadlock simulation (two threads, two accounts) |
| Phase 4 | Multiple deadlock prevention and resolution techniques      |

### How to Run

1. Open a terminal in WSL.
2. Navigate to the **PartA** folder.
3. Use the command:

```bash
 dotnet run
```

4. Follow the on-screen menu to run each phase.

---

# Part B - Inter-Process Communication (Pipes)

Part B demonstrates:

- **Inter-process communication using named pipes (FIFO)**.
- **A writer process sends messages to a named pipe.**
- **A reader process continuously reads from the named pipe.**

### How to Set Up

1. **Create the named pipe (FIFO)** in WSL (this must be done before running either process):

```bash
mkfifo /tmp/genpipe
```

2. Open **two terminals**.

### Terminal 1 - Start Reader

```bash
cd PartB/Reader
dotnet run
```

### Terminal 2 - Start Writer

```bash
cd PartB/Writer
dotnet run
```

3. Use the Writer menu to send messages.

4. Observe the messages appearing in the Reader terminal.

---

# Notes

- Both processes use the named pipe at `/tmp/genpipe`.
- If the pipe does not exist, the programs will gracefully notify you.
- You can terminate the **Reader** using `Ctrl+C`.

---

# Summary

This project covers:

- **Thread management and synchronization.**
- **Resource contention and deadlock prevention.**
- **Process communication using named pipes in WSL.**

---

# Author

Austin Darrow - CS3502 Project

---
