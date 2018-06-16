# Part 4 response

The principles that were applied throughout the project was DRY ("Don't Repeat Yourself") alongside with YAGNI ("You aren't going to need it").  DRY is used to minimize the spread of error.  YAGNI is used to keep the code simple, which also helps to minimize the spread of error.  Parts of the SOLID priciples were used as well...particularly the Single Responsibility Principle and the Dependency Inversion Principle.  When these principles are applied, it allows for the code to be de-coupled and it makes unit testing more ideal.

If the program became more complex, then some of the SQLite data-access logic that is residing in the EmployeeController would be separated into its own component (i.e. a repository), which would later on go through Dependency Injection to be used by the EmployeeController.
