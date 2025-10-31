# ?? C1Window Styling Guide - Lo?i b? Padding m?c ð?nh

## ?? V?n ð?

C1.Blazor.Input's `C1Window` component t? ð?ng thêm padding vào `PopupHeader` và `PopupContent`, gây ra kho?ng tr?ng không mong mu?n.

## ??? C?u trúc HTML ðý?c render b?i C1Window

```html
<div class="c1-window">
    <!-- PopupHeader renders to -->
    <div class="c1-window-header c1-popup-header" style="padding: 1rem;">
        <!-- Your content here -->
    </div>
    
    <!-- PopupContent renders to -->
    <div class="c1-window-content c1-popup-content" style="padding: 1rem;">
        <!-- Your content here -->
    </div>
</div>
```

## ? Gi?i pháp: Override CSS v?i ::deep selector

### 1?? **S? d?ng ::deep selector trong Scoped CSS**

File: `MessageBoxContainer.razor.css`

```css
/* ===== C1Window Container ===== */
::deep .c1-window {
    border-radius: 0.5rem;
    box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
}

/* ===== PopupHeader - Lo?i b? padding m?c ð?nh ===== */
::deep .c1-window .c1-window-header,
::deep .c1-window .c1-popup-header {
    padding: 0 !important; /* !important ð? override inline styles */
    margin: 0 !important;
    border-radius: 0.5rem 0.5rem 0 0;
    border-bottom: 1px solid #dee2e6;
}

/* ===== PopupContent - Lo?i b? padding m?c ð?nh ===== */
::deep .c1-window .c1-window-content,
::deep .c1-window .c1-popup-content {
    padding: 0 !important;
    margin: 0 !important;
}

/* ===== Lo?i b? padding cho các wrapper div bên trong ===== */
::deep .c1-window-content > div,
::deep .c1-popup-content > div {
    padding: 0;
    margin: 0;
}
```

### 2?? **Thêm padding tùy ch?nh vào n?i dung**

Thay v? dùng padding m?c ð?nh c?a C1, thêm wrapper div v?i padding tùy ch?nh:

```razor
<PopupHeader>
    <div class="msgbox-header-content">
        <!-- Your header content with custom padding -->
        <div class="d-flex align-items-center gap-2">
            <i class="bi bi-info-circle"></i>
            <span>Title</span>
        </div>
    </div>
</PopupHeader>

<PopupContent>
    <div style="padding: 0; margin: 0;">
        <!-- Your content with custom padding via Bootstrap classes -->
        <div class="modal-body">
            <!-- Content here -->
        </div>
        <div class="modal-footer">
            <!-- Buttons here -->
        </div>
    </div>
</PopupContent>
```

## ?? Các CSS Classes quan tr?ng c?a C1Window

| Class | Mô t? | Padding m?c ð?nh |
|-------|-------|------------------|
| `.c1-window` | Container chính | `0` |
| `.c1-window-header` | Header container (c?) | `1rem` |
| `.c1-popup-header` | Header container (m?i) | `1rem` |
| `.c1-window-content` | Content container (c?) | `1rem` |
| `.c1-popup-content` | Content container (m?i) | `1rem` |

## ?? Lýu ? quan tr?ng

### ?? T?i sao c?n `!important`?

C1Window thêm inline styles tr?c ti?p vào elements:
```html
<div class="c1-popup-header" style="padding: 1rem;">
```

Inline styles có **specificity cao hõn** CSS classes, nên c?n `!important` ð? override.

### ?? Alternative: S? d?ng Global CSS

N?u không mu?n dùng scoped CSS, thêm vào `wwwroot/index.html`:

```html
<style>
    .c1-window .c1-window-header,
    .c1-window .c1-popup-header {
        padding: 0 !important;
    }
    
    .c1-window .c1-window-content,
    .c1-window .c1-popup-content {
        padding: 0 !important;
    }
</style>
```

