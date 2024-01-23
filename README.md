# MiniApi

![Er-Schemat]([Skärmbild 2024-01-21 171639.png](https://github.com/Robinkalkan/MiniApi/blob/master/Sk%C3%A4rmbild%202024-01-21%20171639.png)https://github.com/Robinkalkan/MiniApi/blob/master/Sk%C3%A4rmbild%202024-01-21%20171639.png)


Förklaring för mina anrop:

"/" - MapGet("/"):
=

Rotvägen för applikationen som returnerar en enkel hälsning, "Hello Christoffer!". En testpunkt för att säkerställa applikationens svar.

"/people" - MapGet("/people"):
=

en Get-endpoint Hämtar alla personer från databasen tillsammans med deras intressen och intresselänkar. Resultatet skrivs ut som en JSON-representation av personerna och deras information.

"/person/{id}/interests" - MapGet("/person/{id}/interests"):
=

En GET-endpoint för att hämta intressen för en specifik person baserat på personens id. Den inkluderar även information om intresselänkar för varje intresse.

"/person/{id}/links" - MapGet("/person/{id}/links"):
=

En GET-endpoint för att hämta länkar för en person baserat på personens id.

"/new/person" - MapPost("/new/person"):
=

En POST-endpoint för att skapa en ny person i databasen. Förväntar sig en POST-förfrågan med personuppgifter i request bodyn och lägger till dessa i databasen. Returnerar en 201 Created-statuskod och personens information vid framgång.

"/person/{personId}/interests" - MapPost("/person/{personId}/interests"):
=

En POST-endpoint för att koppla en person till ett nytt intresse. Förväntar sig en POST-förfrågan med ett intresse i request bodyn och skapar en koppling mellan personen och intresset i databasen. Returnerar en 201 Created-statuskod och information vid framgång.

"/person/{personId}/interest/{interestId}/link" - MapPost("/person/{personId}/interest/{interestId}/link"):
=

En POST-endpoint för att lägga till en ny länk för ett intresse och en person. Förväntar sig en POST-förfrågan med URL:en i request bodyn och skapar en ny intresselänk i databasen. Returnerar en 200 OK-statuskod vid framgång och hanterar felaktig JSON-struktur genom att returnera en 400 Bad Request i sådant fall.


![Uml-Diagramet]([[Skärmbild 2024-01-21 171639.png](https://github.com/Robinkalkan/MiniApi/blob/master/Sk%C3%A4rmbild%202024-01-21%20171639.png)https://github.com/Robinkalkan/MiniApi/blob/master/Sk%C3%A4rmbild%202024-01-21%20171639.png](https://github.com/Robinkalkan/MiniApi/blob/master/Uml-Diagram.png))









