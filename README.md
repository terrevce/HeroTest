Dev Swagger: https://localhost:<port>/swagger/index.html

Live Demo: TBD

Create a DB somehwere on your machine in local sql server with the name "Sample" and run these scripts to get the tables and initial data you'll need on that db.

You may modify the connection string in app settings to connect to your local db, just make sure your app is able to create and access these two tables below if you change the db name and modify the conn string.


```
CREATE TABLE [dbo].[Heroes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Alias] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL,
	[BrandId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Heroes] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Heroes] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Heroes] ADD  DEFAULT (getutcdate()) FOR [UpdatedOn]
GO

ALTER TABLE [dbo].[Heroes] ADD  DEFAULT ((1)) FOR [BrandId]
GO



CREATE TABLE [dbo].[Brand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Brand] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Brand] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO

ALTER TABLE [dbo].[Brand] ADD  DEFAULT (getutcdate()) FOR [UpdatedOn]
GO


INSERT INTO [Brand]
(Name)
VALUES ('DC'),('Marvel')

INSERT INTO [Heroes]
(Name,Alias,BrandID)
VALUES('Superman','Clark Kent', (SELECT id FROM brand WHERE name = 'dc'))
```


Task is to modify the angular app and .net code to show a page that lists all the heroes on the home page on a table. Also there will be a column to "Delete" the hero which will flag the IsActive column on the Heroes table as false.

Use the HeroesController and have an endpoint that returns a list of heroes and what brand name they are tied to and display on home page of angular app using an angular service

Next create a new page in angular app with route add-hero that has a form that lets user create a new hero in the db.  The Hero "Name" and "Alias" and "Brand" are all required fields and should show an error if user tries to submit with any of those fields missing.  Create a new endpoint on HeroesController for the angular service to call to complete this task.

You may create any view models, classes, interfaces, etc that you may need in angular or .net to complete the two tasks.

Do not worry about validating duplicate hero names on add hero page.
