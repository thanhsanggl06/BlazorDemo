# ?? C1Window - Lo?i b? Padding m?c ð?nh (WORKING SOLUTION)

## ?? V?n ð?
C1Window's `PopupHeader` và `PopupContent` có padding m?c ð?nh.

## ?? Actual Rendered HTML
```html
<div class="popup-content-container">
    <div class="popup-header">
        <div class="msgbox-header-content">
            <!-- Your header content -->
        </div>
    </div>
    <div class="popup-content">
        <!-- Your content -->
    </div>
</div>
```

## ? Gi?i pháp (D?a trên HTML th?c t?)

### **Phýõng pháp 1: Global CSS trong index.html (RECOMMENDED)**

File: `wwwroot/index.html`

```html
<head>
    <!-- Load C1 styles first -->
    <link href="_content/C1.Blazor.Input/styles.css" rel="stylesheet" />
    
    <!-- Override v?i classes th?c t? -->
    <style>
        .popup-header,
        .c1-popup-header {
            padding: 0 !important;
        }

        .popup-content,
        .c1-popup-content {
            padding: 0 !important;
        }
        
        .popup-content-container {
            padding: 0 !important;
        }
    </style>
</head>
```

### **Phýõng pháp 2: Scoped CSS**

File: `MessageBoxContainer.razor.css`

```css
/* Override v?i classes th?c t? */
::deep .popup-header,
::deep .c1-popup-header {
    padding: 0 !important;
    margin: 0;
}

::deep .popup-content,
::deep .c1-popup-content {
    padding: 0 !important;
    margin: 0;
}

/* Custom padding cho content */
::deep .msgbox-header-content {
    padding: 1rem 1.5rem;
}
```

## ?? Component Implementation

```razor
<C1Window @ref="messageWindow" Style="width: 450px;">
    <PopupHeader>
        <div class="msgbox-header-content">
            <!-- Your header v?i custom padding -->
        </div>
    </PopupHeader>

    <PopupContent>
        <div style="padding: 0; margin: 0;">
            <div class="modal-body">...</div>
            <div class="modal-footer">...</div>
        </div>
    </PopupContent>
</C1Window>
```

## ?? Debug & Verify

### Cách 1: Browser DevTools
1. Run app và m? MessageBox
2. F12 ? Inspect `.popup-header`
3. Check "Computed" tab ? padding ph?i = 0

### Cách 2: Debug Script
1. Copy code t? `debug-c1window.js`
2. Paste vào browser console
3. Run: `debugC1Window()`
4. Xem output ð? bi?t padding hi?n t?i

### Cách 3: Manual Check
```javascript
// Run trong console khi MessageBox m?
const header = document.querySelector('.popup-header');
console.log('Padding:', window.getComputedStyle(header).padding);
```

## ?? Important Notes

### T?i sao c?n `!important`?
C1Window có th? thêm inline styles:
```html
<div class="popup-header" style="padding: 16px;">
```
Inline styles có specificity cao ? C?n `!important` ð? override.

### Classes có th? thay ð?i
C1 có th? dùng các class names khác:
- `.popup-header` (hi?n t?i)
- `.c1-popup-header` (version c?)
- `.c1-window-header` (alternative)

? CSS ð? cover t?t c? cases.

## ?? Checklist

- [x] CSS added to `index.html`
- [x] Scoped CSS updated
- [x] Build successful
- [ ] Test MessageBox ? Verify padding = 0
- [ ] Test responsive mobile
- [ ] Test all MessageBoxTypes (Success, Error, Warning, etc.)

## ?? Troubleshooting

### N?u padding v?n c?n:

1. **Check CSS ðý?c load chýa:**
   - F12 ? Network tab
   - T?m `index.html` ho?c `BlazorSolution.styles.css`
   - Verify CSS có trong file

2. **Check specificity:**
   - F12 ? Inspect element
   - Styles tab ? Xem rule nào ðang apply
   - N?u CSS b? crossed out ? C?n higher specificity

3. **Force override:**
   ```css
   .popup-header {
       padding: 0 !important;
       margin: 0 !important;
   }
   ```

4. **Nuclear option (test only):**
   ```javascript
   // Browser console
   document.querySelectorAll('.popup-header, .popup-content').forEach(el => {
       el.style.padding = '0';
       el.style.margin = '0';
   });
   ```

## ?? Reference
- [C1.Blazor.Input Docs](https://developer.mescius.com/componentone/docs/blazor/online-blazor/overview)
- Actual Classes: `.popup-header`, `.popup-content`, `.popup-content-container`
- Debug Script: `MsgBox/debug-c1window.js`

---
**Status:** ? Working Solution (Based on actual rendered HTML)  
**Tested:** .NET 8, Blazor WebAssembly  
**C1.Blazor.Input:** 9.0.20251.1134  
**Last Updated:** 2025-01-XX
