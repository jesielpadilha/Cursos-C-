declare 
    @customerId UNIQUEIDENTIFIER = NEWID();

IF NOT EXISTS(SELECT TOP 1 1 FROM Customer WHERE Id = @customerId)
BEGIN
    INSERT INTO Customer([Id]
        ,[FirstName]
        ,[LastName]
        ,[Document]
        ,[Email]
        ,[Phone]
    )
    VALUES (@customerId, 'Jesiel', 'Padilha', '12345678978', 'jesiel@gmail.com', '11 98484-4564')
END

IF EXISTS(SELECT TOP 1 1 FROM Customer WHERE Id = @customerId)
BEGIN
    INSERT INTO [Order] (Id, CustomerId, [Status]) VALUES
        (NEWID(), @customerId,  1)
END