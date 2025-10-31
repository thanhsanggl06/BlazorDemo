# ?? MessageBox Component - Pure Bootstrap Implementation

## ? Features

### ?? Design
- ? **Pure Bootstrap CSS** - No C1Window, no JavaScript dependencies
- ? **Professional UX/UI** - Smooth animations, responsive design
- ? **Accessible** - ARIA labels, keyboard navigation
- ? **Mobile-friendly** - Optimized for all screen sizes

### ?? Functionality
- ? **Multiple Types**: Info, Success, Error, Warning, Question, Prompt
- ? **Multiple Buttons**: OK, OK/Cancel, Yes/No, Yes/No/Cancel
- ? **Backdrop Options**: Show/Hide, Click to close/Lock
- ? **Async Support**: Full async/await pattern
- ? **No External JS**: Pure CSS animations

## ?? Usage

### Basic Alerts

```csharp
// Success
await MessageBox.SuccessAsync("??????????");

// Error
await MessageBox.ErrorAsync("???????????");

// Warning
await MessageBox.WarningAsync("???????");

// Info
await MessageBox.AlertAsync("???????");
```

### Confirm Dialogs

```csharp
bool confirmed = await MessageBox.ConfirmAsync(
    "?????????????",
    "??");

if (confirmed)
{
    // User clicked Yes
}
```

### Prompt Dialogs

```csharp
string name = await MessageBox.PromptAsync(
    "????????????:",
    "??",
    "??????");

if (!string.IsNullOrEmpty(name))
{
    // Use the input
}
```

### Advanced Options

```csharp
// Custom backdrop behavior
await MessageBox.ShowAsync(new MessageBoxModel
{
    Title = "????",
    Message = "?????",
    Type = MessageBoxType.Warning,
    ShowBackdrop = true,              // Show backdrop (default: true)
    CloseOnBackdropClick = false      // Lock modal (default: true)
});
```

## ??? Options

### MessageBoxModel Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Title` | string | "??" | Modal title |
| `Message` | string | "" | Message content (HTML supported) |
| `Type` | MessageBoxType | Info | Icon and color theme |
| `Buttons` | MessageBoxButtons | OK | Button configuration |
| `ShowBackdrop` | bool | true | Show dark background |
| `CloseOnBackdropClick` | bool | true | Allow close on backdrop click |
| `DefaultValue` | string | "" | Default value for Prompt |
| `OkButtonText` | string | "OK" | OK button text |
| `CancelButtonText` | string | "?????" | Cancel button text |
| `YesButtonText` | string | "??" | Yes button text |
| `NoButtonText` | string | "???" | No button text |

### MessageBoxType Enum

```csharp
public enum MessageBoxType
{
    Info,       // ?? Blue info icon
    Success,    // ?? Green check icon
    Warning,    // ?? Yellow warning icon
    Error,      // ?? Red error icon
    Question,   // ?? Blue question icon
    Prompt      // ? Gray input icon
}
```

### MessageBoxButtons Enum

```csharp
public enum MessageBoxButtons
{
    OK,              // [OK]
    OKCancel,        // [Cancel] [OK]
    YesNo,           // [No] [Yes]
    YesNoCancel      // [Cancel] [No] [Yes]
}
```

### MessageBoxResult Enum

```csharp
public enum MessageBoxResult
{
    None,
    OK,
    Cancel,
    Yes,
    No
}
```

## ?? Styling

### Bootstrap Classes Used

```css
.modal                    /* Bootstrap modal container */
.modal-backdrop           /* Dark background */
.modal-dialog             /* Dialog positioning */
.modal-content            /* Content container */
.modal-header             /* Header with title */
.modal-body               /* Body with message */
.modal-footer             /* Footer with buttons */
.btn-close                /* Close button */
```

### Custom Animations

```css
/* Fade in/out */
.modal.fade .modal-dialog {
    transform: translate(0, -50px) scale(0.95);
    opacity: 0;
    transition: transform 0.3s ease-out, opacity 0.15s linear;
}

.modal.show .modal-dialog {
    transform: none;
    opacity: 1;
}

/* Icon bounce */
@keyframes iconBounce {
    0% { transform: scale(0.3); opacity: 0; }
    50% { transform: scale(1.1); }
    100% { transform: scale(1); opacity: 1; }
}
```

