# Ballast Lane Technical Test

Sample project for technical test for ASP NET developer for Ballast Lane.

# User Store Sample

## Overview

The solution is an API for managing books stock for the Bookstore Client, it must allow multiple users to create, modify and remove books from the store. The project structure aims for micro service application architecture deployment so it should have a segreggated authorization API for future application support.

## Tech Requirements

- User management should be handled by a separate API making the project capable of adding future modules without requiring to rewrite the authentication structure.
- Make al CRUD operations available and make it compatible with a vast array of clients.
- User authentication is required before querying and handling the data
- Storage should be handled by the own application to make the solution portable.
