-- create new adpq database

-- add user
CREATE USER adpq WITH
  LOGIN
  PASSWORD 'adpq2adpq' 
  SUPERUSER
  INHERIT
  NOCREATEDB
  NOCREATEROLE
  NOREPLICATION;

-- create db 
CREATE DATABASE adpq2adpq
  WITH 
  OWNER = adpq
  ENCODING = 'UTF8'
  LC_COLLATE = 'C.UTF-8'
  LC_CTYPE = 'C.UTF-8'
  TABLESPACE = pg_default
  CONNECTION LIMIT = -1;


 \connect adpq2adpq
-- create sequences
  
CREATE SEQUENCE public."User_UserId_seq"
    INCREMENT 1
    START 41
    MINVALUE 1
    MAXVALUE 9223372036854775807
    CACHE 1;

ALTER SEQUENCE public."User_UserId_seq"
    OWNER TO adpq;
    
CREATE SEQUENCE public."UserNotification_UserNotificationId_seq"
    INCREMENT 1
    START 53
    MINVALUE 1
    MAXVALUE 9223372036854775807
    CACHE 1;

ALTER SEQUENCE public."UserNotification_UserNotificationId_seq"
    OWNER TO adpq;
    
CREATE SEQUENCE public."Notification_NotificationId_seq"
    INCREMENT 1
    START 17
    MINVALUE 1
    MAXVALUE 9223372036854775807
    CACHE 1;

ALTER SEQUENCE public."Notification_NotificationId_seq"
    OWNER TO adpq;

CREATE SEQUENCE public."NotificationType_NotificationTypeId_seq"
    INCREMENT 1
    START 2
    MINVALUE 1
    MAXVALUE 9223372036854775807
    CACHE 1;

ALTER SEQUENCE public."NotificationType_NotificationTypeId_seq"
    OWNER TO adpq;
    
-- create tables

CREATE TABLE public."User"
(
    "UserId" integer NOT NULL DEFAULT nextval('"User_UserId_seq"'::regclass),
    "FirstName" character varying(100) COLLATE pg_catalog."default",
    "LastName" character varying(100) COLLATE pg_catalog."default",
    "Address1" character varying(250) COLLATE pg_catalog."default",
    "Address2" character varying(250) COLLATE pg_catalog."default",
    "City" character varying(100) COLLATE pg_catalog."default",
    "State" character varying(50) COLLATE pg_catalog."default",
    "Zipcode" character varying(25) COLLATE pg_catalog."default",
    "IsAdmin" boolean NOT NULL,
    "Password" character varying(100) COLLATE pg_catalog."default",
    "Latitude" double precision,
    "Longitude" double precision,
    "Email" character varying(100) COLLATE pg_catalog."default",
    "PasswordSalt" character varying(100) COLLATE pg_catalog."default",
    "IsEmailNotification" boolean NOT NULL DEFAULT false,
    "IsSms" boolean NOT NULL DEFAULT true,
    "Phone" character varying(50) COLLATE pg_catalog."default",
    "CreatedOn" date,
    "CreatedBy" integer,
    "UpdatedOn" date,
    "UpdatedBy" integer,
    CONSTRAINT "User_pkey" PRIMARY KEY ("UserId")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."User"
    OWNER to adpq;
    
CREATE TABLE public."NotificationType"
(
    "NotificationTypeId" integer NOT NULL DEFAULT nextval('"NotificationType_NotificationTypeId_seq"'::regclass),
    "Name" character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT "NotificationType_pkey" PRIMARY KEY ("NotificationTypeId")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."NotificationType"
    OWNER to adpq;
    
CREATE TABLE public."Notification"
(
    "NotificationId" integer NOT NULL DEFAULT nextval('"Notification_NotificationId_seq"'::regclass),
    "NotificationTypeId" integer NOT NULL,
    "Address1" character varying(100) COLLATE pg_catalog."default",
    "Address2" character varying(100) COLLATE pg_catalog."default",
    "City" character varying(100) COLLATE pg_catalog."default",
    "State" character varying(50) COLLATE pg_catalog."default",
    "Zipcode" character varying(50) COLLATE pg_catalog."default",
    "RadiusMiles" integer,
    "EmailSubject" character varying(250) COLLATE pg_catalog."default",
    "EmailMessage" character varying(5000) COLLATE pg_catalog."default",
    "SmsMessage" character varying(450) COLLATE pg_catalog."default",
    "CreatedOn" date,
    "CreatedBy" integer,
    "UpdatedOn" date,
    "UpdatedBy" integer,
    CONSTRAINT "Notification_pkey" PRIMARY KEY ("NotificationId"),
    CONSTRAINT fk_notification_notificationtype FOREIGN KEY ("NotificationTypeId")
        REFERENCES public."NotificationType" ("NotificationTypeId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Notification"
    OWNER to adpq;

-- Index: fki_fk_notification_notificationtype

-- DROP INDEX public.fki_fk_notification_notificationtype;

CREATE INDEX fki_fk_notification_notificationtype
    ON public."Notification" USING btree
    ("NotificationTypeId")
    TABLESPACE pg_default;

CREATE TABLE public."UserNotification"
(
    "UserNotificationId" integer NOT NULL DEFAULT nextval('"UserNotification_UserNotificationId_seq"'::regclass),
    "UserId" integer NOT NULL,
    "NotificationId" integer NOT NULL,
    "IsEmailSent" boolean NOT NULL DEFAULT false,
    "IsSmsSent" boolean NOT NULL DEFAULT false,
    "NotificationDate" date NOT NULL DEFAULT now(),
    "Result" character varying(500) COLLATE pg_catalog."default",
    CONSTRAINT "UserNotification_pkey" PRIMARY KEY ("UserNotificationId"),
    CONSTRAINT "fk_notification_userNotification" FOREIGN KEY ("NotificationId")
        REFERENCES public."Notification" ("NotificationId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "fk_user_userNotification" FOREIGN KEY ("UserId")
        REFERENCES public."User" ("UserId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."UserNotification"
    OWNER to adpq;

-- Index: fki_fk_notification_userNotification

-- DROP INDEX public."fki_fk_notification_userNotification";

CREATE INDEX "fki_fk_notification_userNotification"
    ON public."UserNotification" USING btree
    ("NotificationId")
    TABLESPACE pg_default;

-- Index: fki_fk_user_userNotification

-- DROP INDEX public."fki_fk_user_userNotification";

CREATE INDEX "fki_fk_user_userNotification"
    ON public."UserNotification" USING btree
    ("UserId")
    TABLESPACE pg_default;


-- insert notification type lookup

INSERT INTO public."NotificationType" VALUES (1, 'Blast');
INSERT INTO public."NotificationType" VALUES (2, 'Regional');

-- insert admin user

INSERT INTO public."User" VALUES (36, 'Admin', 'Admin', '1000 main st', NULL, 'Sacramento', 'CA', '95821', true, '8Mg4JPu6/VRkW+7+mqEgddRbXf0vUGzcbrUVMoaYnnQ=', 38.6547351999999975, -121.442350899999994, 'admin@example.net', 'L1lrGc2f+U2rQ6kvwsOYOw==', false, true, NULL, NULL, NULL, NULL, NULL);

  
  