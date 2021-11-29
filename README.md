Web Api calculating net salary by given gross salary and sertain country taxation rules. <br/>
# Endpoints:
* /api/Calculator/Calculate  <br/>
{ <br/>
&emsp;  "taxPayer": { <br/>
&emsp;&emsp;    "fullName": "string", <br/>
&emsp;&emsp;    "dateOfBirth": "string", <br/>
&emsp;&emsp;    "grossIncome": "decimal" <br/>
&emsp;  }, <br/>
&emsp;  "ssn": "string", <br/>
&emsp;  "charitySpent": "decimal" <br/>
} <br/>

# Property description:
* fullName - at least two words separated by space – allowed symbols letters and spaces only (mandatory)
* dateOfBirth - a valid date for date of birth (optional)
* grossIncome - a valid number for the amount for gross income (mandatory)
* ssn - a valid 5 to 10 digits number unique per tax payer (mandatory) (e.g. 12345, 6543297811)
* charitySpent - a valid number for the amount of annual charity spent (optional)
