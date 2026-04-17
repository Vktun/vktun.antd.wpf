# WPF Ant Design Coding Rules

These rules apply to `src/Vktun.Antd.Wpf`, `samples/Vktun.Antd.Wpf.Sample`, and `tests/Vktun.Antd.Wpf.Tests`.

## Architecture Boundaries

- Keep platform-neutral tokens, enums, color math, and resource keys in `Vktun.Antd.Core`.
- Keep WPF controls, dependency properties, services, converters, behaviors, and resource dictionaries in `Vktun.Antd.Wpf`.
- Keep sample-only pages, snippets, and catalog resources in `samples/Vktun.Antd.Wpf.Sample`.
- Do not place WPF types in `Vktun.Antd.Core`.
- Do not mix Avalonia resource or control APIs into WPF code.
- Prefer one public control concept per file under `Controls`.
- Use namespace `Vktun.Antd.Wpf` for WPF public APIs unless the existing file already uses a more specific namespace.

## Public API Rules

- Add XML documentation for every public class, interface, method, property, enum, and dependency property field.
- Prefer nullable annotations that match WPF semantics. Use nullable `object?`, `string?`, and event handlers where null is a valid state.
- Use meaningful Ant Design semantic names: `Type`, `Size`, `Status`, `Variant`, `Placement`, `Kind`, `Mode`, `CurrentPage`.
- Keep public API names stable once introduced. If an older API must remain temporarily, mark it `[Obsolete("Use ... instead. Will be removed in vX.")]`.
- Prefer strongly typed enums from `Vktun.Antd.Core` over strings for semantic variants.
- Throw `ArgumentNullException.ThrowIfNull(...)` and `ArgumentException.ThrowIfNullOrWhiteSpace(...)` at public service and helper boundaries.

## WPF Control Rules

- Derive custom controls from the closest WPF base class that already owns the expected behavior.
- For lookless controls, override `DefaultStyleKeyProperty` in the static constructor.
- Keep visual structure in XAML templates. Keep state, coercion, commands, and event routing in C#.
- Use `TemplatePart` attributes for required named template parts when a control reads parts in `OnApplyTemplate`.
- Use `PART_` names for template parts consumed by C#.
- Detach previous template-part event handlers before attaching new handlers in `OnApplyTemplate`.
- Keep routed events and dependency properties as the external contract. Avoid exposing visual child controls as public mutable state.
- Avoid theme-specific color decisions in C# unless the value is computed from tokens.
- Use `Dispatcher` work only when WPF layout or template timing requires it, and keep the reason visible in code.

## Dependency Property Rules

- Declare dependency properties as `public static readonly DependencyProperty XxxProperty`.
- Register with `nameof(Xxx)` for owner controls and the literal attached-property name for attached properties.
- Use `PropertyMetadata`, `FrameworkPropertyMetadata`, or `CoerceValueCallback` intentionally based on behavior.
- Keep CLR wrappers immediately after their dependency property field.
- Set defaults that match Ant Design semantics and existing project conventions.
- Validate or coerce numeric input where invalid values can break layout or interaction, such as page indexes, precision, min/max, and dimensions.
- Attached property helpers must be named `GetXxx` and `SetXxx`, validate `DependencyObject`, and live under `Assists` unless they are core control API.

## XAML Resource Rules

- Put shared foundational resources in `Themes/Styles/Foundation.xaml`.
- Put component templates into the closest existing category dictionary, for example `Buttons.xaml`, `Inputs.xaml`, `Navigation.xaml`, `DataDisplay.xaml`, or `Overlay.xaml`.
- Add each new style dictionary to `Themes/Generic.xaml` exactly once.
- Use `DynamicResource` for theme-dependent brushes, sizes, shadows, typography, and radii.
- Resolve token resources through `core:AntdResourceKeys` with `{x:Static ...}`.
- Avoid hard-coded colors except for semantic WPF values such as `Transparent` or token-defined constants already represented by resources.
- Prefer `TemplateBinding` for direct templated-parent property forwarding.
- Use triggers for simple visual states and C# state when behavior requires coordination or calculation.
- Keep comments short and readable. Do not leave mojibake or placeholder comments.

## Theme And Token Rules

- Theme switching must keep existing resource dictionaries valid at runtime.
- New theme values should start in `Vktun.Antd.Core` tokens or resource keys, then be projected into WPF resources.
- Controls should consume brushes and dimensions from resources instead of constructing theme brushes inline.
- If a control changes appearance by `Type`, `Status`, or `Size`, each state must work in light and dark themes.
- Do not freeze brushes or transforms that a control later mutates.

## Services And Overlay Rules

- Message, notification, modal, drawer, popconfirm, float button, and similar floating UI should route through `OverlayHost` or the existing overlay pattern.
- Services should validate owner windows and required text/content before creating UI.
- Keep default durations and layout behavior centralized in service/control code, not repeated across samples.
- Async service APIs should return `Task` or `Task<T>` and complete from explicit user actions.

## Sample Rules

- Every new component should have a sample page entry, snippet, or catalog resource showing the intended public API.
- Sample XAML should use the public API consumers are expected to use.
- Keep sample code lightweight. Do not hide required setup behind sample-only helpers unless the setup is not part of the library contract.

## Test Rules

- Add or update tests under `tests/Vktun.Antd.Wpf.Tests` for every behavior change.
- Wrap WPF work in `WpfTestHost.Run`.
- Initialize `Application.Current.Resources` and merge `AntdThemeResources` before testing template/theme behavior.
- Use `FluentAssertions` for assertions.
- For templates, verify named parts, resource-backed brushes, theme switching, and core interaction behavior.
- For controls with numeric or selection state, test clamping, synchronization, and command/event behavior.
- Pump the WPF dispatcher after template application, window show, event raise, or state change that depends on layout.
- Close windows in `finally` blocks.

## Review Checklist

- Public API has XML docs and stable names.
- Dependency properties have sensible defaults, wrappers, validation, and tests.
- Styles use dynamic token resources and are merged through `Generic.xaml`.
- Light/dark theme switching is covered when visual resources change.
- Template parts use `PART_` naming and are verified in tests when consumed by C#.
- Samples demonstrate the intended consumer-facing API.
- `dotnet build Vktun.Antd.slnx` and relevant tests pass before completion.
