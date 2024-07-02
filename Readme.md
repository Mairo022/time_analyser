# Time Analyser

## Overview

It is a command-line tool designed to analyze time entries stored in a text file or added interactively via the terminal, to identify the most popular timeframe(s).

## Features

- **Load from File**: Accepts a command line switch `filename {filename}` to load time entries from a text file. Each entry in the file should be in the format <start time><end time> (e.g., 09:5010:10), with time in `hh:mm` format and each entry on a separate line.

- **Add Time Entries**: Allows users to add time entries interactively via the terminal while the program is running. Entries should be provided in the same format as in the file.

- **Output Popular Timeframes**: Displays the most popular timeframe(s) along with the number of occurrences.

## Usage

### Loading from File

To load time entries from a file, use the command-line switch:

```bash
turnit.exe filename path/to/entries.txt