create table users(
	id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	username varchar(20),
	description text,
	password varchar(16),
	is_delete BIT,
	type_id smallint,
	constraint users_pk primary key(id)
);

create table users_type(
	id smallint not null,
	name varchar(50),
	description varchar(20),
	constraint users_type_pk primary key(id)
);

alter table users add constraint users_fk foreign key(type_id) references users_type(id);
alter table users add constraint user_unique_key unique(username);

INSERT INTO users_type (id, name, description) VALUES (1, 'admin', 'Administrator');
INSERT INTO users_type (id, name, description) VALUES (2, 'customer', 'Customer');

INSERT INTO apartment_types (id, name, description) VALUES (1, 'Class1', '');
INSERT INTO apartment_types (id, name, description) VALUES (2, 'Class2', '');
INSERT INTO apartment_types (id, name, description) VALUES (3, 'Class3', '');
INSERT INTO apartment_types (id, name, description) VALUES (4, 'suite', '');

alter table apartment drop constraint apartment_fk_1;
alter table apartment drop column building_id;

INSERT INTO users (username, description, password, is_delete, type_id)
VALUES ('admin', 'description', 'admin@123', 0, 1);