### Responsive Design

```css
/* Mobile optimizations */
@media (max-width: 576px) {
    .modal-dialog {
        margin: 0.5rem;
        max-width: calc(100% - 1rem);
    }
    
    .modal-footer {
        flex-direction: column-reverse;
        gap: 0.5rem;
    }
    
    .modal-footer .btn {
        width: 100%;
    }
}
```

## ?? Architecture

### Component Structure

```
MsgBox/
??? IMessageBoxService.cs           # Service interface
??? MessageBoxService.cs            # Service implementation
??? MessageBoxModel.cs              # Model & enums
??? MessageBoxContainer.razor       # UI component (Bootstrap)
??? MessageBoxContainer.razor.css   # Styling
??? BOOTSTRAP_README.md             # This file
```

### Flow Diagram

```
User Code
    ?
IMessageBoxService.ShowAsync()
    ?
MessageBoxService (fires OnShow event)
    ?
MessageBoxContainer (renders modal)
    ?
User interacts with modal
    ?
Modal closes with result
    ?
TaskCompletionSource completes
    ?
User Code receives result
```

## ?? Best Practices

### ? DO

```csharp
// Use async/await
var result = await MessageBox.ConfirmAsync("Message");

// Check result properly
if (result)
{
    // Handle confirmation
}

// Use appropriate types
await MessageBox.ErrorAsync("Error occurred");
await MessageBox.SuccessAsync("Success!");

// Provide clear messages
await MessageBox.ConfirmAsync(
    "?????????????\n??????????",
    "????"
);
```

### ? DON'T

```csharp
// Don't forget await
MessageBox.ShowAsync(model); // ? Result ignored

// Don't use generic messages
await MessageBox.AlertAsync("Error"); // ? Not helpful

// Don't show too many dialogs
for (int i = 0; i < 100; i++)
{
    await MessageBox.AlertAsync($"Item {i}"); // ? Bad UX
}

// Don't use HTML when not needed
await MessageBox.AlertAsync("<p>Simple text</p>"); // ? Unnecessary
```

## ?? Performance

### Optimizations

- ? **No JavaScript** - Pure CSS animations
- ? **Lazy rendering** - Modal only rendered when shown
- ? **Minimal DOM** - Clean HTML structure
- ? **CSS transitions** - Hardware accelerated
- ? **No polling** - Event-driven architecture

### Memory Management

```csharp
// Component implements IDisposable
public void Dispose()
{
    MessageBoxService.OnShow -= ShowMessageBox;
    MessageBoxService.OnPrompt -= ShowPromptBox;
}
```

## ?? Customization

### Change Button Colors

```css
/* MessageBoxContainer.razor.css */
.modal-footer .btn-primary {
    background-color: #your-color;
    border-color: #your-color;
}
```

### Change Icon Size

```css
.modal-body .flex-shrink-0 > div {
    font-size: 4rem !important; /* Default: 3rem */
}
```

### Change Animation Speed

```css
.modal.fade .modal-dialog {
    transition: transform 0.5s ease-out; /* Default: 0.3s */
}
```

## ?? Troubleshooting

### Modal doesn't show

1. Check `MessageBoxContainer` is in `MainLayout.razor`
2. Check service is registered in `Program.cs`
3. Check Bootstrap CSS is loaded

### Backdrop doesn't work

1. Verify `ShowBackdrop = true` in model
2. Check CSS is loaded properly
3. Inspect browser console for errors

### Buttons don't work

1. Check `@onclick` handlers
2. Verify `Close()` method is called
3. Check browser console for JS errors

## ?? References

- [Bootstrap Modal Docs](https://getbootstrap.com/docs/5.3/components/modal/)
- [Bootstrap Icons](https://icons.getbootstrap.com/)
- [CSS Transitions](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Transitions)

---

**Version:** 2.0 (Pure Bootstrap)  
**Author:** Your Team  
**License:** MIT  
**Last Updated:** 2025
