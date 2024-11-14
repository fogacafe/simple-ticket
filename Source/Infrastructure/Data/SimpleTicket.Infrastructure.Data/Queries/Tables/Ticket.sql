CREATE TABLE TICKETS (
    Id uniqueidentifier NOT NULL,
    Summary varchar(250) NOT NULL,
    Note NVARCHAR(1000) NULL,
    CreatedAt datetime NOT NULL,
    Deadline datetime NULL,
    FinishedAt datetime NULL,
    CategoryId uniqueidentifier NULL,
    ResponsibleUsername varchar(100) NULL,
    CreatorUsername varchar(100) NOT NULL,
    Priority varchar(20),
    Status varchar(20),
    PRIMARY KEY(Id)
)

CREATE NONCLUSTERED INDEX IX_CREATEDAT_STATUS
ON TICKETS (Status, CreatedAt, ResponsibleUsername);