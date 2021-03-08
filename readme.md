# md5bytes

## help

```
Help
md5bytes <wordlist> <inputfile> <outputfile>
-c       check mode
-h       show this help
```

## check mode
```
md5bytes.exe -c 123456789
Password: 123456789
String in ASCII Bytes: 49 50 51 52 53 54 55 56 57
Bytes after MD5 Hash: 37 249 231 148 50 59 69 56 133 245 24 31 27 98 77 11
MD5 Sum: 25f9e794323b453885f5181f1b624d0b
String from ASCII Bytes after MD5 Hash: %???2;E8??↑▼←bM♂
Reverse:
Bytes from ASCII String after MD5 Hash: 37 63 63 63 50 59 69 56 63 63 24 31 27 98 77 11
MD5 Sum: 253f3f3f323b45383f3f181f1b624d0b
```

## crack
```
md5bytes.exe wordlist.txt hashes.txt out.txt
Read binary data: %???2;E8??↑▼←bM♂
found password!!!!
hash input    : %???2;E8??↑▼←bM♂
wordlist input: %???2;E8??↑▼←bM♂
Password: 123456789
finished
6 wordlist entries calculated
```
