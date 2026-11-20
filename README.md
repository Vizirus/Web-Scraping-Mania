# Web Scraping Mania

A desktop web scraping and source-code extraction tool built with **C# / .NET 8** and **WPF** using the **MVVM** pattern. Originally developed as a computer science research project on *"Website Parsing Mechanisms"* for the Junior Academy of Sciences of Ukraine (MAN), and later re-architected into a modular desktop application with Dependency Injection.

## Research Context & Results

Unlike typical SEO/content scrapers that only extract visible text or product tables, **Web Scraping Mania** focuses on **extracting and analyzing underlying source code** (**HTML**, **CSS**, and **JavaScript** resources) directly from a target page's DOM tree using **XPath** queries and attribute filtering.

### Key Outcomes
- **Dual-Mode Extraction Workflow:**
  1. **Visual Selection Mode:** Inspect any live webpage inside the embedded **WebView2** preview and jump directly to the underlying source code of the active or selected DOM element.
  2. **Query-Based Parsing Mode:** Traverse the DOM via `HtmlAgilityPack` and XPath-style criteria (by HTML tag, attribute name, and attribute value) to isolate matching code fragments.
- **Multi-Resource Separation:** Automatically separates and displays fetched **HTML**, **CSS**, and **JS** source files across dedicated workspace tabs for developer inspection and code structure auditing.
- **Automated Retrieval & Export:** Batch-downloads linked web resources and exports extracted code fragments locally (`DownloadFromWebViewModel`, `SaveFunctions`).
- **Architectural Evolution:** Refactored from an initial research prototype into a maintainable **WPF + MVVM** codebase (`CommunityToolkit.Mvvm`, `Microsoft.Extensions.DependencyInjection`, `Microsoft.Extensions.Hosting`).

## Recognition & Awards

- **Junior Academy of Sciences of Ukraine (MAN):** 1st Place (Poltava Region) & **National Finalist (11th Nationwide)** in Computer Science Research
- **EcoSoft-2024:** 1st Place
- **Regional IT Olympiad:** Prize Winner

## Key Features

- **DOM Parsing by Tag & Attributes:** Filter and extract specific HTML nodes and attributes (`ParseByTagAndArgViewModel`, `SearchParseFunctions`).
- **Integrated Web Preview:** Built-in browser preview powered by **Microsoft WebView2** (`OpenPreviewWindowViewModel`) for live visual element inspection.
- **Multi-Tab Workspace:** Manage multiple URLs, source tabs, and parsing configurations concurrently (`AddNewTabViewModel`, `ShellViewModel`).
- **Batch File Downloading & Export:** Automated retrieval and local saving of extracted source files and web assets.

## Architecture & Tech Stack

- **Language & Runtime:** C# / .NET 8 (`net8.0-windows7.0`)
- **UI & Architecture:** WPF, MVVM (`CommunityToolkit.Mvvm`), `Microsoft.Extensions.DependencyInjection`, `Microsoft.Extensions.Hosting`
- **Parsing & Rendering:** `HtmlAgilityPack` (DOM/XPath), `Microsoft.Web.WebView2`
- **Structure:** Modular separation into `Models`, `ViewModels`, `Views`, and `Commands` (`CommandBase` + parsing, selection, and export helpers)

## Getting Started

1. Open `Web Scraping Mania.sln` in Visual Studio 2022+ (or Rider) with the **.NET Desktop Development** workload installed.
2. Restore NuGet packages and run the `Web Scraping Mania` startup project.
