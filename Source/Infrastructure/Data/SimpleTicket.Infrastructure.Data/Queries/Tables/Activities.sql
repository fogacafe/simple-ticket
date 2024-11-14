CREATE TABLE ACTIVITIES(
    Id uniqueidentifier NOT NULL,
    CreatedAt datetime NOT NULL,
    Note varchar(250) NOT NULL,
    TicketId uniqueidentifier NOT NULL,
    PRIMARY KEY(Id),
    CONSTRAINT FK_ACTIVITIES_TICKETS FOREIGN KEY (TicketId) REFERENCES TICKETS (Id)
)