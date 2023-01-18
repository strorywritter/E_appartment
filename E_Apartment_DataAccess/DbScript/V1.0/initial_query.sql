create table apartment(
	id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	code varchar(20),
	description text,
	flow_no varchar(10),
	status_id smallint,
	is_delete BIT,
	building_id UNIQUEIDENTIFIER,
	type_id smallint,
	constraint apartment_pk primary key(id)
);

create table building(
	id uniqueidentifier not null default newid(),
	code varchar(20),
	description text,
	address varchar(255),
	is_delete BIT,
	constraint building_pk primary key(id)
);

create table occupier_detail(
	id uniqueidentifier not null default newid(),
	code varchar(20),
	name varchar(100),
	alternate_address varchar(250),
	contact_no varchar(12),
	nic_or_passport_no varchar(20),
	is_include_servant BIT,
	apartment_id uniqueidentifier,
	is_delete BIT,
	constraint occupier_details_pk primary key(id)
);

create table apartment_status(
	id smallint not null,
	name varchar(50),
	description varchar(20),
	constraint apartment_status_pk primary key(id)
);

create table apartment_types(
	id smallint not null,
	name varchar(50),
	description varchar(20),
	constraint apartment_type_pk primary key(id)
);

--add primry keys and foreign keys 

alter table apartment add constraint apartment_fk_1 foreign key(building_id) references building(id);
alter table apartment add constraint apartment_fk_2 foreign key(status_id) references apartment_status(id);
alter table apartment add constraint apartment_fk_3 foreign key(type_id) references apartment_types(id);

alter table occupier_detail add constraint occupier_detail_fk foreign key(apartment_id) references apartment(id);

alter table building add name varchar(20);
