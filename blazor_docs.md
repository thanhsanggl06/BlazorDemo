# Blazor Layout Components Documentation

Bộ thư viện Blazor components với API tương tự Material-UI nhưng sử dụng Bootstrap.

## 📋 Mục lục

1. [Cài đặt](#cài-đặt)
2. [Container](#container)
3. [Grid](#grid)
4. [Stack](#stack)
5. [Box](#box)
6. [Ví dụ thực tế](#ví-dụ-thực-tế)
7. [Best Practices](#best-practices)

---

## Cài đặt

### Yêu cầu
- .NET 6.0 trở lên
- Bootstrap 5.0+ (đã được thêm vào project)

### Thêm vào project

1. Copy các file components vào thư mục `Components/Layout`:
   - `Container.razor`
   - `Grid.razor`
   - `Stack.razor`
   - `Box.razor`

2. Đảm bảo Bootstrap CSS được load trong `index.html` hoặc `_Layout.cshtml`:

```html
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
```

3. Import namespace trong `_Imports.razor`:

```razor
@using YourProject.Components.Layout
```

---

## Container

Container là wrapper component cung cấp max-width và padding responsive.

### Props

| Prop | Type | Default | Mô tả |
|------|------|---------|-------|
| `MaxWidth` | string | `"lg"` | Kích thước tối đa: `"xs"`, `"sm"`, `"md"`, `"lg"`, `"xl"`, `"xxl"`, `"fluid"` |
| `Fixed` | bool | `false` | Sử dụng fixed container (container-{breakpoint}) |
| `DisableGutters` | bool | `false` | Tắt padding trái/phải |
| `Class` | string | `null` | CSS classes bổ sung |
| `Style` | string | `null` | Inline styles |
| `ChildContent` | RenderFragment | - | Nội dung bên trong |

### Breakpoints

| MaxWidth | Kích thước |
|----------|------------|
| `xs` | 100% |
| `sm` | 540px |
| `md` | 720px |
| `lg` | 960px |
| `xl` | 1140px |
| `xxl` | 1320px |
| `fluid` | 100% (full width) |

### Ví dụ

```razor
<!-- Container cơ bản -->
<Container>
    <p>Content here</p>
</Container>

<!-- Container với max-width -->
<Container MaxWidth="md">
    <p>Medium width container</p>
</Container>

<!-- Container full width -->
<Container MaxWidth="fluid">
    <p>Full width content</p>
</Container>

<!-- Container không có padding -->
<Container DisableGutters="true">
    <p>No horizontal padding</p>
</Container>

<!-- Container fixed -->
<Container Fixed="true" MaxWidth="lg">
    <p>Fixed large container</p>
</Container>
```

---

## Grid

Grid system 12 cột responsive giống Bootstrap.

### Props - Grid Container

| Prop | Type | Default | Mô tả |
|------|------|---------|-------|
| `Container` | bool | `false` | Đánh dấu là Grid container |
| `Spacing` | int | `0` | Khoảng cách giữa các items (0-5) |
| `JustifyContent` | string | `null` | Căn chỉnh theo trục ngang: `start`, `center`, `end`, `between`, `around`, `evenly` |
| `AlignItems` | string | `null` | Căn chỉnh theo trục dọc: `start`, `center`, `end`, `baseline`, `stretch` |
| `Direction` | string | `null` | Hướng flex: `row`, `row-reverse`, `column`, `column-reverse` |

### Props - Grid Item

| Prop | Type | Default | Mô tả |
|------|------|---------|-------|
| `Item` | bool | `false` | Đánh dấu là Grid item |
| `Xs` | int? | `null` | Số cột trên extra small screens (1-12) |
| `Sm` | int? | `null` | Số cột trên small screens (1-12) |
| `Md` | int? | `null` | Số cột trên medium screens (1-12) |
| `Lg` | int? | `null` | Số cột trên large screens (1-12) |
| `Xl` | int? | `null` | Số cột trên extra large screens (1-12) |
| `Xxl` | int? | `null` | Số cột trên extra extra large screens (1-12) |

**Lưu ý**: Nếu set giá trị = `0`, cột sẽ tự động điều chỉnh width (col-auto).

### Ví dụ

```razor
<!-- Grid 2 cột đều nhau -->
<Grid Container="true" Spacing="3">
    <Grid Item="true" Xs="6">
        <p>Column 1</p>
    </Grid>
    <Grid Item="true" Xs="6">
        <p>Column 2</p>
    </Grid>
</Grid>

<!-- Grid responsive -->
<Grid Container="true" Spacing="4">
    <Grid Item="true" Xs="12" Md="6" Lg="4">
        <p>Full width mobile, 1/2 tablet, 1/3 desktop</p>
    </Grid>
    <Grid Item="true" Xs="12" Md="6" Lg="4">
        <p>Full width mobile, 1/2 tablet, 1/3 desktop</p>
    </Grid>
    <Grid Item="true" Xs="12" Md="12" Lg="4">
        <p>Full width mobile & tablet, 1/3 desktop</p>
    </Grid>
</Grid>

<!-- Grid với alignment -->
<Grid Container="true" JustifyContent="center" AlignItems="center" Spacing="2">
    <Grid Item="true" Xs="4">
        <p>Centered item</p>
    </Grid>
</Grid>

<!-- Grid tự động chia đều -->
<Grid Container="true" Spacing="3">
    <Grid Item="true">
        <p>Auto width</p>
    </Grid>
    <Grid Item="true">
        <p>Auto width</p>
    </Grid>
    <Grid Item="true">
        <p>Auto width</p>
    </Grid>
</Grid>

<!-- Grid nested -->
<Grid Container="true" Spacing="3">
    <Grid Item="true" Xs="12" Md="6">
        <Grid Container="true" Spacing="2">
            <Grid Item="true" Xs="6">Nested 1</Grid>
            <Grid Item="true" Xs="6">Nested 2</Grid>
        </Grid>
    </Grid>
    <Grid Item="true" Xs="12" Md="6">
        <p>Content</p>
    </Grid>
</Grid>
```

---

## Stack

Stack component tạo flexbox layout với spacing tự động giữa các items.

### Props

| Prop | Type | Default | Mô tả |
|------|------|---------|-------|
| `Direction` | string | `"column"` | Hướng stack: `column`, `row`, `column-reverse`, `row-reverse` |
| `Spacing` | int | `2` | Khoảng cách giữa items (0-5) |
| `JustifyContent` | string | `null` | Căn chỉnh theo trục chính: `start`, `center`, `end`, `between`, `around`, `evenly` |
| `AlignItems` | string | `null` | Căn chỉnh theo trục phụ: `start`, `center`, `end`, `baseline`, `stretch` |
| `Divider` | bool | `false` | Hiển thị divider giữa items |
| `DividerColor` | string | `"border"` | Màu divider (Bootstrap color) |
| `Class` | string | `null` | CSS classes bổ sung |
| `Style` | string | `null` | Inline styles |

### Spacing Values

| Value | Gap |
|-------|-----|
| `0` | 0 |
| `1` | 0.25rem (4px) |
| `2` | 0.5rem (8px) |
| `3` | 1rem (16px) |
| `4` | 1.5rem (24px) |
| `5` | 3rem (48px) |

### Ví dụ

```razor
<!-- Stack dọc cơ bản -->
<Stack>
    <div>Item 1</div>
    <div>Item 2</div>
    <div>Item 3</div>
</Stack>

<!-- Stack ngang -->
<Stack Direction="row" Spacing="3">
    <button>Button 1</button>
    <button>Button 2</button>
    <button>Button 3</button>
</Stack>

<!-- Stack với alignment -->
<Stack Direction="row" JustifyContent="center" AlignItems="center" Spacing="4">
    <div>Centered Item 1</div>
    <div>Centered Item 2</div>
</Stack>

<!-- Stack với divider -->
<Stack Direction="column" Spacing="3" Divider="true">
    <div>Section 1</div>
    <div>Section 2</div>
    <div>Section 3</div>
</Stack>

<!-- Stack responsive (dọc mobile, ngang desktop) -->
<Stack Direction="column" Class="flex-md-row" Spacing="3">
    <div>Item 1</div>
    <div>Item 2</div>
    <div>Item 3</div>
</Stack>

<!-- Stack reverse -->
<Stack Direction="column-reverse" Spacing="2">
    <div>Last (shown first)</div>
    <div>Middle</div>
    <div>First (shown last)</div>
</Stack>

<!-- Stack với spacing lớn -->
<Stack Direction="row" Spacing="5" JustifyContent="between">
    <div>Left</div>
    <div>Right</div>
</Stack>
```

---

## Box

Box là primitive component với nhiều styling props, tương tự `<div>` nhưng với utility props.

### Props - Spacing

| Prop | Type | Mô tả |
|------|------|-------|
| `M` | int? | Margin all sides (0-5) |
| `Mt` | int? | Margin top |
| `Mr` | int? | Margin right |
| `Mb` | int? | Margin bottom |
| `Ml` | int? | Margin left |
| `Mx` | int? | Margin horizontal (left & right) |
| `My` | int? | Margin vertical (top & bottom) |
| `P` | int? | Padding all sides (0-5) |
| `Pt` | int? | Padding top |
| `Pr` | int? | Padding right |
| `Pb` | int? | Padding bottom |
| `Pl` | int? | Padding left |
| `Px` | int? | Padding horizontal |
| `Py` | int? | Padding vertical |

### Props - Display & Flexbox

| Prop | Type | Mô tả |
|------|------|-------|
| `Display` | string | `none`, `inline`, `block`, `flex`, `grid`, `inline-flex`, etc. |
| `JustifyContent` | string | `start`, `center`, `end`, `between`, `around`, `evenly` |
| `AlignItems` | string | `start`, `center`, `end`, `baseline`, `stretch` |
| `FlexDirection` | string | `row`, `column`, `row-reverse`, `column-reverse` |
| `FlexWrap` | string | `wrap`, `nowrap`, `wrap-reverse` |
| `Flex` | int? | Flex grow/shrink/basis shorthand |
| `Gap` | int? | Gap between flex/grid items (0-5) |

### Props - Sizing

| Prop | Type | Mô tả |
|------|------|-------|
| `Width` | string | Width (e.g., "100%", "300px", "50vw") |
| `Height` | string | Height (e.g., "100%", "200px", "50vh") |
| `MinWidth` | string | Minimum width |
| `MinHeight` | string | Minimum height |
| `MaxWidth` | string | Maximum width |
| `MaxHeight` | string | Maximum height |

### Props - Colors

| Prop | Type | Mô tả |
|------|------|-------|
| `Bg` | string | Background color: `primary`, `secondary`, `success`, `danger`, `warning`, `info`, `light`, `dark`, `white`, `transparent` |
| `Color` | string | Text color: same as Bg |

### Props - Border

| Prop | Type | Mô tả |
|------|------|-------|
| `Border` | bool | Add border |
| `BorderColor` | string | Border color (Bootstrap colors) |
| `BorderRadius` | int? | Border radius (0-5, hoặc pill, circle) |

### Props - Position

| Prop | Type | Mô tả |
|------|------|-------|
| `Position` | string | `static`, `relative`, `absolute`, `fixed`, `sticky` |
| `Top` | string | Top offset |
| `Right` | string | Right offset |
| `Bottom` | string | Bottom offset |
| `Left` | string | Left offset |

### Props - Overflow

| Prop | Type | Mô tả |
|------|------|-------|
| `Overflow` | string | `auto`, `hidden`, `visible`, `scroll` |
| `OverflowX` | string | Overflow horizontal |
| `OverflowY` | string | Overflow vertical |

### Props - Text

| Prop | Type | Mô tả |
|------|------|-------|
| `TextAlign` | string | `start`, `center`, `end` |
| `FontWeight` | string | `light`, `normal`, `bold`, `bolder` |
| `FontSize` | string | Font size (e.g., "14px", "1.2rem") |

### Props - Other

| Prop | Type | Mô tả |
|------|------|-------|
| `Shadow` | string | Box shadow: `sm`, `default`, `lg` |
| `Class` | string | Additional CSS classes |
| `Style` | string | Inline styles |

### Ví dụ

```razor
<!-- Box cơ bản với padding và background -->
<Box P="4" Bg="light">
    <p>Content with padding</p>
</Box>

<!-- Box flexbox -->
<Box Display="flex" JustifyContent="between" AlignItems="center" P="3">
    <span>Left</span>
    <span>Right</span>
</Box>

<!-- Box với kích thước cố định -->
<Box Width="300px" Height="200px" Bg="primary" Color="white" P="3">
    <p>Fixed size box</p>
</Box>

<!-- Box centered -->
<Box Display="flex" JustifyContent="center" AlignItems="center" Height="100vh">
    <h1>Centered Content</h1>
</Box>

<!-- Box với border và shadow -->
<Box P="4" Border="true" BorderRadius="3" Shadow="lg" Bg="white">
    <h3>Card-like Box</h3>
    <p>With shadow and rounded corners</p>
</Box>

<!-- Box với spacing -->
<Box M="3" P="4" Mb="5">
    <p>Margin and padding</p>
</Box>

<!-- Box absolute positioning -->
<Box Position="relative" Height="200px" Bg="light">
    <Box Position="absolute" Top="10px" Right="10px" Bg="danger" Color="white" P="2">
        Badge
    </Box>
</Box>

<!-- Box overflow -->
<Box Width="300px" Height="100px" Overflow="auto" Border="true" P="2">
    <p>Long content that will scroll...</p>
    <p>More content...</p>
    <p>Even more content...</p>
</Box>

<!-- Box responsive -->
<Box 
    Display="flex" 
    FlexDirection="column" 
    Class="flex-md-row"
    Gap="3"
    P="4">
    <Box Flex="1" Bg="primary" Color="white" P="3">Column 1</Box>
    <Box Flex="1" Bg="secondary" Color="white" P="3">Column 2</Box>
</Box>
```

---

## Ví dụ thực tế

### 1. Dashboard Layout

```razor
<Container MaxWidth="xl">
    <!-- Header -->
    <Box Bg="white" Shadow="sm" P="3" Mb="4">
        <Box Display="flex" JustifyContent="between" AlignItems="center">
            <h1>Dashboard</h1>
            <Stack Direction="row" Spacing="2">
                <button class="btn btn-outline-primary">Settings</button>
                <button class="btn btn-primary">Logout</button>
            </Stack>
        </Box>
    </Box>

    <!-- Main Content -->
    <Grid Container="true" Spacing="4">
        <!-- Sidebar -->
        <Grid Item="true" Xs="12" Md="3">
            <Box Bg="white" P="3" Shadow="sm" BorderRadius="2">
                <h5>Navigation</h5>
                <Stack Direction="column" Spacing="2">
                    <Box P="2" Bg="primary" Color="white" BorderRadius="1">
                        Home
                    </Box>
                    <Box P="2" Class="bg-light" BorderRadius="1">
                        Profile
                    </Box>
                    <Box P="2" Class="bg-light" BorderRadius="1">
                        Settings
                    </Box>
                </Stack>
            </Box>
        </Grid>

        <!-- Content -->
        <Grid Item="true" Xs="12" Md="9">
            <Grid Container="true" Spacing="3">
                <!-- Stats Cards -->
                <Grid Item="true" Xs="12" Sm="6" Lg="3">
                    <Box Bg="white" P="3" Shadow="sm" BorderRadius="2">
                        <h6 class="text-muted">Total Users</h6>
                        <h2>1,234</h2>
                    </Box>
                </Grid>
                <Grid Item="true" Xs="12" Sm="6" Lg="3">
                    <Box Bg="white" P="3" Shadow="sm" BorderRadius="2">
                        <h6 class="text-muted">Revenue</h6>
                        <h2>$45,678</h2>
                    </Box>
                </Grid>
                <Grid Item="true" Xs="12" Sm="6" Lg="3">
                    <Box Bg="white" P="3" Shadow="sm" BorderRadius="2">
                        <h6 class="text-muted">Orders</h6>
                        <h2>567</h2>
                    </Box>
                </Grid>
                <Grid Item="true" Xs="12" Sm="6" Lg="3">
                    <Box Bg="white" P="3" Shadow="sm" BorderRadius="2">
                        <h6 class="text-muted">Products</h6>
                        <h2>89</h2>
                    </Box>
                </Grid>
            </Grid>
        </Grid>
    </Grid>
</Container>
```

### 2. Form Layout

```razor
<Container MaxWidth="md">
    <Box Bg="white" P="4" Shadow="lg" BorderRadius="3">
        <h2 class="mb-4">User Registration</h2>
        
        <Stack Direction="column" Spacing="3">
            <!-- Name Row -->
            <Grid Container="true" Spacing="3">
                <Grid Item="true" Xs="12" Md="6">
                    <Box>
                        <label class="form-label">First Name</label>
                        <input type="text" class="form-control" />
                    </Box>
                </Grid>
                <Grid Item="true" Xs="12" Md="6">
                    <Box>
                        <label class="form-label">Last Name</label>
                        <input type="text" class="form-control" />
                    </Box>
                </Grid>
            </Grid>

            <!-- Email -->
            <Box>
                <label class="form-label">Email</label>
                <input type="email" class="form-control" />
            </Box>

            <!-- Password -->
            <Box>
                <label class="form-label">Password</label>
                <input type="password" class="form-control" />
            </Box>

            <!-- Actions -->
            <Box Display="flex" JustifyContent="end" Gap="2" Mt="3">
                <button class="btn btn-outline-secondary">Cancel</button>
                <button class="btn btn-primary">Submit</button>
            </Box>
        </Stack>
    </Box>
</Container>
```

### 3. Product Grid

```razor
<Container>
    <h2 class="mb-4">Our Products</h2>
    
    <Grid Container="true" Spacing="4">
        @foreach (var product in products)
        {
            <Grid Item="true" Xs="12" Sm="6" Md="4" Lg="3">
                <Box Bg="white" Shadow="sm" BorderRadius="2" Overflow="hidden">
                    <!-- Image -->
                    <Box Width="100%" Height="200px" Bg="light">
                        <img src="@product.Image" class="w-100 h-100" style="object-fit: cover;" />
                    </Box>
                    
                    <!-- Content -->
                    <Box P="3">
                        <h5>@product.Name</h5>
                        <p class="text-muted">@product.Description</p>
                        <Box Display="flex" JustifyContent="between" AlignItems="center" Mt="3">
                            <h4 class="mb-0">$@product.Price</h4>
                            <button class="btn btn-primary btn-sm">Add to Cart</button>
                        </Box>
                    </Box>
                </Box>
            </Grid>
        }
    </Grid>
</Container>
```

### 4. Hero Section

```razor
<Box Bg="primary" Color="white" Py="5">
    <Container>
        <Grid Container="true" AlignItems="center" Spacing="5">
            <Grid Item="true" Xs="12" Md="6">
                <Stack Direction="column" Spacing="3">
                    <h1 class="display-4">Welcome to Our Platform</h1>
                    <p class="lead">
                        Build amazing applications with our powerful tools and components.
                    </p>
                    <Box>
                        <Stack Direction="row" Spacing="2">
                            <button class="btn btn-light btn-lg">Get Started</button>
                            <button class="btn btn-outline-light btn-lg">Learn More</button>
                        </Stack>
                    </Box>
                </Stack>
            </Grid>
            <Grid Item="true" Xs="12" Md="6">
                <Box Width="100%" Height="400px" Bg="light" BorderRadius="3">
                    <!-- Hero Image -->
                </Box>
            </Grid>
        </Grid>
    </Container>
</Box>
```

### 5. Modal/Dialog

```razor
<Box 
    Position="fixed" 
    Top="0" 
    Left="0" 
    Width="100vw" 
    Height="100vh" 
    Display="flex" 
    JustifyContent="center" 
    AlignItems="center"
    Style="background: rgba(0,0,0,0.5);">
    
    <Box 
        Bg="white" 
        Width="500px" 
        MaxWidth="90%" 
        BorderRadius="3" 
        Shadow="lg">
        
        <!-- Header -->
        <Box P="4" Class="border-bottom">
            <Box Display="flex" JustifyContent="between" AlignItems="center">
                <h4 class="mb-0">Modal Title</h4>
                <button class="btn-close"></button>
            </Box>
        </Box>
        
        <!-- Body -->
        <Box P="4">
            <p>Modal content goes here...</p>
        </Box>
        
        <!-- Footer -->
        <Box P="4" Class="border-top">
            <Stack Direction="row" JustifyContent="end" Spacing="2">
                <button class="btn btn-outline-secondary">Cancel</button>
                <button class="btn btn-primary">Save Changes</button>
            </Stack>
        </Box>
    </Box>
</Box>
```

---

## Best Practices

### 1. Responsive Design

Luôn suy nghĩ mobile-first:

```razor
<!-- ✅ Good -->
<Grid Item="true" Xs="12" Md="6" Lg="4">
    Content
</Grid>

<!-- ❌ Avoid -->
<Grid Item="true" Lg="4">
    Content (sẽ bị full width trên mobile)
</Grid>
```

### 2. Spacing Consistency

Sử dụng spacing scale nhất quán (0-5):

```razor
<!-- ✅ Good: Consistent spacing -->
<Stack Spacing="3">
    <Box P="3">Item 1</Box>
    <Box P="3">Item 2</Box>
</Stack>

<!-- ❌ Avoid: Mixed spacing -->
<Stack Spacing="3">
    <Box P="5">Item 1</Box>
    <Box P="1">Item 2</Box>
</Stack>
```

### 3. Semantic Structure

Sử dụng đúng component cho đúng mục đích:

```razor
<!-- ✅ Good: Stack cho vertical list -->
<Stack Direction="column" Spacing="2">
    <div>Item 1</div>
    <div>Item 2</div>
</Stack>

<!-- ❌ Avoid: Grid cho simple vertical list -->
<Grid Container="true">
    <Grid Item="true" Xs="12">Item 1</Grid>
    <Grid Item="true" Xs="12">Item 2</Grid>
</Grid>
```

### 4. Nesting Grid

Grid có thể nest nhưng đừng nest quá sâu:

```razor
<!-- ✅ Good: 2 levels -->
<Grid Container="true">
    <Grid Item="true" Xs="6">
        <Grid Container="true">
            <Grid Item="true" Xs="12">Nested</Grid>
        </Grid>
    </Grid>
</Grid>

<!-- ❌ Avoid: 4+ levels -->
<Grid Container="true">
    <Grid Item="true">
        <Grid Container="true">
            <Grid Item="true">
                <Grid Container="true">...too deep</Grid>
            </Grid>
        </Grid>
    </Grid>
</Grid>
```

### 5. Performance

Tránh inline styles phức tạp, dùng CSS classes:

```razor
<!-- ✅ Good -->
<Box Class="custom-box" P="3">
    Content
</Box>

<!-- ❌ Avoid -->
<Box Style="padding: 16px; background: linear-gradient(...); border: 2px solid red; box-shadow: ...;">
    Content
</Box>
```

### 6. Container Usage

Chỉ dùng một Container ở top level:

```razor
<!-- ✅ Good -->
<Container>
    <Grid Container="true">
        <Grid Item="true" Xs="12">Content</Grid>
    </Grid>
</Container>

<!-- ❌ Avoid: Multiple nested Containers -->
<Container>
    <Container>
        <Container>Content</Container>
    </Container>
</Container>
```

### 7. Accessibility

Luôn nghĩ về accessibility:

```razor
<!-- ✅ Good -->
<Box Component="nav" Role="navigation">
    <Stack Direction="row" Spacing="3">
        <a href="/">Home</a>
        <a href="/about">About</a>
    </Stack>
</Box>

<!-- Add ARIA labels khi cần -->
<Box Display="flex" aria-label="User actions">
    <button>Edit</button>
    <button>Delete</button>
</Box>
```

### 8. Color System

Sử dụng Bootstrap color system:

```razor
<!-- ✅ Good: Bootstrap colors -->
<Box Bg="primary" Color="white">Primary</Box>
<Box Bg="success" Color="white">Success</Box>
<Box Bg="danger" Color="white">Danger</Box>

<!-- ✅ Also good: Custom CSS -->
<Box Class="custom-bg" Color="white">Custom</Box>
```

---

## Tips & Tricks

### 1. Full Height Layout

```razor
<Box Display="flex" FlexDirection="column" MinHeight="100vh">
    <!-- Header -->
    <Box Bg="primary" Color="white" P="3">
        Header
    </Box>
    
    <!-- Main (grows to fill space) -->
    <Box Flex="1" P="4">
        Main Content
    </Box>
    
    <!-- Footer -->
    <Box Bg="dark" Color="white" P="3">
        Footer
    </Box>
</Box>
```

### 2. Centered Page

```razor
<Box Display="flex" JustifyContent="center" AlignItems="center" MinHeight="100vh">
    <Box Width="400px" P="4" Bg="white" Shadow="lg" BorderRadius="3">
        <h2>Login</h2>
        <!-- Form content -->
    </Box>
</Box>
```

### 3. Sticky Header

```razor
<Box Position="sticky" Top="0" Bg="white" Shadow="sm" P="3" Style="z-index: 1000;">
    Navigation
</Box>
```

### 4. Responsive Image Grid

```razor
<Grid Container="true" Spacing="2">
    @foreach (var image in images)
    {
        <Grid Item="true" Xs="6" Sm="4" Md="3" Lg="2">
            <Box Width="100%" Height="0" Style="padding-bottom: 100%; position: relative;">
                <img 
                    src="@image" 
                    style="position: absolute; width: 100%; height: 100%; object-fit: cover;" 
                />
            </Box>
        </Grid>
    }
</Grid>
```

### 5. Card with Hover Effect

```razor
<Box 
    Bg="white" 
    P="3" 
    Shadow="sm" 
    BorderRadius="2"
    Class="hover-card"
    Style="transition: all 0.3s;">
    <h5>Card Title</h5>
    <p>Card content</p>
</Box>

<style>
.hover-card:hover {
    transform: translateY(-4px);
    box-shadow: 0 0.5rem 1rem rgba(0,0,0,0.15) !important;
}
</style>
```

### 6. Masonry-like Grid

```razor
<Grid Container="true" Spacing="3">
    <Grid Item="true" Xs="12" Sm="6" Md="4">
        <Box P="3" Bg="light" BorderRadius="2" Height="200px">Short</Box>
    </Grid>
    <Grid Item="true" Xs="12" Sm="6" Md="4">
        <Box P="3" Bg="light" BorderRadius="2" Height="300px">Tall</Box>
    </Grid>
    <Grid Item="true" Xs="12" Sm="6" Md="4">
        <Box P="3" Bg="light" BorderRadius="2" Height="250px">Medium</Box>
    </Grid>
</Grid>
```

### 7. Loading Skeleton

```razor
<Stack Direction="column" Spacing="3">
    @for (int i = 0; i < 3; i++)
    {
        <Box Bg="light" P="3" BorderRadius="2" Class="placeholder-glow">
            <Box Class="placeholder col-6 mb-2"></Box>
            <Box Class="placeholder col-12"></Box>
        </Box>
    }
</Stack>
```

---

## Troubleshooting

### Component không hiển thị đúng

1. Kiểm tra Bootstrap CSS đã được load chưa
2. Kiểm tra namespace import trong `_Imports.razor`
3. Kiểm tra cú pháp props (dùng `"3"` chứ không phải `{3}`)

### Grid không responsive

- Đảm bảo set props từ nhỏ đến lớn: `Xs`, `Sm`, `Md`, `Lg`
- Mobile-first: bắt đầu với `Xs="12"`

### Spacing không đúng

- Bootstrap spacing: 0-5 (không phải pixel)
- 1 = 0.25rem, 2 = 0.5rem, 3 = 1rem, 4 = 1.5rem, 5 = 3rem

### Colors không work

- Chỉ dùng Bootstrap color names: `primary`, `secondary`, `success`, `danger`, `warning`, `info`, `light`, `dark`
- Hoặc dùng custom CSS class

---

## Changelog

### Version 1.0.0
- Initial release
- Container, Grid, Stack, Box components
- Full Bootstrap 5 integration
- Responsive design support

---

## License & Support

Components được xây dựng dựa trên Bootstrap 5 và tuân theo MIT License.

Nếu gặp vấn đề hoặc có câu hỏi, vui lòng tạo issue hoặc liên hệ team development.

---

**Happy Coding! 🚀**