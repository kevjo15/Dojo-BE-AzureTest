# Coding Conventions

## Introduction

This document outlines the coding conventions and guidelines to be followed when working on this project. By following
these conventions, we aim to maintain a clean, organized, and well-crafted and well-tested .NET API solution that
adheres to the principles of Clean Architecture.

## Table of Contents

- [Clean Architecture](#clean-architecture)
- [Unit Testing](#unit-testing)
- [Code Formatting](#code-formatting)
- [Pull Request](#pull-request)
- [Clean Code Principles](#clean-code-principles)
- [C# Language Guidelines](#c-language-guidelines)

## Clean Architecture

This project is built according tot he principles of Clean Architecture, to ensure separation of concerns, making the
API domain-centric, with a clear distinction between the `Domain`, `Application`, `Infrastructure` and `API` layers.

![Clean Architecture Model](https://github.com/InFiNet-Code-AB/Dojo-BE/assets/49125242/cc31085c-6510-4a98-87ae-9511eaf0f375)

## Unit Testing

Ensure that all public classes and functions are tested. Ensure that tests are isolated and all dependencies mocked.

### Frameworks

- Testing Framework: **To Be Discussed** (e.g. MSTest, NUnit, xUnit)
- Mocking Framework: **To Be Discussed** (NSubstitute, Moq\*)

### Naming tests

- Give your test methods descriptive names that convey their purpose, including the name of the method being tested,
  scenario under which it's being tested, and the expected behavior when the scenario is invoked.
- Follow the `MethodUnderTestName_Scenario_ExpectedBehavior` naming pattern

### Structuring tests

- Follow the **AAA** (_Arange-Act-Assert_) pattern, which makes the code simple and readable.
  - **Arrange** - set up test environment, instantiate unit under test and/or mock dependencies.
  - **Act** - call the unit under test using the arranged environment.
  - **Assert** - check if the actual result equals expected result.
- Try to keep one assertion per test.

### TDD (Test-Driven-Development)

We encourage using the TDD methodology:

- Write a failing test
- Implement the functionality to make the test pass
- Refactor the code

## Code Formatting

We have code style and conventions defined in the [.editorconfig](.editorconfig) file. The conventions are based on
[Microsoft's Code-style rule options](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options).

To comply with the code style defined in [.editorconfig](.editorconfig), use `dotnet format` locally before pushing
code.

To enforce running `dotnet format` on commit, apply the following steps, to hook up `dotnet format` so that it
automatically formats the code in the files included in your commit.

1. Replace the content of `.git/hooks/pre-commit.sample` with the following:

   ```
   #!/bin/sh
   FILES=$(git diff --cached --name-only --diff-filter=ACM "*.cs")
   if [ -n "$FILES" ]
   then
      dotnet format --no-restore --verbosity n --include $FILES
      echo "$FILES" | xargs git add
   fi
   ```

2. Change the name of `.git/hooks/pre-commit.sample` to `.git/hooks/pre-commit`.

## Git Conventions

### Commit Messages
- Write meaningful and concise commit messages.
- Use the imperative mood in your commit messages (e.g., "Add feature," "Fix bug," "Update documentation").

### Squashing Commits
- Before merging feature branches into the main branch, squash related commits into a single commit to maintain a clean and readable history.

### Pull Request

- Avoid committing directly to the main branch; prefer submitting changes through pull requests.
- Before merging a pull request into the main branch, squash related commits into a single commit to maintain a clean and readable history.
- Use pull request template, to ensure that all contributors provide essential information about the changes made in
a pull request. This information is crucial for reviewers to understand the background behind the changes.
  ```markdown
  # Pull Request Title
  
  ## Description
  
  <!-- Briefly describe the changes introduced by this pull request. -->
  
  ## Changes Made
  
  <!-- List the main changes made in this pull request. -->
  
  ## Related Ticket or Issue
  
  - Fixes #ISSUE_NUMBER <!-- Replace ISSUE_NUMBER with the actual number of the GitHub issue or ticket. -->
  - Closes #PROJECT_NAME/ISSUE_NUMBER <!-- Replace PROJECT_NAME with the name of your GitHub project. -->
  
  ## Checklist
  
  - [ ] Code adheres to the coding conventions
  - [ ] Unit tests are added for new code
  - [ ] Existing unit tests are updated if needed
  - [ ] Code follows the principles of Clean Architecture
  - [ ] Code formatting complies with the `.editorconfig` file
  - [ ] Pull request template is filled out
  ```

## Clean Code Principles

Follow Clean Code principles, to ensure that your dcode is well-crafted, clear, readable, and a joy to work with.

- Use meaningful and descriptive names - a name should express itention, it should tell why the code exists, what it
  does and how it is used.
  - Class name should be a noun or noun phrase, in singular: `User`, `Course` (not `Users`, `Courses`);
  - Function/method name should have a verb or verb phrase: `getUser`, `createCourse`, `changePassword`;
  - Use pronounceable and searchable names. Don't use random abbreviations;
  - Pick one word per concept: `getUsers`, `getCourses` and `getContent` instead of `getUsers`, `fetchCourses`
    and `retreiveContent`;
  - Avoid encoding - don't append prefixes or data types: `UsernameString`, `UserData`, `UserClass`;
- Avoid magic numbers - use named constants instead of hard-coded values: `totalPrice -= totalPrice * DISCOUNT_RATE;`
  instead of `totalPrice -= totalPrice * 0.2`
- Use comments sparingly - if your code requires a comment, consider renaming and/or refactoring it, to be
  self-explanatory.
- Follow _Single Responsibility Principle_ - one function should do one thing
- Follow _DRY_ principle (_Don't Repeat Yourself_) - avoid duplicating code, make it reusable.
- Refactor continuously - you don't need a ticket to do refactoring or tackle tech debt. Do it all the time.

## C# Language Guidelines

Follow common C# code conventions:

- Utilize modern language features and C# versions whenever possible.
- Avoid obsolete or outdated language constructs.
- Only catch exceptions that can be properly handled; avoid catching generic exceptions.
- Use specific exception types to provide meaningful error messages.
- Use LINQ queries and methods for collection manipulation to improve code readability.
- Use the language keywords for data types instead of the runtime types, e.g. `string` instead of `System.String`, or
  `int` instead of `System.Int32`.
- Use implicit typing for local variables when the type of the variable is obvious from the right side of the
  assignment, e.g. `var user = new User();` instead of `User user = new User();`.
- Use string interpolation to concatenate short strings, e.g. `$"{user.FirstName}, {user.LastName}";`.
- Use meaningful names for query variables in LINQ, e.g. `users.Where(user => user.Name == "John")` instead of
  `users.Where(x => x.Name === "John")`
- For more detailed overview, refer
  [Common C# code conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).
