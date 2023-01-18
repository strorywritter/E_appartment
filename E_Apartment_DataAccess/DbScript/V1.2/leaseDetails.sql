create table lease_details(
	id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	monthly_fee numeric(12,2),
	from_date date,
	to_date date,
	is_delete BIT,
	apartment_id UNIQUEIDENTIFIER,
	occupier_id UNIQUEIDENTIFIER,
	constraint lease_details_pk primary key(id)
);


create table lease_extension(
	id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	from_date date,
	to_date date,
	is_delete BIT,
	lease_details_id UNIQUEIDENTIFIER,
	constraint lease_extension_pk primary key(id)
);
------------------------------------------------------------------------------------------------------------------------
alter table lease_details add constraint lease_details_fk foreign key(apartment_id) references apartment(id);
alter table lease_details add constraint lease_details_fk1 foreign key(occupier_id) references occupier_detail(id);
alter table lease_extension add constraint lease_extension_fk foreign key(lease_details_id) references lease_details(id);

INSERT INTO apartment_status (id, name, description) VALUES (1, 'Available', '');
INSERT INTO apartment_status (id, name, description) VALUES (2, 'Occupied', '');
INSERT INTO apartment_status (id, name, description) VALUES (3, 'Unavailable', '');

alter table lease_details add lease_status_id smallint;

create table lease_statuses(
	id smallint not null,
	name varchar(50),
	description varchar(20),
	constraint lease_statuses_pk primary key(id)
);
------------------------------------------------------------------------------------------------------------------------
alter table lease_details add constraint lease_details_fk2 foreign key(lease_status_id) references lease_statuses(id);

INSERT INTO lease_statuses (id, name, description) VALUES (1, 'Approved', '');
INSERT INTO lease_statuses (id, name, description) VALUES (2, 'Not Approved', '');

-------------------------------------------------------------------------------------------------------------------------
alter table lease_extension add lease_status_id smallint;

alter table lease_extension add constraint lease_extension_fk2 foreign key(lease_status_id) references lease_statuses(id);

