#!/bin/bash

# Ask the user for the migration name and Context name

read -p "Enter the migration name: " migration_name
read -p "Enter the context name (default: AppDbContext): " context_name

# Set default values if not provided, but require migration name to be provided
: ${context_name:="AppDbContext"}

if [ -z "$migration_name" ]; then
  echo "Migration name is required."
  exit 1
fi

echo "Adding migration '$migration_name' for context '$context_name'..."

dotnet ef migrations add --project src/Infrastructure/ --startup-project src/WebApi/ --context $context_name $migration_name

if [ $? -ne 0 ]; then
  echo "Failed to add migration '$migration_name' for context '$context_name'."
  exit 1
else
  echo "Migration '$migration_name' added successfully for context '$context_name'."
fi