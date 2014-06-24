DbUnitTestDemo
==============

This is a demo project for my blog post about using SQL Server Compact Edition 4.0 in unit tests.

The unit tests will create a new database, from scratch using Entity Framework, and populate it 
before every test. This way code can be tested even if it depends on the database.
