#!/bin/bash

read -p "Enter the context name (default: AppDbContext): " context_name

# Set default values if not provided
: ${context_name:="AppDbContext"}

echo "Updating database for context '$context_name'..."

dotnet ef database update --project src/Infrastructure/ --startup-project src/WebApi/ --context $context_name

if [ $? -ne 0 ]; then
  echo "Failed to update database for context '$context_name'."
  exit 1
else
  echo "Database updated successfully for context '$context_name'."
fi