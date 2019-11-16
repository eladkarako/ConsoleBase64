
```js
/*╔════════════════════════════════════════════════════════════════════════════════════╗
  ║ writes back files' base64-equivalent, with an embedded-mimetype prefix.            ║
  ╟────────────────────────────────────────────────────────────────────────────────────╢
  ║ Note #1: mimetype matching is ext. based.                                          ║
  ╟────────────────────────────────────────────────────────────────────────────────────╢
  ║ Note #2: reading is in a binary-mode, but if you encode Unicode text-files,        ║
  ║ decoding (in the browser), will show you a binary-string representation of         ║
  ║ the data, where each character-code is less then 256),                             ║
  ║ so add ';charset=utf8;' to the prefix, and add 'charset="UTF-8"' attribute to the  ║
  ║ embedded element whenever possible.                                                ║
  ║                                                                                    ║
  ║ If you need decode the embedded data in-your-code, you should explicitly convert   ║
  ║ it from "binary-string" (UTF8) to UTF16 (JavaScript standard) using either the     ║
  ║ tweak-method of: 'decodeURIComponent(escape("....."));'                            ║
  ║ or with 'utf8ByteArrayToString' from the Google-closure - crypt library,           ║
  ║ https://github.com/google/closure-library/blob/master/closure/goog/crypt/crypt.js  ║
  ╚════════════════════════════════════════════════════════════════════════════════════╝
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ */
```
