# EmployeePaySlip
This is an implementation of MYOB Coding test applications

# RUN
This is an aspnet core web application created in Visual Studio 2017 Community. 
1. Download the zip package.
2. Unzip in your computer and open the project by using VS
3. Build and run.

# Assumptions
1. The input file should be utf-8 format.
2. The input file should have 5 columns, otherwise app will give a error mint.
3. The payment period is month. I will change the format of the payment_start_date in a real project. It would be in 2 fields by divide into start_date and end_date. And should with year.
4. The result should be stored in a database. But I just store it in file.
5. The tax rate does not change. Actrually, it should change each year. In a really system, should match the tax rate by year.
