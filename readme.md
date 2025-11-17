# GitHub User Activity

A C# console application that fetches and displays recent activity for any GitHub user using the GitHub API.

## Project Overview

This project is based on the [GitHub User Activity challenge](https://roadmap.sh/projects/github-user-activity) from roadmap.sh.

## Features

- Fetches recent public events for any GitHub user
- Displays user-friendly activity messages including:
  - Push events with commit counts
  - Issue creation
  - Repository stars
  - Fork events
  - Pull request actions
  - Branch/repository creation and deletion
  - And more...

## Prerequisites

- .NET 8.0 SDK or later

## Usage

1. Run the application:
   ```bash
   dotnet run
   ```

2. Enter a GitHub username when prompted

3. View the user's recent activity in a formatted list

## Example Output

```
Enter a GitHub username:
DaltonLuis

Output:
- Pushed 3 commits to DaltonLuis/GithubUserActivite
- Opened a new issue in DaltonLuis/TaskTrackerCLI
- Starred kamranahmedse/developer-roadmap
- Created branch in DaltonLuis/BillingSystem
- Forked example/repository
```

## Implementation Details

- Uses `HttpClient` to make requests to the GitHub API
- Deserializes JSON responses using `System.Text.Json`
- Handles various GitHub event types with pattern matching
- Case-insensitive username support

## GitHub API

This application uses the GitHub Events API endpoint:
```
https://api.github.com/users/{username}/events
```

No authentication is required, but unauthenticated requests are limited to 60 requests per hour per IP address.