# How to Contribute

Thank you for your interest in contributing to our project! We welcome all contributions that adhere to our guidelines. Here’s how you can get started:

## Fork the Repository

1. **Fork the repository**: Click the "Fork" button at the top right of the repository page on GitHub.
2. **Clone your fork**: Clone your forked repository to your local machine using:
    ```bash
    git clone https://github.com/your-username/html-attribute-dictionary.git
    ```
3. **Add upstream remote**: Add the original repository as a remote to keep your fork up to date:
    ```bash
    cd micro-utilities
    git remote add upstream https://github.com/friend-to-net-web-developers/html-attribute-dictionary.git
    ```

## Create a Branch

1. **Create a new branch** for your feature or bug fix:
    ```bash
    git checkout -b feature-or-bugfix-name
    ```

## Make Your Changes

1. **Make your changes**: Develop your feature or fix the bug.
2. **Follow the Code of Conduct**: Ensure that all interactions and contributions align with our [Code of Conduct](CODE_OF_CONDUCT.md).

## Commit Your Changes

1. **Commit your changes**: Write clear, concise commit messages:
    ```bash
    git add .
    git commit -m "Brief description of your changes"
    ```

## Push Your Changes

1. **Push your changes** to your forked repository:
    ```bash
    git push origin feature-or-bugfix-name
    ```

## Create a Pull Request

1. **Create a pull request**: Go to the original repository on GitHub and click the "New pull request" button.
2. **Select your branch**: Choose the branch you want to merge into (`dev`) and the branch you want to merge from (your feature branch).
   - **NOTE**: If you attempt to merge into anything other than `dev`, your PR will be rejected. 
3. **Provide details**: Fill in the pull request template with a description of your changes and any other relevant information.
4. **Submit the pull request**: Click "Create pull request."

## Code Review

1. **Participate in the review process**: Be responsive to any comments or feedback provided by the maintainers. Make any necessary changes and update your pull request.

## Stay Up to Date

1. **Sync your fork**: Keep your fork up to date with the latest changes from the original repository:
    ```bash
    git fetch upstream
    git checkout dev
    git merge upstream/dev
    git push origin dev
    ```

Thank you for contributing to our project!
