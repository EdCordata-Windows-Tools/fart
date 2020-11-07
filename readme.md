# FART - Find And Replace Text

Small windows command line tool to find and replace text in files

### Usage
`fart [options]`

### Options
* `-h`/`-help` - Display help
* `-v`/`-version` - Display version 
* `-p`/`-preview` - Display each line with line number in terminal
                    without changing the file 
* `-i`/`-input` - Input file
* `-o`/`-output` - Output file (will change input file, if not specified)
* `-f`/`-find` - Find text 
* `-r`/`-replace` - Replace text 

### Example
`fart -input "/home/user/test.txt -find "foo" -replace "bar"`