### ?? Alternative: S? d?ng Style Parameter

Thêm style tr?c ti?p vào C1Window:

```razor
<C1Window @ref="messageWindow"
          Style="@windowStyle"
          HeaderStyle="padding: 0 !important;"
          ContentStyle="padding: 0 !important;">
    <!-- ... -->
</C1Window>
```

**Lýu ?:** Ki?m tra documentation c?a C1.Blazor.Input ð? xem parameters `HeaderStyle` và `ContentStyle` có t?n t?i không.

## ?? Best Practices

### ? Nên làm

1. **Lo?i b? padding m?c ð?nh** b?ng CSS
2. **Thêm padding tùy ch?nh** vào wrapper div bên trong
3. **S? d?ng Bootstrap classes** cho spacing (modal-body, modal-footer)
4. **Gi? c?u trúc HTML nh?t quán** gi?a các MessageBox types

### ? Không nên

1. **Không hardcode padding** tr?c ti?p vào PopupHeader/PopupContent
2. **Không nest nhi?u wrapper divs** không c?n thi?t
3. **Không mix inline styles và CSS classes** cho cùng m?t property

## ?? Debug Tips

### Ki?m tra padding ðý?c apply t? ðâu:

1. M? Developer Tools (F12)
2. Inspect element `c1-popup-header` ho?c `c1-popup-content`
3. Xem "Computed" tab ð? th?y padding final
4. Xem "Styles" tab ð? th?y source c?a padding

### N?u padding v?n c?n:

```css
/* Th? v?i higher specificity */
::deep .c1-window > .c1-popup-header {
    padding: 0 !important;
}

::deep .c1-window > .c1-popup-content {
    padding: 0 !important;
}

/* Ho?c target t?t c? child elements */
::deep .c1-window * {
    padding: 0 !important;
}
```

**C?nh báo:** `* { padding: 0 !important; }` s? lo?i b? ALL padding, k? c? c?a n?i dung bên trong!

## ?? So sánh các phýõng pháp

| Phýõng pháp | Ýu ði?m | Nhý?c ði?m |
|-------------|---------|------------|
| **::deep + Scoped CSS** | ? Component-specific<br>? Không ?nh hý?ng global | ?? C?n `!important` |
| **Global CSS** | ? Áp d?ng toàn b? app<br>? D? debug | ? Có th? conflict v?i components khác |
| **Inline Style** | ? Highest specificity<br>? Không c?n !important | ? Khó maintain<br>? L?p code |
| **C1Window Parameters** | ? Clean, declarative | ? Ph? thu?c vào C1 API |

## ?? K?t lu?n

**Phýõng pháp ðý?c khuy?n ngh?:**

```razor
<!-- Component -->
<C1Window Style="@windowStyle">
    <PopupHeader>
        <div class="custom-header-wrapper">
            <!-- Content with custom padding -->
        </div>
    </PopupHeader>
    
    <PopupContent>
        <div class="custom-content-wrapper">
            <!-- Content with custom padding -->
        </div>
    </PopupContent>
</C1Window>
```

```css
/* Scoped CSS */
::deep .c1-popup-header,
::deep .c1-popup-content {
    padding: 0 !important;
}

.custom-header-wrapper {
    padding: 1rem 1.5rem;
}

.custom-content-wrapper {
    padding: 0; /* Bootstrap modal-body/modal-footer s? có padding riêng */
}
```

---

## ?? Resources

- [C1.Blazor.Input Documentation](https://www.grapecity.com/componentone/docs/blazor/online-blazor/overview.html)
- [CSS ::deep selector](https://developer.mozilla.org/en-US/docs/Web/CSS/::deep)
- [CSS Specificity](https://developer.mozilla.org/en-US/docs/Web/CSS/Specificity)

---

**Last Updated:** 2025
**Version:** 1.0
**C1.Blazor.Input Version:** 9.0.20251.1134
