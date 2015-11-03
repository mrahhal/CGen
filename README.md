# CGen
Hashes, encrypts, decrypts data using various kinds of algorithms

## Overview

### Random generation
- Generate cryptographically random bytes (converted to hex for easier use):
```
cgen rand [length] # Default is 32 bytes = 64 hex characters
```

- Generate a guid:
```
cgen guid
```

### Hashing
- `SHA1`
- `SHA256`
- `SHA512`
- `MD5`

```
cgen hash [sha1|sha256|sha512] [options]
```

### Symmetric encryption
- `AES`
- `Rijndael`

```
cgen [encrypt|decrypt] [aes|rijndael] [options]
```

### Asymmetric encryption
- `RSA`

```
cgen [encrypt|decrypt] rsa [options]
```

### Public/private keys generation
- `RSA`

```
cgen generate [options]
```

## Help
To list all the commands:
```
cgen --h
```

To show help for a certain command:
```
cgen [command] --h
```
