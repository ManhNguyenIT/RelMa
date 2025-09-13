--
-- PostgreSQL database cluster dump
--

-- Started on 2025-08-04 14:28:40

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

CREATE ROLE admin;
ALTER ROLE admin WITH NOSUPERUSER INHERIT NOCREATEROLE CREATEDB LOGIN NOREPLICATION NOBYPASSRLS;
CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS;

--
-- User Configurations
--








--
-- Databases
--

--
-- Database "template1" dump
--

\connect template1

--
-- PostgreSQL database dump
--

-- Dumped from database version 17.5
-- Dumped by pg_dump version 17.5

-- Started on 2025-08-04 14:28:41

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- Completed on 2025-08-04 14:28:41

--
-- PostgreSQL database dump complete
--

--
-- Database "keycloak" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 17.5
-- Dumped by pg_dump version 17.5

-- Started on 2025-08-04 14:28:41

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4246 (class 1262 OID 16384)
-- Name: keycloak; Type: DATABASE; Schema: -; Owner: admin
--

CREATE DATABASE keycloak WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.UTF-8';


ALTER DATABASE keycloak OWNER TO admin;

\connect keycloak

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: admin
--

-- *not* creating schema, since initdb creates it


ALTER SCHEMA public OWNER TO admin;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 251 (class 1259 OID 17018)
-- Name: admin_event_entity; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.admin_event_entity (
    id character varying(36) NOT NULL,
    admin_event_time bigint,
    realm_id character varying(255),
    operation_type character varying(255),
    auth_realm_id character varying(255),
    auth_client_id character varying(255),
    auth_user_id character varying(255),
    ip_address character varying(255),
    resource_path character varying(2550),
    representation text,
    error character varying(255),
    resource_type character varying(64),
    details_json text
);


ALTER TABLE public.admin_event_entity OWNER TO admin;

--
-- TOC entry 278 (class 1259 OID 17461)
-- Name: associated_policy; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.associated_policy (
    policy_id character varying(36) NOT NULL,
    associated_policy_id character varying(36) NOT NULL
);


ALTER TABLE public.associated_policy OWNER TO admin;

--
-- TOC entry 254 (class 1259 OID 17033)
-- Name: authentication_execution; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.authentication_execution (
    id character varying(36) NOT NULL,
    alias character varying(255),
    authenticator character varying(36),
    realm_id character varying(36),
    flow_id character varying(36),
    requirement integer,
    priority integer,
    authenticator_flow boolean DEFAULT false NOT NULL,
    auth_flow_id character varying(36),
    auth_config character varying(36)
);


ALTER TABLE public.authentication_execution OWNER TO admin;

--
-- TOC entry 253 (class 1259 OID 17028)
-- Name: authentication_flow; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.authentication_flow (
    id character varying(36) NOT NULL,
    alias character varying(255),
    description character varying(255),
    realm_id character varying(36),
    provider_id character varying(36) DEFAULT 'basic-flow'::character varying NOT NULL,
    top_level boolean DEFAULT false NOT NULL,
    built_in boolean DEFAULT false NOT NULL
);


ALTER TABLE public.authentication_flow OWNER TO admin;

--
-- TOC entry 252 (class 1259 OID 17023)
-- Name: authenticator_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.authenticator_config (
    id character varying(36) NOT NULL,
    alias character varying(255),
    realm_id character varying(36)
);


ALTER TABLE public.authenticator_config OWNER TO admin;

--
-- TOC entry 255 (class 1259 OID 17038)
-- Name: authenticator_config_entry; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.authenticator_config_entry (
    authenticator_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);


ALTER TABLE public.authenticator_config_entry OWNER TO admin;

--
-- TOC entry 279 (class 1259 OID 17476)
-- Name: broker_link; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.broker_link (
    identity_provider character varying(255) NOT NULL,
    storage_provider_id character varying(255),
    realm_id character varying(36) NOT NULL,
    broker_user_id character varying(255),
    broker_username character varying(255),
    token text,
    user_id character varying(255) NOT NULL
);


ALTER TABLE public.broker_link OWNER TO admin;

--
-- TOC entry 219 (class 1259 OID 16399)
-- Name: client; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.client (
    id character varying(36) NOT NULL,
    enabled boolean DEFAULT false NOT NULL,
    full_scope_allowed boolean DEFAULT false NOT NULL,
    client_id character varying(255),
    not_before integer,
    public_client boolean DEFAULT false NOT NULL,
    secret character varying(255),
    base_url character varying(255),
    bearer_only boolean DEFAULT false NOT NULL,
    management_url character varying(255),
    surrogate_auth_required boolean DEFAULT false NOT NULL,
    realm_id character varying(36),
    protocol character varying(255),
    node_rereg_timeout integer DEFAULT 0,
    frontchannel_logout boolean DEFAULT false NOT NULL,
    consent_required boolean DEFAULT false NOT NULL,
    name character varying(255),
    service_accounts_enabled boolean DEFAULT false NOT NULL,
    client_authenticator_type character varying(255),
    root_url character varying(255),
    description character varying(255),
    registration_token character varying(255),
    standard_flow_enabled boolean DEFAULT true NOT NULL,
    implicit_flow_enabled boolean DEFAULT false NOT NULL,
    direct_access_grants_enabled boolean DEFAULT false NOT NULL,
    always_display_in_console boolean DEFAULT false NOT NULL
);


ALTER TABLE public.client OWNER TO admin;

--
-- TOC entry 238 (class 1259 OID 16757)
-- Name: client_attributes; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.client_attributes (
    client_id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    value text
);


ALTER TABLE public.client_attributes OWNER TO admin;

--
-- TOC entry 290 (class 1259 OID 17725)
-- Name: client_auth_flow_bindings; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.client_auth_flow_bindings (
    client_id character varying(36) NOT NULL,
    flow_id character varying(36),
    binding_name character varying(255) NOT NULL
);


ALTER TABLE public.client_auth_flow_bindings OWNER TO admin;

--
-- TOC entry 289 (class 1259 OID 17600)
-- Name: client_initial_access; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.client_initial_access (
    id character varying(36) NOT NULL,
    realm_id character varying(36) NOT NULL,
    "timestamp" integer,
    expiration integer,
    count integer,
    remaining_count integer
);


ALTER TABLE public.client_initial_access OWNER TO admin;

--
-- TOC entry 239 (class 1259 OID 16767)
-- Name: client_node_registrations; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.client_node_registrations (
    client_id character varying(36) NOT NULL,
    value integer,
    name character varying(255) NOT NULL
);


ALTER TABLE public.client_node_registrations OWNER TO admin;

--
-- TOC entry 267 (class 1259 OID 17266)
-- Name: client_scope; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.client_scope (
    id character varying(36) NOT NULL,
    name character varying(255),
    realm_id character varying(36),
    description character varying(255),
    protocol character varying(255)
);


ALTER TABLE public.client_scope OWNER TO admin;

--
-- TOC entry 268 (class 1259 OID 17280)
-- Name: client_scope_attributes; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.client_scope_attributes (
    scope_id character varying(36) NOT NULL,
    value character varying(2048),
    name character varying(255) NOT NULL
);


ALTER TABLE public.client_scope_attributes OWNER TO admin;

--
-- TOC entry 291 (class 1259 OID 17766)
-- Name: client_scope_client; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.client_scope_client (
    client_id character varying(255) NOT NULL,
    scope_id character varying(255) NOT NULL,
    default_scope boolean DEFAULT false NOT NULL
);


ALTER TABLE public.client_scope_client OWNER TO admin;

--
-- TOC entry 269 (class 1259 OID 17285)
-- Name: client_scope_role_mapping; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.client_scope_role_mapping (
    scope_id character varying(36) NOT NULL,
    role_id character varying(36) NOT NULL
);


ALTER TABLE public.client_scope_role_mapping OWNER TO admin;

--
-- TOC entry 287 (class 1259 OID 17521)
-- Name: component; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.component (
    id character varying(36) NOT NULL,
    name character varying(255),
    parent_id character varying(36),
    provider_id character varying(36),
    provider_type character varying(255),
    realm_id character varying(36),
    sub_type character varying(255)
);


ALTER TABLE public.component OWNER TO admin;

--
-- TOC entry 286 (class 1259 OID 17516)
-- Name: component_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.component_config (
    id character varying(36) NOT NULL,
    component_id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    value text
);


ALTER TABLE public.component_config OWNER TO admin;

--
-- TOC entry 220 (class 1259 OID 16418)
-- Name: composite_role; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.composite_role (
    composite character varying(36) NOT NULL,
    child_role character varying(36) NOT NULL
);


ALTER TABLE public.composite_role OWNER TO admin;

--
-- TOC entry 221 (class 1259 OID 16421)
-- Name: credential; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.credential (
    id character varying(36) NOT NULL,
    salt bytea,
    type character varying(255),
    user_id character varying(36),
    created_date bigint,
    user_label character varying(255),
    secret_data text,
    credential_data text,
    priority integer,
    version integer DEFAULT 0
);


ALTER TABLE public.credential OWNER TO admin;

--
-- TOC entry 218 (class 1259 OID 16391)
-- Name: databasechangelog; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.databasechangelog (
    id character varying(255) NOT NULL,
    author character varying(255) NOT NULL,
    filename character varying(255) NOT NULL,
    dateexecuted timestamp without time zone NOT NULL,
    orderexecuted integer NOT NULL,
    exectype character varying(10) NOT NULL,
    md5sum character varying(35),
    description character varying(255),
    comments character varying(255),
    tag character varying(255),
    liquibase character varying(20),
    contexts character varying(255),
    labels character varying(255),
    deployment_id character varying(10)
);


ALTER TABLE public.databasechangelog OWNER TO admin;

--
-- TOC entry 217 (class 1259 OID 16386)
-- Name: databasechangeloglock; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.databasechangeloglock (
    id integer NOT NULL,
    locked boolean NOT NULL,
    lockgranted timestamp without time zone,
    lockedby character varying(255)
);


ALTER TABLE public.databasechangeloglock OWNER TO admin;

--
-- TOC entry 292 (class 1259 OID 17782)
-- Name: default_client_scope; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.default_client_scope (
    realm_id character varying(36) NOT NULL,
    scope_id character varying(36) NOT NULL,
    default_scope boolean DEFAULT false NOT NULL
);


ALTER TABLE public.default_client_scope OWNER TO admin;

--
-- TOC entry 222 (class 1259 OID 16426)
-- Name: event_entity; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.event_entity (
    id character varying(36) NOT NULL,
    client_id character varying(255),
    details_json character varying(2550),
    error character varying(255),
    ip_address character varying(255),
    realm_id character varying(255),
    session_id character varying(255),
    event_time bigint,
    type character varying(255),
    user_id character varying(255),
    details_json_long_value text
);


ALTER TABLE public.event_entity OWNER TO admin;

--
-- TOC entry 280 (class 1259 OID 17481)
-- Name: fed_user_attribute; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.fed_user_attribute (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36),
    value character varying(2024),
    long_value_hash bytea,
    long_value_hash_lower_case bytea,
    long_value text
);


ALTER TABLE public.fed_user_attribute OWNER TO admin;

--
-- TOC entry 281 (class 1259 OID 17486)
-- Name: fed_user_consent; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.fed_user_consent (
    id character varying(36) NOT NULL,
    client_id character varying(255),
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36),
    created_date bigint,
    last_updated_date bigint,
    client_storage_provider character varying(36),
    external_client_id character varying(255)
);


ALTER TABLE public.fed_user_consent OWNER TO admin;

--
-- TOC entry 294 (class 1259 OID 17808)
-- Name: fed_user_consent_cl_scope; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.fed_user_consent_cl_scope (
    user_consent_id character varying(36) NOT NULL,
    scope_id character varying(36) NOT NULL
);


ALTER TABLE public.fed_user_consent_cl_scope OWNER TO admin;

--
-- TOC entry 282 (class 1259 OID 17495)
-- Name: fed_user_credential; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.fed_user_credential (
    id character varying(36) NOT NULL,
    salt bytea,
    type character varying(255),
    created_date bigint,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36),
    user_label character varying(255),
    secret_data text,
    credential_data text,
    priority integer
);


ALTER TABLE public.fed_user_credential OWNER TO admin;

--
-- TOC entry 283 (class 1259 OID 17504)
-- Name: fed_user_group_membership; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.fed_user_group_membership (
    group_id character varying(36) NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36)
);


ALTER TABLE public.fed_user_group_membership OWNER TO admin;

--
-- TOC entry 284 (class 1259 OID 17507)
-- Name: fed_user_required_action; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.fed_user_required_action (
    required_action character varying(255) DEFAULT ' '::character varying NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36)
);


ALTER TABLE public.fed_user_required_action OWNER TO admin;

--
-- TOC entry 285 (class 1259 OID 17513)
-- Name: fed_user_role_mapping; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.fed_user_role_mapping (
    role_id character varying(36) NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    storage_provider_id character varying(36)
);


ALTER TABLE public.fed_user_role_mapping OWNER TO admin;

--
-- TOC entry 242 (class 1259 OID 16803)
-- Name: federated_identity; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.federated_identity (
    identity_provider character varying(255) NOT NULL,
    realm_id character varying(36),
    federated_user_id character varying(255),
    federated_username character varying(255),
    token text,
    user_id character varying(36) NOT NULL
);


ALTER TABLE public.federated_identity OWNER TO admin;

--
-- TOC entry 288 (class 1259 OID 17578)
-- Name: federated_user; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.federated_user (
    id character varying(255) NOT NULL,
    storage_provider_id character varying(255),
    realm_id character varying(36) NOT NULL
);


ALTER TABLE public.federated_user OWNER TO admin;

--
-- TOC entry 264 (class 1259 OID 17205)
-- Name: group_attribute; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.group_attribute (
    id character varying(36) DEFAULT 'sybase-needs-something-here'::character varying NOT NULL,
    name character varying(255) NOT NULL,
    value character varying(255),
    group_id character varying(36) NOT NULL
);


ALTER TABLE public.group_attribute OWNER TO admin;

--
-- TOC entry 263 (class 1259 OID 17202)
-- Name: group_role_mapping; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.group_role_mapping (
    role_id character varying(36) NOT NULL,
    group_id character varying(36) NOT NULL
);


ALTER TABLE public.group_role_mapping OWNER TO admin;

--
-- TOC entry 243 (class 1259 OID 16808)
-- Name: identity_provider; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.identity_provider (
    internal_id character varying(36) NOT NULL,
    enabled boolean DEFAULT false NOT NULL,
    provider_alias character varying(255),
    provider_id character varying(255),
    store_token boolean DEFAULT false NOT NULL,
    authenticate_by_default boolean DEFAULT false NOT NULL,
    realm_id character varying(36),
    add_token_role boolean DEFAULT true NOT NULL,
    trust_email boolean DEFAULT false NOT NULL,
    first_broker_login_flow_id character varying(36),
    post_broker_login_flow_id character varying(36),
    provider_display_name character varying(255),
    link_only boolean DEFAULT false NOT NULL,
    organization_id character varying(255),
    hide_on_login boolean DEFAULT false
);


ALTER TABLE public.identity_provider OWNER TO admin;

--
-- TOC entry 244 (class 1259 OID 16817)
-- Name: identity_provider_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.identity_provider_config (
    identity_provider_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);


ALTER TABLE public.identity_provider_config OWNER TO admin;

--
-- TOC entry 248 (class 1259 OID 16921)
-- Name: identity_provider_mapper; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.identity_provider_mapper (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    idp_alias character varying(255) NOT NULL,
    idp_mapper_name character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL
);


ALTER TABLE public.identity_provider_mapper OWNER TO admin;

--
-- TOC entry 249 (class 1259 OID 16926)
-- Name: idp_mapper_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.idp_mapper_config (
    idp_mapper_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);


ALTER TABLE public.idp_mapper_config OWNER TO admin;

--
-- TOC entry 303 (class 1259 OID 18007)
-- Name: jgroups_ping; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.jgroups_ping (
    address character varying(200) NOT NULL,
    name character varying(200),
    cluster_name character varying(200) NOT NULL,
    ip character varying(200) NOT NULL,
    coord boolean
);


ALTER TABLE public.jgroups_ping OWNER TO admin;

--
-- TOC entry 262 (class 1259 OID 17199)
-- Name: keycloak_group; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.keycloak_group (
    id character varying(36) NOT NULL,
    name character varying(255),
    parent_group character varying(36) NOT NULL,
    realm_id character varying(36),
    type integer DEFAULT 0 NOT NULL,
    description character varying(255)
);


ALTER TABLE public.keycloak_group OWNER TO admin;

--
-- TOC entry 223 (class 1259 OID 16434)
-- Name: keycloak_role; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.keycloak_role (
    id character varying(36) NOT NULL,
    client_realm_constraint character varying(255),
    client_role boolean DEFAULT false NOT NULL,
    description character varying(255),
    name character varying(255),
    realm_id character varying(255),
    client character varying(36),
    realm character varying(36)
);


ALTER TABLE public.keycloak_role OWNER TO admin;

--
-- TOC entry 247 (class 1259 OID 16918)
-- Name: migration_model; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.migration_model (
    id character varying(36) NOT NULL,
    version character varying(36),
    update_time bigint DEFAULT 0 NOT NULL
);


ALTER TABLE public.migration_model OWNER TO admin;

--
-- TOC entry 261 (class 1259 OID 17190)
-- Name: offline_client_session; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.offline_client_session (
    user_session_id character varying(36) NOT NULL,
    client_id character varying(255) NOT NULL,
    offline_flag character varying(4) NOT NULL,
    "timestamp" integer,
    data text,
    client_storage_provider character varying(36) DEFAULT 'local'::character varying NOT NULL,
    external_client_id character varying(255) DEFAULT 'local'::character varying NOT NULL,
    version integer DEFAULT 0
);


ALTER TABLE public.offline_client_session OWNER TO admin;

--
-- TOC entry 260 (class 1259 OID 17185)
-- Name: offline_user_session; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.offline_user_session (
    user_session_id character varying(36) NOT NULL,
    user_id character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    created_on integer NOT NULL,
    offline_flag character varying(4) NOT NULL,
    data text,
    last_session_refresh integer DEFAULT 0 NOT NULL,
    broker_session_id character varying(1024),
    version integer DEFAULT 0
);


ALTER TABLE public.offline_user_session OWNER TO admin;

--
-- TOC entry 300 (class 1259 OID 17970)
-- Name: org; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.org (
    id character varying(255) NOT NULL,
    enabled boolean NOT NULL,
    realm_id character varying(255) NOT NULL,
    group_id character varying(255) NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(4000),
    alias character varying(255) NOT NULL,
    redirect_url character varying(2048)
);


ALTER TABLE public.org OWNER TO admin;

--
-- TOC entry 301 (class 1259 OID 17981)
-- Name: org_domain; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.org_domain (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    verified boolean NOT NULL,
    org_id character varying(255) NOT NULL
);


ALTER TABLE public.org_domain OWNER TO admin;

--
-- TOC entry 274 (class 1259 OID 17404)
-- Name: policy_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.policy_config (
    policy_id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    value text
);


ALTER TABLE public.policy_config OWNER TO admin;

--
-- TOC entry 240 (class 1259 OID 16792)
-- Name: protocol_mapper; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.protocol_mapper (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    protocol character varying(255) NOT NULL,
    protocol_mapper_name character varying(255) NOT NULL,
    client_id character varying(36),
    client_scope_id character varying(36)
);


ALTER TABLE public.protocol_mapper OWNER TO admin;

--
-- TOC entry 241 (class 1259 OID 16798)
-- Name: protocol_mapper_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.protocol_mapper_config (
    protocol_mapper_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);


ALTER TABLE public.protocol_mapper_config OWNER TO admin;

--
-- TOC entry 224 (class 1259 OID 16440)
-- Name: realm; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.realm (
    id character varying(36) NOT NULL,
    access_code_lifespan integer,
    user_action_lifespan integer,
    access_token_lifespan integer,
    account_theme character varying(255),
    admin_theme character varying(255),
    email_theme character varying(255),
    enabled boolean DEFAULT false NOT NULL,
    events_enabled boolean DEFAULT false NOT NULL,
    events_expiration bigint,
    login_theme character varying(255),
    name character varying(255),
    not_before integer,
    password_policy character varying(2550),
    registration_allowed boolean DEFAULT false NOT NULL,
    remember_me boolean DEFAULT false NOT NULL,
    reset_password_allowed boolean DEFAULT false NOT NULL,
    social boolean DEFAULT false NOT NULL,
    ssl_required character varying(255),
    sso_idle_timeout integer,
    sso_max_lifespan integer,
    update_profile_on_soc_login boolean DEFAULT false NOT NULL,
    verify_email boolean DEFAULT false NOT NULL,
    master_admin_client character varying(36),
    login_lifespan integer,
    internationalization_enabled boolean DEFAULT false NOT NULL,
    default_locale character varying(255),
    reg_email_as_username boolean DEFAULT false NOT NULL,
    admin_events_enabled boolean DEFAULT false NOT NULL,
    admin_events_details_enabled boolean DEFAULT false NOT NULL,
    edit_username_allowed boolean DEFAULT false NOT NULL,
    otp_policy_counter integer DEFAULT 0,
    otp_policy_window integer DEFAULT 1,
    otp_policy_period integer DEFAULT 30,
    otp_policy_digits integer DEFAULT 6,
    otp_policy_alg character varying(36) DEFAULT 'HmacSHA1'::character varying,
    otp_policy_type character varying(36) DEFAULT 'totp'::character varying,
    browser_flow character varying(36),
    registration_flow character varying(36),
    direct_grant_flow character varying(36),
    reset_credentials_flow character varying(36),
    client_auth_flow character varying(36),
    offline_session_idle_timeout integer DEFAULT 0,
    revoke_refresh_token boolean DEFAULT false NOT NULL,
    access_token_life_implicit integer DEFAULT 0,
    login_with_email_allowed boolean DEFAULT true NOT NULL,
    duplicate_emails_allowed boolean DEFAULT false NOT NULL,
    docker_auth_flow character varying(36),
    refresh_token_max_reuse integer DEFAULT 0,
    allow_user_managed_access boolean DEFAULT false NOT NULL,
    sso_max_lifespan_remember_me integer DEFAULT 0 NOT NULL,
    sso_idle_timeout_remember_me integer DEFAULT 0 NOT NULL,
    default_role character varying(255)
);


ALTER TABLE public.realm OWNER TO admin;

--
-- TOC entry 225 (class 1259 OID 16457)
-- Name: realm_attribute; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.realm_attribute (
    name character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL,
    value text
);


ALTER TABLE public.realm_attribute OWNER TO admin;

--
-- TOC entry 266 (class 1259 OID 17214)
-- Name: realm_default_groups; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.realm_default_groups (
    realm_id character varying(36) NOT NULL,
    group_id character varying(36) NOT NULL
);


ALTER TABLE public.realm_default_groups OWNER TO admin;

--
-- TOC entry 246 (class 1259 OID 16910)
-- Name: realm_enabled_event_types; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.realm_enabled_event_types (
    realm_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);


ALTER TABLE public.realm_enabled_event_types OWNER TO admin;

--
-- TOC entry 226 (class 1259 OID 16465)
-- Name: realm_events_listeners; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.realm_events_listeners (
    realm_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);


ALTER TABLE public.realm_events_listeners OWNER TO admin;

--
-- TOC entry 299 (class 1259 OID 17916)
-- Name: realm_localizations; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.realm_localizations (
    realm_id character varying(255) NOT NULL,
    locale character varying(255) NOT NULL,
    texts text NOT NULL
);


ALTER TABLE public.realm_localizations OWNER TO admin;

--
-- TOC entry 227 (class 1259 OID 16468)
-- Name: realm_required_credential; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.realm_required_credential (
    type character varying(255) NOT NULL,
    form_label character varying(255),
    input boolean DEFAULT false NOT NULL,
    secret boolean DEFAULT false NOT NULL,
    realm_id character varying(36) NOT NULL
);


ALTER TABLE public.realm_required_credential OWNER TO admin;

--
-- TOC entry 228 (class 1259 OID 16475)
-- Name: realm_smtp_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.realm_smtp_config (
    realm_id character varying(36) NOT NULL,
    value character varying(255),
    name character varying(255) NOT NULL
);


ALTER TABLE public.realm_smtp_config OWNER TO admin;

--
-- TOC entry 245 (class 1259 OID 16826)
-- Name: realm_supported_locales; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.realm_supported_locales (
    realm_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);


ALTER TABLE public.realm_supported_locales OWNER TO admin;

--
-- TOC entry 229 (class 1259 OID 16485)
-- Name: redirect_uris; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.redirect_uris (
    client_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);


ALTER TABLE public.redirect_uris OWNER TO admin;

--
-- TOC entry 259 (class 1259 OID 17149)
-- Name: required_action_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.required_action_config (
    required_action_id character varying(36) NOT NULL,
    value text,
    name character varying(255) NOT NULL
);


ALTER TABLE public.required_action_config OWNER TO admin;

--
-- TOC entry 258 (class 1259 OID 17142)
-- Name: required_action_provider; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.required_action_provider (
    id character varying(36) NOT NULL,
    alias character varying(255),
    name character varying(255),
    realm_id character varying(36),
    enabled boolean DEFAULT false NOT NULL,
    default_action boolean DEFAULT false NOT NULL,
    provider_id character varying(255),
    priority integer
);


ALTER TABLE public.required_action_provider OWNER TO admin;

--
-- TOC entry 296 (class 1259 OID 17847)
-- Name: resource_attribute; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.resource_attribute (
    id character varying(36) DEFAULT 'sybase-needs-something-here'::character varying NOT NULL,
    name character varying(255) NOT NULL,
    value character varying(255),
    resource_id character varying(36) NOT NULL
);


ALTER TABLE public.resource_attribute OWNER TO admin;

--
-- TOC entry 276 (class 1259 OID 17431)
-- Name: resource_policy; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.resource_policy (
    resource_id character varying(36) NOT NULL,
    policy_id character varying(36) NOT NULL
);


ALTER TABLE public.resource_policy OWNER TO admin;

--
-- TOC entry 275 (class 1259 OID 17416)
-- Name: resource_scope; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.resource_scope (
    resource_id character varying(36) NOT NULL,
    scope_id character varying(36) NOT NULL
);


ALTER TABLE public.resource_scope OWNER TO admin;

--
-- TOC entry 270 (class 1259 OID 17354)
-- Name: resource_server; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.resource_server (
    id character varying(36) NOT NULL,
    allow_rs_remote_mgmt boolean DEFAULT false NOT NULL,
    policy_enforce_mode smallint NOT NULL,
    decision_strategy smallint DEFAULT 1 NOT NULL
);


ALTER TABLE public.resource_server OWNER TO admin;

--
-- TOC entry 295 (class 1259 OID 17823)
-- Name: resource_server_perm_ticket; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.resource_server_perm_ticket (
    id character varying(36) NOT NULL,
    owner character varying(255) NOT NULL,
    requester character varying(255) NOT NULL,
    created_timestamp bigint NOT NULL,
    granted_timestamp bigint,
    resource_id character varying(36) NOT NULL,
    scope_id character varying(36),
    resource_server_id character varying(36) NOT NULL,
    policy_id character varying(36)
);


ALTER TABLE public.resource_server_perm_ticket OWNER TO admin;

--
-- TOC entry 273 (class 1259 OID 17390)
-- Name: resource_server_policy; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.resource_server_policy (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(255),
    type character varying(255) NOT NULL,
    decision_strategy smallint,
    logic smallint,
    resource_server_id character varying(36) NOT NULL,
    owner character varying(255)
);


ALTER TABLE public.resource_server_policy OWNER TO admin;

--
-- TOC entry 271 (class 1259 OID 17362)
-- Name: resource_server_resource; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.resource_server_resource (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    type character varying(255),
    icon_uri character varying(255),
    owner character varying(255) NOT NULL,
    resource_server_id character varying(36) NOT NULL,
    owner_managed_access boolean DEFAULT false NOT NULL,
    display_name character varying(255)
);


ALTER TABLE public.resource_server_resource OWNER TO admin;

--
-- TOC entry 272 (class 1259 OID 17376)
-- Name: resource_server_scope; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.resource_server_scope (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    icon_uri character varying(255),
    resource_server_id character varying(36) NOT NULL,
    display_name character varying(255)
);


ALTER TABLE public.resource_server_scope OWNER TO admin;

--
-- TOC entry 297 (class 1259 OID 17865)
-- Name: resource_uris; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.resource_uris (
    resource_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);


ALTER TABLE public.resource_uris OWNER TO admin;

--
-- TOC entry 302 (class 1259 OID 17998)
-- Name: revoked_token; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.revoked_token (
    id character varying(255) NOT NULL,
    expire bigint NOT NULL
);


ALTER TABLE public.revoked_token OWNER TO admin;

--
-- TOC entry 298 (class 1259 OID 17875)
-- Name: role_attribute; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.role_attribute (
    id character varying(36) NOT NULL,
    role_id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    value character varying(255)
);


ALTER TABLE public.role_attribute OWNER TO admin;

--
-- TOC entry 230 (class 1259 OID 16488)
-- Name: scope_mapping; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.scope_mapping (
    client_id character varying(36) NOT NULL,
    role_id character varying(36) NOT NULL
);


ALTER TABLE public.scope_mapping OWNER TO admin;

--
-- TOC entry 277 (class 1259 OID 17446)
-- Name: scope_policy; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.scope_policy (
    scope_id character varying(36) NOT NULL,
    policy_id character varying(36) NOT NULL
);


ALTER TABLE public.scope_policy OWNER TO admin;

--
-- TOC entry 304 (class 1259 OID 18014)
-- Name: server_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.server_config (
    server_config_key character varying(255) NOT NULL,
    value text NOT NULL,
    version integer DEFAULT 0
);


ALTER TABLE public.server_config OWNER TO admin;

--
-- TOC entry 231 (class 1259 OID 16494)
-- Name: user_attribute; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_attribute (
    name character varying(255) NOT NULL,
    value character varying(255),
    user_id character varying(36) NOT NULL,
    id character varying(36) DEFAULT 'sybase-needs-something-here'::character varying NOT NULL,
    long_value_hash bytea,
    long_value_hash_lower_case bytea,
    long_value text
);


ALTER TABLE public.user_attribute OWNER TO admin;

--
-- TOC entry 250 (class 1259 OID 16931)
-- Name: user_consent; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_consent (
    id character varying(36) NOT NULL,
    client_id character varying(255),
    user_id character varying(36) NOT NULL,
    created_date bigint,
    last_updated_date bigint,
    client_storage_provider character varying(36),
    external_client_id character varying(255)
);


ALTER TABLE public.user_consent OWNER TO admin;

--
-- TOC entry 293 (class 1259 OID 17798)
-- Name: user_consent_client_scope; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_consent_client_scope (
    user_consent_id character varying(36) NOT NULL,
    scope_id character varying(36) NOT NULL
);


ALTER TABLE public.user_consent_client_scope OWNER TO admin;

--
-- TOC entry 232 (class 1259 OID 16499)
-- Name: user_entity; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_entity (
    id character varying(36) NOT NULL,
    email character varying(255),
    email_constraint character varying(255),
    email_verified boolean DEFAULT false NOT NULL,
    enabled boolean DEFAULT false NOT NULL,
    federation_link character varying(255),
    first_name character varying(255),
    last_name character varying(255),
    realm_id character varying(255),
    username character varying(255),
    created_timestamp bigint,
    service_account_client_link character varying(255),
    not_before integer DEFAULT 0 NOT NULL
);


ALTER TABLE public.user_entity OWNER TO admin;

--
-- TOC entry 233 (class 1259 OID 16507)
-- Name: user_federation_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_federation_config (
    user_federation_provider_id character varying(36) NOT NULL,
    value character varying(255),
    name character varying(255) NOT NULL
);


ALTER TABLE public.user_federation_config OWNER TO admin;

--
-- TOC entry 256 (class 1259 OID 17043)
-- Name: user_federation_mapper; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_federation_mapper (
    id character varying(36) NOT NULL,
    name character varying(255) NOT NULL,
    federation_provider_id character varying(36) NOT NULL,
    federation_mapper_type character varying(255) NOT NULL,
    realm_id character varying(36) NOT NULL
);


ALTER TABLE public.user_federation_mapper OWNER TO admin;

--
-- TOC entry 257 (class 1259 OID 17048)
-- Name: user_federation_mapper_config; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_federation_mapper_config (
    user_federation_mapper_id character varying(36) NOT NULL,
    value character varying(255),
    name character varying(255) NOT NULL
);


ALTER TABLE public.user_federation_mapper_config OWNER TO admin;

--
-- TOC entry 234 (class 1259 OID 16512)
-- Name: user_federation_provider; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_federation_provider (
    id character varying(36) NOT NULL,
    changed_sync_period integer,
    display_name character varying(255),
    full_sync_period integer,
    last_sync integer,
    priority integer,
    provider_name character varying(255),
    realm_id character varying(36)
);


ALTER TABLE public.user_federation_provider OWNER TO admin;

--
-- TOC entry 265 (class 1259 OID 17211)
-- Name: user_group_membership; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_group_membership (
    group_id character varying(36) NOT NULL,
    user_id character varying(36) NOT NULL,
    membership_type character varying(255) NOT NULL
);


ALTER TABLE public.user_group_membership OWNER TO admin;

--
-- TOC entry 235 (class 1259 OID 16517)
-- Name: user_required_action; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_required_action (
    user_id character varying(36) NOT NULL,
    required_action character varying(255) DEFAULT ' '::character varying NOT NULL
);


ALTER TABLE public.user_required_action OWNER TO admin;

--
-- TOC entry 236 (class 1259 OID 16520)
-- Name: user_role_mapping; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_role_mapping (
    role_id character varying(255) NOT NULL,
    user_id character varying(36) NOT NULL
);


ALTER TABLE public.user_role_mapping OWNER TO admin;

--
-- TOC entry 237 (class 1259 OID 16534)
-- Name: web_origins; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.web_origins (
    client_id character varying(36) NOT NULL,
    value character varying(255) NOT NULL
);


ALTER TABLE public.web_origins OWNER TO admin;

--
-- TOC entry 4187 (class 0 OID 17018)
-- Dependencies: 251
-- Data for Name: admin_event_entity; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.admin_event_entity (id, admin_event_time, realm_id, operation_type, auth_realm_id, auth_client_id, auth_user_id, ip_address, resource_path, representation, error, resource_type, details_json) FROM stdin;
\.


--
-- TOC entry 4214 (class 0 OID 17461)
-- Dependencies: 278
-- Data for Name: associated_policy; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.associated_policy (policy_id, associated_policy_id) FROM stdin;
\.


--
-- TOC entry 4190 (class 0 OID 17033)
-- Dependencies: 254
-- Data for Name: authentication_execution; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.authentication_execution (id, alias, authenticator, realm_id, flow_id, requirement, priority, authenticator_flow, auth_flow_id, auth_config) FROM stdin;
610cda26-5611-4f03-9a60-fe8fe51664d6	\N	auth-cookie	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	dda11955-b6a0-49b6-b6f9-23f996dedcb3	2	10	f	\N	\N
ca575acd-48a5-4f51-8e05-eba8b597dd11	\N	auth-spnego	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	dda11955-b6a0-49b6-b6f9-23f996dedcb3	3	20	f	\N	\N
4e35abee-e611-4137-a9b4-f5d5d1c71192	\N	identity-provider-redirector	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	dda11955-b6a0-49b6-b6f9-23f996dedcb3	2	25	f	\N	\N
d7da79d0-1e55-4b5a-8f9b-cc501a2d9520	\N	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	dda11955-b6a0-49b6-b6f9-23f996dedcb3	2	30	t	25ca2d36-a605-4d4b-a50f-3237e5ec4d07	\N
a94d4406-c19b-4fa4-941b-d200f2fe0055	\N	auth-username-password-form	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	25ca2d36-a605-4d4b-a50f-3237e5ec4d07	0	10	f	\N	\N
a7b09799-3cda-4f08-801b-10e98e0ab2e1	\N	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	25ca2d36-a605-4d4b-a50f-3237e5ec4d07	1	20	t	eabd7f94-a463-4c7d-806f-5df3653a4935	\N
2770a828-6536-4dde-b46c-e5a27819389b	\N	conditional-user-configured	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	eabd7f94-a463-4c7d-806f-5df3653a4935	0	10	f	\N	\N
829f221d-6be4-406c-9e9b-d1cfc5473051	\N	auth-otp-form	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	eabd7f94-a463-4c7d-806f-5df3653a4935	2	20	f	\N	\N
8101abe3-2eae-4670-b865-c70ba2b893fd	\N	webauthn-authenticator	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	eabd7f94-a463-4c7d-806f-5df3653a4935	3	30	f	\N	\N
7f2d5854-2423-4a0e-90fa-821fecd48436	\N	auth-recovery-authn-code-form	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	eabd7f94-a463-4c7d-806f-5df3653a4935	3	40	f	\N	\N
44000ee4-1730-48fa-84d2-4bdccec6b2ed	\N	direct-grant-validate-username	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	56510d42-ae29-4292-a6cd-885d225b1d6b	0	10	f	\N	\N
89814419-6c30-4eda-a2a0-2481f85c6927	\N	direct-grant-validate-password	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	56510d42-ae29-4292-a6cd-885d225b1d6b	0	20	f	\N	\N
480898a9-5b9f-448c-89d1-f1f5f8357c45	\N	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	56510d42-ae29-4292-a6cd-885d225b1d6b	1	30	t	f5728ebc-6435-4607-ae3f-c49181eae268	\N
0db24d87-8d65-4c78-8220-014615aac230	\N	conditional-user-configured	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f5728ebc-6435-4607-ae3f-c49181eae268	0	10	f	\N	\N
21a08bbc-915b-4f51-8429-f8f10b940e22	\N	direct-grant-validate-otp	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f5728ebc-6435-4607-ae3f-c49181eae268	0	20	f	\N	\N
f48baaad-e5be-4356-b12b-183d9b748ed3	\N	registration-page-form	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	2c452b8d-00d1-4938-afc5-26322ecdd8af	0	10	t	446f7b2c-1a5d-4cce-9d82-04184f6dd612	\N
ba8d0d7b-46f9-4903-aff2-0237444e6039	\N	registration-user-creation	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	446f7b2c-1a5d-4cce-9d82-04184f6dd612	0	20	f	\N	\N
bdc7c380-4214-4bbd-b78f-d1677b2f4ad2	\N	registration-password-action	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	446f7b2c-1a5d-4cce-9d82-04184f6dd612	0	50	f	\N	\N
81aa2c1b-06c8-4b85-a72d-5d9b3038fd5f	\N	registration-recaptcha-action	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	446f7b2c-1a5d-4cce-9d82-04184f6dd612	3	60	f	\N	\N
74c939e7-6630-488f-b7fe-f36d6d817779	\N	registration-terms-and-conditions	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	446f7b2c-1a5d-4cce-9d82-04184f6dd612	3	70	f	\N	\N
40e078a4-5425-4d26-8c31-557436f0a32d	\N	reset-credentials-choose-user	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	575ec34d-c697-4594-ac2c-74141f7c1c17	0	10	f	\N	\N
940a0bdd-4fc2-4cb8-91dd-74deef2d621c	\N	reset-credential-email	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	575ec34d-c697-4594-ac2c-74141f7c1c17	0	20	f	\N	\N
042e8d20-562b-4bce-b7f1-3d70a90b68ed	\N	reset-password	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	575ec34d-c697-4594-ac2c-74141f7c1c17	0	30	f	\N	\N
e869352d-e6d6-4d62-8832-7362a1540cd6	\N	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	575ec34d-c697-4594-ac2c-74141f7c1c17	1	40	t	77e308fa-186d-4374-954b-5f374f292566	\N
b7399eb2-96ad-44a0-b03d-6f8669eba903	\N	conditional-user-configured	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	77e308fa-186d-4374-954b-5f374f292566	0	10	f	\N	\N
312ff6c8-1168-47cc-9543-061088688623	\N	reset-otp	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	77e308fa-186d-4374-954b-5f374f292566	0	20	f	\N	\N
e83fa558-c8ba-4e1c-87c3-d67c1a1c8271	\N	client-secret	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	7a10855f-ee13-4278-9b63-02382a570b8c	2	10	f	\N	\N
258c10ab-4322-4da3-bb73-15cb79d3e022	\N	client-jwt	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	7a10855f-ee13-4278-9b63-02382a570b8c	2	20	f	\N	\N
eaae3603-0658-468a-ba15-aa110de639ef	\N	client-secret-jwt	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	7a10855f-ee13-4278-9b63-02382a570b8c	2	30	f	\N	\N
c65d28bb-fc7a-4d6a-85b0-96671eb27d6d	\N	client-x509	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	7a10855f-ee13-4278-9b63-02382a570b8c	2	40	f	\N	\N
0a367d0c-9342-4fcd-a6d3-ea1567c44ab0	\N	idp-review-profile	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f3a74744-0746-4945-8a7b-7205957d8bf0	0	10	f	\N	61f16194-6d0f-4c72-b41c-b4b3f15cd00a
9f036078-110e-4bfb-b490-89ba87d30686	\N	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f3a74744-0746-4945-8a7b-7205957d8bf0	0	20	t	75887fce-a98c-4d2c-ae3e-401295a0e198	\N
26c00cd4-9e07-4abb-a9e3-986eca6f6f41	\N	idp-create-user-if-unique	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	75887fce-a98c-4d2c-ae3e-401295a0e198	2	10	f	\N	53e50f44-1f14-4b89-931d-1d21acf9f854
5ebe2143-a85d-464b-958c-61fb8f60ae88	\N	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	75887fce-a98c-4d2c-ae3e-401295a0e198	2	20	t	84922938-7a6d-4bc2-b500-ba2ff2cf3b71	\N
f4b4ed4f-d63c-4310-be20-f4758ecbc342	\N	idp-confirm-link	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	84922938-7a6d-4bc2-b500-ba2ff2cf3b71	0	10	f	\N	\N
13f8ed92-3f87-4bf4-9e3d-ed66479a39d5	\N	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	84922938-7a6d-4bc2-b500-ba2ff2cf3b71	0	20	t	9d442587-2fdc-4f79-9ee1-07e41cf19b38	\N
4ecf0213-bfd6-42e0-a1fb-5025bb570178	\N	idp-email-verification	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	9d442587-2fdc-4f79-9ee1-07e41cf19b38	2	10	f	\N	\N
05a65db0-f4de-478c-9d5c-95d1db692ed2	\N	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	9d442587-2fdc-4f79-9ee1-07e41cf19b38	2	20	t	ad84d605-2718-4f16-b756-1867d87c0813	\N
1d7c9ed2-ebab-4814-b128-d1b9abc7e9b4	\N	idp-username-password-form	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	ad84d605-2718-4f16-b756-1867d87c0813	0	10	f	\N	\N
e9fea63a-2e31-4527-87df-33b750aef0a5	\N	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	ad84d605-2718-4f16-b756-1867d87c0813	1	20	t	95c1b736-84f2-4d23-b1bf-6cc1a90134c4	\N
90514041-d951-4d0b-9afd-09748a030cdf	\N	conditional-user-configured	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	95c1b736-84f2-4d23-b1bf-6cc1a90134c4	0	10	f	\N	\N
a98c4c85-4499-431a-aeb5-6ceb56698d81	\N	auth-otp-form	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	95c1b736-84f2-4d23-b1bf-6cc1a90134c4	2	20	f	\N	\N
3f95a40a-13cc-4a4c-9ef0-a3e33de3823d	\N	webauthn-authenticator	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	95c1b736-84f2-4d23-b1bf-6cc1a90134c4	3	30	f	\N	\N
9bdbfd8d-6452-4ad6-baa4-afecd69f98a8	\N	auth-recovery-authn-code-form	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	95c1b736-84f2-4d23-b1bf-6cc1a90134c4	3	40	f	\N	\N
d0dcfd20-431a-4331-92ba-2d864d729d1a	\N	http-basic-authenticator	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	c7398fa2-31c7-4c0e-862e-3e4e41b97565	0	10	f	\N	\N
0195921a-8f1b-46b1-8a70-998a11b802a6	\N	docker-http-basic-authenticator	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	56295342-9379-42c8-bd0f-d8b1733918c2	0	10	f	\N	\N
2778266c-62a8-4de4-af9a-0b8edb2fd153	\N	auth-cookie	1bcb83bb-6f51-410d-972e-8077c4359429	d5e7e1fa-6bc5-41d5-8c2c-15d4e50a4104	2	10	f	\N	\N
abc8cc16-064c-4152-9f86-865782cdc2c1	\N	auth-spnego	1bcb83bb-6f51-410d-972e-8077c4359429	d5e7e1fa-6bc5-41d5-8c2c-15d4e50a4104	3	20	f	\N	\N
7779610f-1fe8-482e-ae0d-ff6fed2b6d64	\N	identity-provider-redirector	1bcb83bb-6f51-410d-972e-8077c4359429	d5e7e1fa-6bc5-41d5-8c2c-15d4e50a4104	2	25	f	\N	\N
98dc7a43-036d-4ea7-983e-ac7220b89802	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	d5e7e1fa-6bc5-41d5-8c2c-15d4e50a4104	2	30	t	021b99f2-8708-4c1d-b5e4-25b3b517fafc	\N
5aeb7681-fe9a-4a1d-ba45-6277202c1a53	\N	auth-username-password-form	1bcb83bb-6f51-410d-972e-8077c4359429	021b99f2-8708-4c1d-b5e4-25b3b517fafc	0	10	f	\N	\N
5ad7e1f3-1b8e-4153-af48-cd3b813820b4	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	021b99f2-8708-4c1d-b5e4-25b3b517fafc	1	20	t	3f9844a7-a712-4fe7-a58b-41aa268ae008	\N
7232720c-9ec1-4202-8e8e-7cf8f4010f79	\N	conditional-user-configured	1bcb83bb-6f51-410d-972e-8077c4359429	3f9844a7-a712-4fe7-a58b-41aa268ae008	0	10	f	\N	\N
fd39ba2a-414f-4f16-9edd-d3ee8bffda6b	\N	auth-otp-form	1bcb83bb-6f51-410d-972e-8077c4359429	3f9844a7-a712-4fe7-a58b-41aa268ae008	2	20	f	\N	\N
4a0d4006-6114-4598-9de3-576d40708b70	\N	webauthn-authenticator	1bcb83bb-6f51-410d-972e-8077c4359429	3f9844a7-a712-4fe7-a58b-41aa268ae008	3	30	f	\N	\N
65c2ae8e-a891-424a-a13b-45d255affa8e	\N	auth-recovery-authn-code-form	1bcb83bb-6f51-410d-972e-8077c4359429	3f9844a7-a712-4fe7-a58b-41aa268ae008	3	40	f	\N	\N
0c4a3c55-6632-418e-b0a4-e955d804a454	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	d5e7e1fa-6bc5-41d5-8c2c-15d4e50a4104	2	26	t	98625a43-6640-45a6-8a8d-aa5baf951b22	\N
131c72ef-fa64-49fc-a036-c5e4381ceecb	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	98625a43-6640-45a6-8a8d-aa5baf951b22	1	10	t	61087d10-5e09-45f3-aa77-d2b39b7c7020	\N
1e6f14b4-04c0-49ff-9602-0fe554ebd0e2	\N	conditional-user-configured	1bcb83bb-6f51-410d-972e-8077c4359429	61087d10-5e09-45f3-aa77-d2b39b7c7020	0	10	f	\N	\N
3ceb3031-831c-4a12-9722-119721a8fdb5	\N	organization	1bcb83bb-6f51-410d-972e-8077c4359429	61087d10-5e09-45f3-aa77-d2b39b7c7020	2	20	f	\N	\N
f2308576-32e8-48c9-9281-b11b3f37c69d	\N	direct-grant-validate-username	1bcb83bb-6f51-410d-972e-8077c4359429	77386210-c959-4196-bab4-04f2fc58e97b	0	10	f	\N	\N
8ba4a4df-8e3e-43eb-9cad-e5a11d0ccc9c	\N	direct-grant-validate-password	1bcb83bb-6f51-410d-972e-8077c4359429	77386210-c959-4196-bab4-04f2fc58e97b	0	20	f	\N	\N
90230f76-45a0-4bcc-a77b-834ab470d93a	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	77386210-c959-4196-bab4-04f2fc58e97b	1	30	t	cf239976-6748-4983-b40d-e69538cf9a3a	\N
119eefb6-60e1-4076-9d9b-e1c195cd8b73	\N	conditional-user-configured	1bcb83bb-6f51-410d-972e-8077c4359429	cf239976-6748-4983-b40d-e69538cf9a3a	0	10	f	\N	\N
0928e8c4-4729-48b1-bd98-bf6e38167e36	\N	direct-grant-validate-otp	1bcb83bb-6f51-410d-972e-8077c4359429	cf239976-6748-4983-b40d-e69538cf9a3a	0	20	f	\N	\N
cd0e39c8-ab67-4566-b2f9-ebb0b8a0321e	\N	registration-page-form	1bcb83bb-6f51-410d-972e-8077c4359429	bbe75f57-eee1-413e-9f97-e57f2f9eaea8	0	10	t	6171addd-fc15-4480-a2af-51188561f428	\N
fa5f9c93-29ce-4d98-9fe5-c42a210dbcd7	\N	registration-user-creation	1bcb83bb-6f51-410d-972e-8077c4359429	6171addd-fc15-4480-a2af-51188561f428	0	20	f	\N	\N
24ae51ad-473d-4c70-87e6-96098d2aaf29	\N	registration-password-action	1bcb83bb-6f51-410d-972e-8077c4359429	6171addd-fc15-4480-a2af-51188561f428	0	50	f	\N	\N
df6e500c-1b59-47c5-ac3f-321b2ec4b3e9	\N	registration-recaptcha-action	1bcb83bb-6f51-410d-972e-8077c4359429	6171addd-fc15-4480-a2af-51188561f428	3	60	f	\N	\N
48f8b1df-9ed0-41a2-ae38-dbe6f9a678b7	\N	registration-terms-and-conditions	1bcb83bb-6f51-410d-972e-8077c4359429	6171addd-fc15-4480-a2af-51188561f428	3	70	f	\N	\N
b82e2962-5aea-477e-9c98-e4e3dc703ffe	\N	reset-credentials-choose-user	1bcb83bb-6f51-410d-972e-8077c4359429	64cd3b6e-e1d3-48b8-8411-22d6a072dee0	0	10	f	\N	\N
c4e9c654-f04a-483c-bd25-80a34db816be	\N	reset-credential-email	1bcb83bb-6f51-410d-972e-8077c4359429	64cd3b6e-e1d3-48b8-8411-22d6a072dee0	0	20	f	\N	\N
b5b05c15-1d62-42db-839f-9656e9e44af7	\N	reset-password	1bcb83bb-6f51-410d-972e-8077c4359429	64cd3b6e-e1d3-48b8-8411-22d6a072dee0	0	30	f	\N	\N
0fe5ac1d-5868-419e-9968-058a74265448	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	64cd3b6e-e1d3-48b8-8411-22d6a072dee0	1	40	t	becaa117-15e5-45e8-a1a3-21e9ed2386c7	\N
71cdb56b-af0d-4b2c-99ba-b2a2b09d88d5	\N	conditional-user-configured	1bcb83bb-6f51-410d-972e-8077c4359429	becaa117-15e5-45e8-a1a3-21e9ed2386c7	0	10	f	\N	\N
e91a61e4-a35e-428c-9df1-db67790c5801	\N	reset-otp	1bcb83bb-6f51-410d-972e-8077c4359429	becaa117-15e5-45e8-a1a3-21e9ed2386c7	0	20	f	\N	\N
7f060eec-7810-40c5-aedd-805d9cb739d6	\N	client-secret	1bcb83bb-6f51-410d-972e-8077c4359429	60674ea8-99c3-4ecb-9bf3-3c02d4ebc6b2	2	10	f	\N	\N
f8cb0c97-dbc8-4b56-ad0a-05a58b9cc123	\N	client-jwt	1bcb83bb-6f51-410d-972e-8077c4359429	60674ea8-99c3-4ecb-9bf3-3c02d4ebc6b2	2	20	f	\N	\N
1f542bdb-4f45-4714-86c5-5dd5d26438a2	\N	client-secret-jwt	1bcb83bb-6f51-410d-972e-8077c4359429	60674ea8-99c3-4ecb-9bf3-3c02d4ebc6b2	2	30	f	\N	\N
545ec095-adb6-493c-813d-7cefe7809942	\N	client-x509	1bcb83bb-6f51-410d-972e-8077c4359429	60674ea8-99c3-4ecb-9bf3-3c02d4ebc6b2	2	40	f	\N	\N
2d358076-5c47-4c5b-bf31-7f711aff6617	\N	idp-review-profile	1bcb83bb-6f51-410d-972e-8077c4359429	d79b5d89-c58c-4fbe-bd2b-a81b5ae758a9	0	10	f	\N	18676a29-cc38-4dca-b38e-77eaae0e716d
52d903d6-6b42-4820-8f8b-35fcfbec1bbd	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	d79b5d89-c58c-4fbe-bd2b-a81b5ae758a9	0	20	t	adffdc5c-03cd-4fec-bec4-176a2c6f31b4	\N
54d49d4c-eb42-4dba-995d-f5f8802e02d1	\N	idp-create-user-if-unique	1bcb83bb-6f51-410d-972e-8077c4359429	adffdc5c-03cd-4fec-bec4-176a2c6f31b4	2	10	f	\N	a32d048a-cb6b-4cf1-9121-c683ae58bacb
d4b4f840-81c1-4902-a2eb-608be27e1757	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	adffdc5c-03cd-4fec-bec4-176a2c6f31b4	2	20	t	800122c5-e595-4b18-9b47-2d2a6114c82e	\N
961c8e5a-2f71-48b9-ba95-556f76847480	\N	idp-confirm-link	1bcb83bb-6f51-410d-972e-8077c4359429	800122c5-e595-4b18-9b47-2d2a6114c82e	0	10	f	\N	\N
ca8909f5-12e6-42ca-ae93-f8a95d4b971d	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	800122c5-e595-4b18-9b47-2d2a6114c82e	0	20	t	240e0a30-5c0b-46d2-b79f-b1a766ab2c11	\N
cf2d0421-d09e-44ca-ae3d-367a1b18a65e	\N	idp-email-verification	1bcb83bb-6f51-410d-972e-8077c4359429	240e0a30-5c0b-46d2-b79f-b1a766ab2c11	2	10	f	\N	\N
a139cd59-9f12-413d-b1d6-7ce4e4b46ae0	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	240e0a30-5c0b-46d2-b79f-b1a766ab2c11	2	20	t	cce8d71e-170d-42ca-8eb2-00379e8d36e1	\N
a4207aa0-6db8-49dd-8511-d602fca847da	\N	idp-username-password-form	1bcb83bb-6f51-410d-972e-8077c4359429	cce8d71e-170d-42ca-8eb2-00379e8d36e1	0	10	f	\N	\N
503657ca-1ed5-4c8f-8db9-5577630811fd	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	cce8d71e-170d-42ca-8eb2-00379e8d36e1	1	20	t	65b58b02-4caf-4902-b4ee-32dd71bf9380	\N
d7135390-1e0c-4562-b54f-4600a049da72	\N	conditional-user-configured	1bcb83bb-6f51-410d-972e-8077c4359429	65b58b02-4caf-4902-b4ee-32dd71bf9380	0	10	f	\N	\N
8404f96c-2631-4197-afd9-ffaceadc4533	\N	auth-otp-form	1bcb83bb-6f51-410d-972e-8077c4359429	65b58b02-4caf-4902-b4ee-32dd71bf9380	2	20	f	\N	\N
de8aa438-0d59-4d5f-8379-aadc13339c16	\N	webauthn-authenticator	1bcb83bb-6f51-410d-972e-8077c4359429	65b58b02-4caf-4902-b4ee-32dd71bf9380	3	30	f	\N	\N
f8a1a45f-c81b-4d6c-8491-aaf986b55a44	\N	auth-recovery-authn-code-form	1bcb83bb-6f51-410d-972e-8077c4359429	65b58b02-4caf-4902-b4ee-32dd71bf9380	3	40	f	\N	\N
d354dfb0-6d73-47bf-954e-dd68a5073d96	\N	\N	1bcb83bb-6f51-410d-972e-8077c4359429	d79b5d89-c58c-4fbe-bd2b-a81b5ae758a9	1	50	t	9ed018dd-2093-4b46-b7de-ff1f8776a54b	\N
d96dbfbd-4459-4844-9720-91edd45fd581	\N	conditional-user-configured	1bcb83bb-6f51-410d-972e-8077c4359429	9ed018dd-2093-4b46-b7de-ff1f8776a54b	0	10	f	\N	\N
07d6da7a-3add-4820-b002-506861ffd204	\N	idp-add-organization-member	1bcb83bb-6f51-410d-972e-8077c4359429	9ed018dd-2093-4b46-b7de-ff1f8776a54b	0	20	f	\N	\N
b3910459-ec19-4b75-8dfa-fece50973291	\N	http-basic-authenticator	1bcb83bb-6f51-410d-972e-8077c4359429	2c68b3c3-c75e-4efe-a180-d859e50a321a	0	10	f	\N	\N
91161131-e855-4f36-9d2e-5326917ade07	\N	docker-http-basic-authenticator	1bcb83bb-6f51-410d-972e-8077c4359429	6854c68c-d750-4169-9f81-ed80da1dcc92	0	10	f	\N	\N
\.


--
-- TOC entry 4189 (class 0 OID 17028)
-- Dependencies: 253
-- Data for Name: authentication_flow; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.authentication_flow (id, alias, description, realm_id, provider_id, top_level, built_in) FROM stdin;
dda11955-b6a0-49b6-b6f9-23f996dedcb3	browser	Browser based authentication	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	t	t
25ca2d36-a605-4d4b-a50f-3237e5ec4d07	forms	Username, password, otp and other auth forms.	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	f	t
eabd7f94-a463-4c7d-806f-5df3653a4935	Browser - Conditional 2FA	Flow to determine if any 2FA is required for the authentication	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	f	t
56510d42-ae29-4292-a6cd-885d225b1d6b	direct grant	OpenID Connect Resource Owner Grant	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	t	t
f5728ebc-6435-4607-ae3f-c49181eae268	Direct Grant - Conditional OTP	Flow to determine if the OTP is required for the authentication	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	f	t
2c452b8d-00d1-4938-afc5-26322ecdd8af	registration	Registration flow	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	t	t
446f7b2c-1a5d-4cce-9d82-04184f6dd612	registration form	Registration form	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	form-flow	f	t
575ec34d-c697-4594-ac2c-74141f7c1c17	reset credentials	Reset credentials for a user if they forgot their password or something	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	t	t
77e308fa-186d-4374-954b-5f374f292566	Reset - Conditional OTP	Flow to determine if the OTP should be reset or not. Set to REQUIRED to force.	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	f	t
7a10855f-ee13-4278-9b63-02382a570b8c	clients	Base authentication for clients	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	client-flow	t	t
f3a74744-0746-4945-8a7b-7205957d8bf0	first broker login	Actions taken after first broker login with identity provider account, which is not yet linked to any Keycloak account	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	t	t
75887fce-a98c-4d2c-ae3e-401295a0e198	User creation or linking	Flow for the existing/non-existing user alternatives	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	f	t
84922938-7a6d-4bc2-b500-ba2ff2cf3b71	Handle Existing Account	Handle what to do if there is existing account with same email/username like authenticated identity provider	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	f	t
9d442587-2fdc-4f79-9ee1-07e41cf19b38	Account verification options	Method with which to verity the existing account	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	f	t
ad84d605-2718-4f16-b756-1867d87c0813	Verify Existing Account by Re-authentication	Reauthentication of existing account	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	f	t
95c1b736-84f2-4d23-b1bf-6cc1a90134c4	First broker login - Conditional 2FA	Flow to determine if any 2FA is required for the authentication	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	f	t
c7398fa2-31c7-4c0e-862e-3e4e41b97565	saml ecp	SAML ECP Profile Authentication Flow	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	t	t
56295342-9379-42c8-bd0f-d8b1733918c2	docker auth	Used by Docker clients to authenticate against the IDP	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	basic-flow	t	t
d5e7e1fa-6bc5-41d5-8c2c-15d4e50a4104	browser	Browser based authentication	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	t	t
021b99f2-8708-4c1d-b5e4-25b3b517fafc	forms	Username, password, otp and other auth forms.	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
3f9844a7-a712-4fe7-a58b-41aa268ae008	Browser - Conditional 2FA	Flow to determine if any 2FA is required for the authentication	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
98625a43-6640-45a6-8a8d-aa5baf951b22	Organization	\N	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
61087d10-5e09-45f3-aa77-d2b39b7c7020	Browser - Conditional Organization	Flow to determine if the organization identity-first login is to be used	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
77386210-c959-4196-bab4-04f2fc58e97b	direct grant	OpenID Connect Resource Owner Grant	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	t	t
cf239976-6748-4983-b40d-e69538cf9a3a	Direct Grant - Conditional OTP	Flow to determine if the OTP is required for the authentication	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
bbe75f57-eee1-413e-9f97-e57f2f9eaea8	registration	Registration flow	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	t	t
6171addd-fc15-4480-a2af-51188561f428	registration form	Registration form	1bcb83bb-6f51-410d-972e-8077c4359429	form-flow	f	t
64cd3b6e-e1d3-48b8-8411-22d6a072dee0	reset credentials	Reset credentials for a user if they forgot their password or something	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	t	t
becaa117-15e5-45e8-a1a3-21e9ed2386c7	Reset - Conditional OTP	Flow to determine if the OTP should be reset or not. Set to REQUIRED to force.	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
60674ea8-99c3-4ecb-9bf3-3c02d4ebc6b2	clients	Base authentication for clients	1bcb83bb-6f51-410d-972e-8077c4359429	client-flow	t	t
d79b5d89-c58c-4fbe-bd2b-a81b5ae758a9	first broker login	Actions taken after first broker login with identity provider account, which is not yet linked to any Keycloak account	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	t	t
adffdc5c-03cd-4fec-bec4-176a2c6f31b4	User creation or linking	Flow for the existing/non-existing user alternatives	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
800122c5-e595-4b18-9b47-2d2a6114c82e	Handle Existing Account	Handle what to do if there is existing account with same email/username like authenticated identity provider	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
240e0a30-5c0b-46d2-b79f-b1a766ab2c11	Account verification options	Method with which to verity the existing account	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
cce8d71e-170d-42ca-8eb2-00379e8d36e1	Verify Existing Account by Re-authentication	Reauthentication of existing account	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
65b58b02-4caf-4902-b4ee-32dd71bf9380	First broker login - Conditional 2FA	Flow to determine if any 2FA is required for the authentication	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
9ed018dd-2093-4b46-b7de-ff1f8776a54b	First Broker Login - Conditional Organization	Flow to determine if the authenticator that adds organization members is to be used	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	f	t
2c68b3c3-c75e-4efe-a180-d859e50a321a	saml ecp	SAML ECP Profile Authentication Flow	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	t	t
6854c68c-d750-4169-9f81-ed80da1dcc92	docker auth	Used by Docker clients to authenticate against the IDP	1bcb83bb-6f51-410d-972e-8077c4359429	basic-flow	t	t
\.


--
-- TOC entry 4188 (class 0 OID 17023)
-- Dependencies: 252
-- Data for Name: authenticator_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.authenticator_config (id, alias, realm_id) FROM stdin;
61f16194-6d0f-4c72-b41c-b4b3f15cd00a	review profile config	9c60f1e0-3006-44bf-be9a-e9110bd2df7a
53e50f44-1f14-4b89-931d-1d21acf9f854	create unique user config	9c60f1e0-3006-44bf-be9a-e9110bd2df7a
18676a29-cc38-4dca-b38e-77eaae0e716d	review profile config	1bcb83bb-6f51-410d-972e-8077c4359429
a32d048a-cb6b-4cf1-9121-c683ae58bacb	create unique user config	1bcb83bb-6f51-410d-972e-8077c4359429
\.


--
-- TOC entry 4191 (class 0 OID 17038)
-- Dependencies: 255
-- Data for Name: authenticator_config_entry; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.authenticator_config_entry (authenticator_id, value, name) FROM stdin;
53e50f44-1f14-4b89-931d-1d21acf9f854	false	require.password.update.after.registration
61f16194-6d0f-4c72-b41c-b4b3f15cd00a	missing	update.profile.on.first.login
18676a29-cc38-4dca-b38e-77eaae0e716d	missing	update.profile.on.first.login
a32d048a-cb6b-4cf1-9121-c683ae58bacb	false	require.password.update.after.registration
\.


--
-- TOC entry 4215 (class 0 OID 17476)
-- Dependencies: 279
-- Data for Name: broker_link; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.broker_link (identity_provider, storage_provider_id, realm_id, broker_user_id, broker_username, token, user_id) FROM stdin;
\.


--
-- TOC entry 4155 (class 0 OID 16399)
-- Dependencies: 219
-- Data for Name: client; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.client (id, enabled, full_scope_allowed, client_id, not_before, public_client, secret, base_url, bearer_only, management_url, surrogate_auth_required, realm_id, protocol, node_rereg_timeout, frontchannel_logout, consent_required, name, service_accounts_enabled, client_authenticator_type, root_url, description, registration_token, standard_flow_enabled, implicit_flow_enabled, direct_access_grants_enabled, always_display_in_console) FROM stdin;
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	f	master-realm	0	f	\N	\N	t	\N	f	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N	0	f	f	master Realm	f	client-secret	\N	\N	\N	t	f	f	f
955b4b55-16f3-472b-a919-6e4969b3feb0	t	f	account	0	t	\N	/realms/master/account/	f	\N	f	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	openid-connect	0	f	f	${client_account}	f	client-secret	${authBaseUrl}	\N	\N	t	f	f	f
31102431-0756-40bd-bffb-0757ab96be0d	t	f	account-console	0	t	\N	/realms/master/account/	f	\N	f	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	openid-connect	0	f	f	${client_account-console}	f	client-secret	${authBaseUrl}	\N	\N	t	f	f	f
c5de8543-d5d7-4480-ac34-5ace58f94217	t	f	broker	0	f	\N	\N	t	\N	f	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	openid-connect	0	f	f	${client_broker}	f	client-secret	\N	\N	\N	t	f	f	f
07df0e9c-0213-48a6-b89f-658593bde218	t	t	security-admin-console	0	t	\N	/admin/master/console/	f	\N	f	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	openid-connect	0	f	f	${client_security-admin-console}	f	client-secret	${authAdminUrl}	\N	\N	t	f	f	f
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	t	t	admin-cli	0	t	\N	\N	f	\N	f	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	openid-connect	0	f	f	${client_admin-cli}	f	client-secret	\N	\N	\N	f	f	t	f
cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	f	relma-realm	0	f	\N	\N	t	\N	f	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N	0	f	f	relma Realm	f	client-secret	\N	\N	\N	t	f	f	f
8c641705-f255-4386-ae8b-799b374d8ba5	t	f	realm-management	0	f	\N	\N	t	\N	f	1bcb83bb-6f51-410d-972e-8077c4359429	openid-connect	0	f	f	${client_realm-management}	f	client-secret	\N	\N	\N	t	f	f	f
a647b04c-25f4-4103-b180-ab357a7e456a	t	f	account	0	t	\N	/realms/relma/account/	f	\N	f	1bcb83bb-6f51-410d-972e-8077c4359429	openid-connect	0	f	f	${client_account}	f	client-secret	${authBaseUrl}	\N	\N	t	f	f	f
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	t	f	account-console	0	t	\N	/realms/relma/account/	f	\N	f	1bcb83bb-6f51-410d-972e-8077c4359429	openid-connect	0	f	f	${client_account-console}	f	client-secret	${authBaseUrl}	\N	\N	t	f	f	f
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	t	f	broker	0	f	\N	\N	t	\N	f	1bcb83bb-6f51-410d-972e-8077c4359429	openid-connect	0	f	f	${client_broker}	f	client-secret	\N	\N	\N	t	f	f	f
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	t	t	security-admin-console	0	t	\N	/admin/relma/console/	f	\N	f	1bcb83bb-6f51-410d-972e-8077c4359429	openid-connect	0	f	f	${client_security-admin-console}	f	client-secret	${authAdminUrl}	\N	\N	t	f	f	f
2dd2604b-db45-4e12-87cf-c3d59614195b	t	t	admin-cli	0	t	\N	\N	f	\N	f	1bcb83bb-6f51-410d-972e-8077c4359429	openid-connect	0	f	f	${client_admin-cli}	f	client-secret	\N	\N	\N	f	f	t	f
3f25824c-6d29-4079-a63b-f39091380fb6	t	t	remal-web-app	0	t	\N		f		f	1bcb83bb-6f51-410d-972e-8077c4359429	openid-connect	-1	t	f		f	client-secret			\N	t	f	t	f
4a3129e2-ab8f-4d02-b98d-aa9f82145149	t	t	relma-api-service	0	f	ezst3hPkNGellfyaG9jzvtK5nik4qkDv		f		f	1bcb83bb-6f51-410d-972e-8077c4359429	openid-connect	-1	t	f		f	client-secret			\N	t	f	t	f
\.


--
-- TOC entry 4174 (class 0 OID 16757)
-- Dependencies: 238
-- Data for Name: client_attributes; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.client_attributes (client_id, name, value) FROM stdin;
955b4b55-16f3-472b-a919-6e4969b3feb0	post.logout.redirect.uris	+
31102431-0756-40bd-bffb-0757ab96be0d	post.logout.redirect.uris	+
31102431-0756-40bd-bffb-0757ab96be0d	pkce.code.challenge.method	S256
07df0e9c-0213-48a6-b89f-658593bde218	post.logout.redirect.uris	+
07df0e9c-0213-48a6-b89f-658593bde218	pkce.code.challenge.method	S256
07df0e9c-0213-48a6-b89f-658593bde218	client.use.lightweight.access.token.enabled	true
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	client.use.lightweight.access.token.enabled	true
a647b04c-25f4-4103-b180-ab357a7e456a	post.logout.redirect.uris	+
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	post.logout.redirect.uris	+
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	pkce.code.challenge.method	S256
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	post.logout.redirect.uris	+
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	pkce.code.challenge.method	S256
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	client.use.lightweight.access.token.enabled	true
2dd2604b-db45-4e12-87cf-c3d59614195b	client.use.lightweight.access.token.enabled	true
3f25824c-6d29-4079-a63b-f39091380fb6	standard.token.exchange.enabled	false
3f25824c-6d29-4079-a63b-f39091380fb6	oauth2.device.authorization.grant.enabled	false
3f25824c-6d29-4079-a63b-f39091380fb6	oidc.ciba.grant.enabled	false
3f25824c-6d29-4079-a63b-f39091380fb6	backchannel.logout.session.required	true
3f25824c-6d29-4079-a63b-f39091380fb6	backchannel.logout.revoke.offline.tokens	false
4a3129e2-ab8f-4d02-b98d-aa9f82145149	client.secret.creation.time	1754041712
4a3129e2-ab8f-4d02-b98d-aa9f82145149	standard.token.exchange.enabled	false
4a3129e2-ab8f-4d02-b98d-aa9f82145149	oauth2.device.authorization.grant.enabled	false
4a3129e2-ab8f-4d02-b98d-aa9f82145149	oidc.ciba.grant.enabled	false
4a3129e2-ab8f-4d02-b98d-aa9f82145149	backchannel.logout.session.required	true
4a3129e2-ab8f-4d02-b98d-aa9f82145149	backchannel.logout.revoke.offline.tokens	false
4a3129e2-ab8f-4d02-b98d-aa9f82145149	realm_client	false
4a3129e2-ab8f-4d02-b98d-aa9f82145149	display.on.consent.screen	false
4a3129e2-ab8f-4d02-b98d-aa9f82145149	frontchannel.logout.session.required	true
\.


--
-- TOC entry 4226 (class 0 OID 17725)
-- Dependencies: 290
-- Data for Name: client_auth_flow_bindings; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.client_auth_flow_bindings (client_id, flow_id, binding_name) FROM stdin;
\.


--
-- TOC entry 4225 (class 0 OID 17600)
-- Dependencies: 289
-- Data for Name: client_initial_access; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.client_initial_access (id, realm_id, "timestamp", expiration, count, remaining_count) FROM stdin;
\.


--
-- TOC entry 4175 (class 0 OID 16767)
-- Dependencies: 239
-- Data for Name: client_node_registrations; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.client_node_registrations (client_id, value, name) FROM stdin;
\.


--
-- TOC entry 4203 (class 0 OID 17266)
-- Dependencies: 267
-- Data for Name: client_scope; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.client_scope (id, name, realm_id, description, protocol) FROM stdin;
b388e2bc-bacc-4f48-93cf-52408edbb05d	offline_access	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	OpenID Connect built-in scope: offline_access	openid-connect
79c36a51-655c-4e85-93d6-9830b3978dae	role_list	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	SAML role list	saml
5fcc4963-a080-43db-a0b2-a0dec44ca4b0	saml_organization	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	Organization Membership	saml
c81ce17d-7ad4-432e-a19d-69719f95deb5	profile	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	OpenID Connect built-in scope: profile	openid-connect
bee3d7eb-817c-4fde-998a-a6b0ca227c64	email	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	OpenID Connect built-in scope: email	openid-connect
c8ceba4a-ba7a-4fbe-811e-5d60126fc508	address	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	OpenID Connect built-in scope: address	openid-connect
37062dfb-3d8d-4878-9fff-b84f9cea09dd	phone	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	OpenID Connect built-in scope: phone	openid-connect
13872a50-75a4-4d61-9b8d-29ee9925e3bd	roles	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	OpenID Connect scope for add user roles to the access token	openid-connect
1fff4531-7378-462d-899b-45c91f87398d	web-origins	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	OpenID Connect scope for add allowed web origins to the access token	openid-connect
910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	microprofile-jwt	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	Microprofile - JWT built-in scope	openid-connect
399bea11-410b-47f6-aec7-14fc5292312b	acr	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	OpenID Connect scope for add acr (authentication context class reference) to the token	openid-connect
6f5f268b-8c54-430d-ab31-18cf136b31b7	basic	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	OpenID Connect scope for add all basic claims to the token	openid-connect
bb5760a9-c212-489e-9dd3-255eae1434a4	service_account	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	Specific scope for a client enabled for service accounts	openid-connect
d631f21c-5c6b-4e79-bde9-3276d2eb2eff	organization	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	Additional claims about the organization a subject belongs to	openid-connect
0c348ee7-e445-446d-a027-46f6033d5bf9	offline_access	1bcb83bb-6f51-410d-972e-8077c4359429	OpenID Connect built-in scope: offline_access	openid-connect
0e236cce-035e-408a-b9c5-4c5b44f48fa5	role_list	1bcb83bb-6f51-410d-972e-8077c4359429	SAML role list	saml
8adadaca-c13b-46f1-bec5-95a9ed2f85de	saml_organization	1bcb83bb-6f51-410d-972e-8077c4359429	Organization Membership	saml
1aab8122-bbd8-4659-8067-089a57fbc21b	profile	1bcb83bb-6f51-410d-972e-8077c4359429	OpenID Connect built-in scope: profile	openid-connect
433e3125-9ba0-46a3-95cd-bb58d1932256	email	1bcb83bb-6f51-410d-972e-8077c4359429	OpenID Connect built-in scope: email	openid-connect
3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	address	1bcb83bb-6f51-410d-972e-8077c4359429	OpenID Connect built-in scope: address	openid-connect
d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	phone	1bcb83bb-6f51-410d-972e-8077c4359429	OpenID Connect built-in scope: phone	openid-connect
1768dcff-3cc3-4d32-839a-13e3a381b09b	roles	1bcb83bb-6f51-410d-972e-8077c4359429	OpenID Connect scope for add user roles to the access token	openid-connect
c3787d89-cd29-4cfc-9e33-f484915204fc	web-origins	1bcb83bb-6f51-410d-972e-8077c4359429	OpenID Connect scope for add allowed web origins to the access token	openid-connect
82b5f4a5-829c-49be-b4ed-561172ed59af	microprofile-jwt	1bcb83bb-6f51-410d-972e-8077c4359429	Microprofile - JWT built-in scope	openid-connect
15af2cf2-5174-41b9-b356-ef154a709ceb	acr	1bcb83bb-6f51-410d-972e-8077c4359429	OpenID Connect scope for add acr (authentication context class reference) to the token	openid-connect
c03d35bc-76a3-46a6-a187-38ea2dad9020	basic	1bcb83bb-6f51-410d-972e-8077c4359429	OpenID Connect scope for add all basic claims to the token	openid-connect
f04da0a3-b1fa-4ccd-9cb7-104583bcb867	service_account	1bcb83bb-6f51-410d-972e-8077c4359429	Specific scope for a client enabled for service accounts	openid-connect
68cab4d6-3871-4a3d-94ca-294ca1c715a1	organization	1bcb83bb-6f51-410d-972e-8077c4359429	Additional claims about the organization a subject belongs to	openid-connect
da252e95-380c-4844-b05e-d50eaf94a2b2	tenant	1bcb83bb-6f51-410d-972e-8077c4359429		openid-connect
\.


--
-- TOC entry 4204 (class 0 OID 17280)
-- Dependencies: 268
-- Data for Name: client_scope_attributes; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.client_scope_attributes (scope_id, value, name) FROM stdin;
b388e2bc-bacc-4f48-93cf-52408edbb05d	true	display.on.consent.screen
b388e2bc-bacc-4f48-93cf-52408edbb05d	${offlineAccessScopeConsentText}	consent.screen.text
79c36a51-655c-4e85-93d6-9830b3978dae	true	display.on.consent.screen
79c36a51-655c-4e85-93d6-9830b3978dae	${samlRoleListScopeConsentText}	consent.screen.text
5fcc4963-a080-43db-a0b2-a0dec44ca4b0	false	display.on.consent.screen
c81ce17d-7ad4-432e-a19d-69719f95deb5	true	display.on.consent.screen
c81ce17d-7ad4-432e-a19d-69719f95deb5	${profileScopeConsentText}	consent.screen.text
c81ce17d-7ad4-432e-a19d-69719f95deb5	true	include.in.token.scope
bee3d7eb-817c-4fde-998a-a6b0ca227c64	true	display.on.consent.screen
bee3d7eb-817c-4fde-998a-a6b0ca227c64	${emailScopeConsentText}	consent.screen.text
bee3d7eb-817c-4fde-998a-a6b0ca227c64	true	include.in.token.scope
c8ceba4a-ba7a-4fbe-811e-5d60126fc508	true	display.on.consent.screen
c8ceba4a-ba7a-4fbe-811e-5d60126fc508	${addressScopeConsentText}	consent.screen.text
c8ceba4a-ba7a-4fbe-811e-5d60126fc508	true	include.in.token.scope
37062dfb-3d8d-4878-9fff-b84f9cea09dd	true	display.on.consent.screen
37062dfb-3d8d-4878-9fff-b84f9cea09dd	${phoneScopeConsentText}	consent.screen.text
37062dfb-3d8d-4878-9fff-b84f9cea09dd	true	include.in.token.scope
13872a50-75a4-4d61-9b8d-29ee9925e3bd	true	display.on.consent.screen
13872a50-75a4-4d61-9b8d-29ee9925e3bd	${rolesScopeConsentText}	consent.screen.text
13872a50-75a4-4d61-9b8d-29ee9925e3bd	false	include.in.token.scope
1fff4531-7378-462d-899b-45c91f87398d	false	display.on.consent.screen
1fff4531-7378-462d-899b-45c91f87398d		consent.screen.text
1fff4531-7378-462d-899b-45c91f87398d	false	include.in.token.scope
910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	false	display.on.consent.screen
910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	true	include.in.token.scope
399bea11-410b-47f6-aec7-14fc5292312b	false	display.on.consent.screen
399bea11-410b-47f6-aec7-14fc5292312b	false	include.in.token.scope
6f5f268b-8c54-430d-ab31-18cf136b31b7	false	display.on.consent.screen
6f5f268b-8c54-430d-ab31-18cf136b31b7	false	include.in.token.scope
bb5760a9-c212-489e-9dd3-255eae1434a4	false	display.on.consent.screen
bb5760a9-c212-489e-9dd3-255eae1434a4	false	include.in.token.scope
d631f21c-5c6b-4e79-bde9-3276d2eb2eff	true	display.on.consent.screen
d631f21c-5c6b-4e79-bde9-3276d2eb2eff	${organizationScopeConsentText}	consent.screen.text
d631f21c-5c6b-4e79-bde9-3276d2eb2eff	true	include.in.token.scope
0c348ee7-e445-446d-a027-46f6033d5bf9	true	display.on.consent.screen
0c348ee7-e445-446d-a027-46f6033d5bf9	${offlineAccessScopeConsentText}	consent.screen.text
0e236cce-035e-408a-b9c5-4c5b44f48fa5	true	display.on.consent.screen
0e236cce-035e-408a-b9c5-4c5b44f48fa5	${samlRoleListScopeConsentText}	consent.screen.text
8adadaca-c13b-46f1-bec5-95a9ed2f85de	false	display.on.consent.screen
1aab8122-bbd8-4659-8067-089a57fbc21b	true	display.on.consent.screen
1aab8122-bbd8-4659-8067-089a57fbc21b	${profileScopeConsentText}	consent.screen.text
1aab8122-bbd8-4659-8067-089a57fbc21b	true	include.in.token.scope
433e3125-9ba0-46a3-95cd-bb58d1932256	true	display.on.consent.screen
433e3125-9ba0-46a3-95cd-bb58d1932256	${emailScopeConsentText}	consent.screen.text
433e3125-9ba0-46a3-95cd-bb58d1932256	true	include.in.token.scope
3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	true	display.on.consent.screen
3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	${addressScopeConsentText}	consent.screen.text
3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	true	include.in.token.scope
d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	true	display.on.consent.screen
d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	${phoneScopeConsentText}	consent.screen.text
d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	true	include.in.token.scope
1768dcff-3cc3-4d32-839a-13e3a381b09b	true	display.on.consent.screen
1768dcff-3cc3-4d32-839a-13e3a381b09b	${rolesScopeConsentText}	consent.screen.text
1768dcff-3cc3-4d32-839a-13e3a381b09b	false	include.in.token.scope
c3787d89-cd29-4cfc-9e33-f484915204fc	false	display.on.consent.screen
c3787d89-cd29-4cfc-9e33-f484915204fc		consent.screen.text
c3787d89-cd29-4cfc-9e33-f484915204fc	false	include.in.token.scope
82b5f4a5-829c-49be-b4ed-561172ed59af	false	display.on.consent.screen
82b5f4a5-829c-49be-b4ed-561172ed59af	true	include.in.token.scope
15af2cf2-5174-41b9-b356-ef154a709ceb	false	display.on.consent.screen
15af2cf2-5174-41b9-b356-ef154a709ceb	false	include.in.token.scope
c03d35bc-76a3-46a6-a187-38ea2dad9020	false	display.on.consent.screen
c03d35bc-76a3-46a6-a187-38ea2dad9020	false	include.in.token.scope
f04da0a3-b1fa-4ccd-9cb7-104583bcb867	false	display.on.consent.screen
f04da0a3-b1fa-4ccd-9cb7-104583bcb867	false	include.in.token.scope
68cab4d6-3871-4a3d-94ca-294ca1c715a1	true	display.on.consent.screen
68cab4d6-3871-4a3d-94ca-294ca1c715a1	${organizationScopeConsentText}	consent.screen.text
68cab4d6-3871-4a3d-94ca-294ca1c715a1	true	include.in.token.scope
da252e95-380c-4844-b05e-d50eaf94a2b2	true	display.on.consent.screen
da252e95-380c-4844-b05e-d50eaf94a2b2		consent.screen.text
da252e95-380c-4844-b05e-d50eaf94a2b2	false	include.in.token.scope
da252e95-380c-4844-b05e-d50eaf94a2b2		gui.order
\.


--
-- TOC entry 4227 (class 0 OID 17766)
-- Dependencies: 291
-- Data for Name: client_scope_client; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.client_scope_client (client_id, scope_id, default_scope) FROM stdin;
955b4b55-16f3-472b-a919-6e4969b3feb0	399bea11-410b-47f6-aec7-14fc5292312b	t
955b4b55-16f3-472b-a919-6e4969b3feb0	1fff4531-7378-462d-899b-45c91f87398d	t
955b4b55-16f3-472b-a919-6e4969b3feb0	bee3d7eb-817c-4fde-998a-a6b0ca227c64	t
955b4b55-16f3-472b-a919-6e4969b3feb0	13872a50-75a4-4d61-9b8d-29ee9925e3bd	t
955b4b55-16f3-472b-a919-6e4969b3feb0	c81ce17d-7ad4-432e-a19d-69719f95deb5	t
955b4b55-16f3-472b-a919-6e4969b3feb0	6f5f268b-8c54-430d-ab31-18cf136b31b7	t
955b4b55-16f3-472b-a919-6e4969b3feb0	b388e2bc-bacc-4f48-93cf-52408edbb05d	f
955b4b55-16f3-472b-a919-6e4969b3feb0	c8ceba4a-ba7a-4fbe-811e-5d60126fc508	f
955b4b55-16f3-472b-a919-6e4969b3feb0	910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	f
955b4b55-16f3-472b-a919-6e4969b3feb0	d631f21c-5c6b-4e79-bde9-3276d2eb2eff	f
955b4b55-16f3-472b-a919-6e4969b3feb0	37062dfb-3d8d-4878-9fff-b84f9cea09dd	f
31102431-0756-40bd-bffb-0757ab96be0d	399bea11-410b-47f6-aec7-14fc5292312b	t
31102431-0756-40bd-bffb-0757ab96be0d	1fff4531-7378-462d-899b-45c91f87398d	t
31102431-0756-40bd-bffb-0757ab96be0d	bee3d7eb-817c-4fde-998a-a6b0ca227c64	t
31102431-0756-40bd-bffb-0757ab96be0d	13872a50-75a4-4d61-9b8d-29ee9925e3bd	t
31102431-0756-40bd-bffb-0757ab96be0d	c81ce17d-7ad4-432e-a19d-69719f95deb5	t
31102431-0756-40bd-bffb-0757ab96be0d	6f5f268b-8c54-430d-ab31-18cf136b31b7	t
31102431-0756-40bd-bffb-0757ab96be0d	b388e2bc-bacc-4f48-93cf-52408edbb05d	f
31102431-0756-40bd-bffb-0757ab96be0d	c8ceba4a-ba7a-4fbe-811e-5d60126fc508	f
31102431-0756-40bd-bffb-0757ab96be0d	910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	f
31102431-0756-40bd-bffb-0757ab96be0d	d631f21c-5c6b-4e79-bde9-3276d2eb2eff	f
31102431-0756-40bd-bffb-0757ab96be0d	37062dfb-3d8d-4878-9fff-b84f9cea09dd	f
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	399bea11-410b-47f6-aec7-14fc5292312b	t
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	1fff4531-7378-462d-899b-45c91f87398d	t
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	bee3d7eb-817c-4fde-998a-a6b0ca227c64	t
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	13872a50-75a4-4d61-9b8d-29ee9925e3bd	t
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	c81ce17d-7ad4-432e-a19d-69719f95deb5	t
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	6f5f268b-8c54-430d-ab31-18cf136b31b7	t
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	b388e2bc-bacc-4f48-93cf-52408edbb05d	f
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	c8ceba4a-ba7a-4fbe-811e-5d60126fc508	f
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	f
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	d631f21c-5c6b-4e79-bde9-3276d2eb2eff	f
9e6c5043-ab72-4f83-b2b3-8a7596f149e5	37062dfb-3d8d-4878-9fff-b84f9cea09dd	f
c5de8543-d5d7-4480-ac34-5ace58f94217	399bea11-410b-47f6-aec7-14fc5292312b	t
c5de8543-d5d7-4480-ac34-5ace58f94217	1fff4531-7378-462d-899b-45c91f87398d	t
c5de8543-d5d7-4480-ac34-5ace58f94217	bee3d7eb-817c-4fde-998a-a6b0ca227c64	t
c5de8543-d5d7-4480-ac34-5ace58f94217	13872a50-75a4-4d61-9b8d-29ee9925e3bd	t
c5de8543-d5d7-4480-ac34-5ace58f94217	c81ce17d-7ad4-432e-a19d-69719f95deb5	t
c5de8543-d5d7-4480-ac34-5ace58f94217	6f5f268b-8c54-430d-ab31-18cf136b31b7	t
c5de8543-d5d7-4480-ac34-5ace58f94217	b388e2bc-bacc-4f48-93cf-52408edbb05d	f
c5de8543-d5d7-4480-ac34-5ace58f94217	c8ceba4a-ba7a-4fbe-811e-5d60126fc508	f
c5de8543-d5d7-4480-ac34-5ace58f94217	910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	f
c5de8543-d5d7-4480-ac34-5ace58f94217	d631f21c-5c6b-4e79-bde9-3276d2eb2eff	f
c5de8543-d5d7-4480-ac34-5ace58f94217	37062dfb-3d8d-4878-9fff-b84f9cea09dd	f
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	399bea11-410b-47f6-aec7-14fc5292312b	t
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	1fff4531-7378-462d-899b-45c91f87398d	t
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	bee3d7eb-817c-4fde-998a-a6b0ca227c64	t
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	13872a50-75a4-4d61-9b8d-29ee9925e3bd	t
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	c81ce17d-7ad4-432e-a19d-69719f95deb5	t
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	6f5f268b-8c54-430d-ab31-18cf136b31b7	t
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	b388e2bc-bacc-4f48-93cf-52408edbb05d	f
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	c8ceba4a-ba7a-4fbe-811e-5d60126fc508	f
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	f
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	d631f21c-5c6b-4e79-bde9-3276d2eb2eff	f
e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	37062dfb-3d8d-4878-9fff-b84f9cea09dd	f
07df0e9c-0213-48a6-b89f-658593bde218	399bea11-410b-47f6-aec7-14fc5292312b	t
07df0e9c-0213-48a6-b89f-658593bde218	1fff4531-7378-462d-899b-45c91f87398d	t
07df0e9c-0213-48a6-b89f-658593bde218	bee3d7eb-817c-4fde-998a-a6b0ca227c64	t
07df0e9c-0213-48a6-b89f-658593bde218	13872a50-75a4-4d61-9b8d-29ee9925e3bd	t
07df0e9c-0213-48a6-b89f-658593bde218	c81ce17d-7ad4-432e-a19d-69719f95deb5	t
07df0e9c-0213-48a6-b89f-658593bde218	6f5f268b-8c54-430d-ab31-18cf136b31b7	t
07df0e9c-0213-48a6-b89f-658593bde218	b388e2bc-bacc-4f48-93cf-52408edbb05d	f
07df0e9c-0213-48a6-b89f-658593bde218	c8ceba4a-ba7a-4fbe-811e-5d60126fc508	f
07df0e9c-0213-48a6-b89f-658593bde218	910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	f
07df0e9c-0213-48a6-b89f-658593bde218	d631f21c-5c6b-4e79-bde9-3276d2eb2eff	f
07df0e9c-0213-48a6-b89f-658593bde218	37062dfb-3d8d-4878-9fff-b84f9cea09dd	f
a647b04c-25f4-4103-b180-ab357a7e456a	1aab8122-bbd8-4659-8067-089a57fbc21b	t
a647b04c-25f4-4103-b180-ab357a7e456a	433e3125-9ba0-46a3-95cd-bb58d1932256	t
a647b04c-25f4-4103-b180-ab357a7e456a	1768dcff-3cc3-4d32-839a-13e3a381b09b	t
a647b04c-25f4-4103-b180-ab357a7e456a	c3787d89-cd29-4cfc-9e33-f484915204fc	t
a647b04c-25f4-4103-b180-ab357a7e456a	15af2cf2-5174-41b9-b356-ef154a709ceb	t
a647b04c-25f4-4103-b180-ab357a7e456a	c03d35bc-76a3-46a6-a187-38ea2dad9020	t
a647b04c-25f4-4103-b180-ab357a7e456a	68cab4d6-3871-4a3d-94ca-294ca1c715a1	f
a647b04c-25f4-4103-b180-ab357a7e456a	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	f
a647b04c-25f4-4103-b180-ab357a7e456a	0c348ee7-e445-446d-a027-46f6033d5bf9	f
a647b04c-25f4-4103-b180-ab357a7e456a	82b5f4a5-829c-49be-b4ed-561172ed59af	f
a647b04c-25f4-4103-b180-ab357a7e456a	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	f
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	1aab8122-bbd8-4659-8067-089a57fbc21b	t
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	433e3125-9ba0-46a3-95cd-bb58d1932256	t
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	1768dcff-3cc3-4d32-839a-13e3a381b09b	t
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	c3787d89-cd29-4cfc-9e33-f484915204fc	t
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	15af2cf2-5174-41b9-b356-ef154a709ceb	t
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	c03d35bc-76a3-46a6-a187-38ea2dad9020	t
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	68cab4d6-3871-4a3d-94ca-294ca1c715a1	f
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	f
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	0c348ee7-e445-446d-a027-46f6033d5bf9	f
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	82b5f4a5-829c-49be-b4ed-561172ed59af	f
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	f
2dd2604b-db45-4e12-87cf-c3d59614195b	1aab8122-bbd8-4659-8067-089a57fbc21b	t
2dd2604b-db45-4e12-87cf-c3d59614195b	433e3125-9ba0-46a3-95cd-bb58d1932256	t
2dd2604b-db45-4e12-87cf-c3d59614195b	1768dcff-3cc3-4d32-839a-13e3a381b09b	t
2dd2604b-db45-4e12-87cf-c3d59614195b	c3787d89-cd29-4cfc-9e33-f484915204fc	t
2dd2604b-db45-4e12-87cf-c3d59614195b	15af2cf2-5174-41b9-b356-ef154a709ceb	t
2dd2604b-db45-4e12-87cf-c3d59614195b	c03d35bc-76a3-46a6-a187-38ea2dad9020	t
2dd2604b-db45-4e12-87cf-c3d59614195b	68cab4d6-3871-4a3d-94ca-294ca1c715a1	f
2dd2604b-db45-4e12-87cf-c3d59614195b	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	f
2dd2604b-db45-4e12-87cf-c3d59614195b	0c348ee7-e445-446d-a027-46f6033d5bf9	f
2dd2604b-db45-4e12-87cf-c3d59614195b	82b5f4a5-829c-49be-b4ed-561172ed59af	f
2dd2604b-db45-4e12-87cf-c3d59614195b	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	f
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	1aab8122-bbd8-4659-8067-089a57fbc21b	t
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	433e3125-9ba0-46a3-95cd-bb58d1932256	t
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	1768dcff-3cc3-4d32-839a-13e3a381b09b	t
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	c3787d89-cd29-4cfc-9e33-f484915204fc	t
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	15af2cf2-5174-41b9-b356-ef154a709ceb	t
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	c03d35bc-76a3-46a6-a187-38ea2dad9020	t
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	68cab4d6-3871-4a3d-94ca-294ca1c715a1	f
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	f
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	0c348ee7-e445-446d-a027-46f6033d5bf9	f
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	82b5f4a5-829c-49be-b4ed-561172ed59af	f
1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	f
8c641705-f255-4386-ae8b-799b374d8ba5	1aab8122-bbd8-4659-8067-089a57fbc21b	t
8c641705-f255-4386-ae8b-799b374d8ba5	433e3125-9ba0-46a3-95cd-bb58d1932256	t
8c641705-f255-4386-ae8b-799b374d8ba5	1768dcff-3cc3-4d32-839a-13e3a381b09b	t
8c641705-f255-4386-ae8b-799b374d8ba5	c3787d89-cd29-4cfc-9e33-f484915204fc	t
8c641705-f255-4386-ae8b-799b374d8ba5	15af2cf2-5174-41b9-b356-ef154a709ceb	t
8c641705-f255-4386-ae8b-799b374d8ba5	c03d35bc-76a3-46a6-a187-38ea2dad9020	t
8c641705-f255-4386-ae8b-799b374d8ba5	68cab4d6-3871-4a3d-94ca-294ca1c715a1	f
8c641705-f255-4386-ae8b-799b374d8ba5	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	f
8c641705-f255-4386-ae8b-799b374d8ba5	0c348ee7-e445-446d-a027-46f6033d5bf9	f
8c641705-f255-4386-ae8b-799b374d8ba5	82b5f4a5-829c-49be-b4ed-561172ed59af	f
8c641705-f255-4386-ae8b-799b374d8ba5	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	f
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	1aab8122-bbd8-4659-8067-089a57fbc21b	t
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	433e3125-9ba0-46a3-95cd-bb58d1932256	t
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	1768dcff-3cc3-4d32-839a-13e3a381b09b	t
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	c3787d89-cd29-4cfc-9e33-f484915204fc	t
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	15af2cf2-5174-41b9-b356-ef154a709ceb	t
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	c03d35bc-76a3-46a6-a187-38ea2dad9020	t
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	68cab4d6-3871-4a3d-94ca-294ca1c715a1	f
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	f
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	0c348ee7-e445-446d-a027-46f6033d5bf9	f
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	82b5f4a5-829c-49be-b4ed-561172ed59af	f
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	f
3f25824c-6d29-4079-a63b-f39091380fb6	1aab8122-bbd8-4659-8067-089a57fbc21b	t
3f25824c-6d29-4079-a63b-f39091380fb6	433e3125-9ba0-46a3-95cd-bb58d1932256	t
3f25824c-6d29-4079-a63b-f39091380fb6	1768dcff-3cc3-4d32-839a-13e3a381b09b	t
3f25824c-6d29-4079-a63b-f39091380fb6	c3787d89-cd29-4cfc-9e33-f484915204fc	t
3f25824c-6d29-4079-a63b-f39091380fb6	15af2cf2-5174-41b9-b356-ef154a709ceb	t
3f25824c-6d29-4079-a63b-f39091380fb6	c03d35bc-76a3-46a6-a187-38ea2dad9020	t
3f25824c-6d29-4079-a63b-f39091380fb6	68cab4d6-3871-4a3d-94ca-294ca1c715a1	f
3f25824c-6d29-4079-a63b-f39091380fb6	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	f
3f25824c-6d29-4079-a63b-f39091380fb6	0c348ee7-e445-446d-a027-46f6033d5bf9	f
3f25824c-6d29-4079-a63b-f39091380fb6	82b5f4a5-829c-49be-b4ed-561172ed59af	f
3f25824c-6d29-4079-a63b-f39091380fb6	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	f
4a3129e2-ab8f-4d02-b98d-aa9f82145149	1aab8122-bbd8-4659-8067-089a57fbc21b	t
4a3129e2-ab8f-4d02-b98d-aa9f82145149	433e3125-9ba0-46a3-95cd-bb58d1932256	t
4a3129e2-ab8f-4d02-b98d-aa9f82145149	1768dcff-3cc3-4d32-839a-13e3a381b09b	t
4a3129e2-ab8f-4d02-b98d-aa9f82145149	c3787d89-cd29-4cfc-9e33-f484915204fc	t
4a3129e2-ab8f-4d02-b98d-aa9f82145149	15af2cf2-5174-41b9-b356-ef154a709ceb	t
4a3129e2-ab8f-4d02-b98d-aa9f82145149	c03d35bc-76a3-46a6-a187-38ea2dad9020	t
4a3129e2-ab8f-4d02-b98d-aa9f82145149	68cab4d6-3871-4a3d-94ca-294ca1c715a1	f
4a3129e2-ab8f-4d02-b98d-aa9f82145149	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	f
4a3129e2-ab8f-4d02-b98d-aa9f82145149	0c348ee7-e445-446d-a027-46f6033d5bf9	f
4a3129e2-ab8f-4d02-b98d-aa9f82145149	82b5f4a5-829c-49be-b4ed-561172ed59af	f
4a3129e2-ab8f-4d02-b98d-aa9f82145149	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	f
4a3129e2-ab8f-4d02-b98d-aa9f82145149	da252e95-380c-4844-b05e-d50eaf94a2b2	t
3f25824c-6d29-4079-a63b-f39091380fb6	da252e95-380c-4844-b05e-d50eaf94a2b2	t
\.


--
-- TOC entry 4205 (class 0 OID 17285)
-- Dependencies: 269
-- Data for Name: client_scope_role_mapping; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.client_scope_role_mapping (scope_id, role_id) FROM stdin;
b388e2bc-bacc-4f48-93cf-52408edbb05d	2e74a5cf-4b39-44c1-af0f-10cd748b824a
0c348ee7-e445-446d-a027-46f6033d5bf9	94735068-d221-4f3b-bdd0-075e38c1d87f
\.


--
-- TOC entry 4223 (class 0 OID 17521)
-- Dependencies: 287
-- Data for Name: component; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.component (id, name, parent_id, provider_id, provider_type, realm_id, sub_type) FROM stdin;
d67068cd-10f0-413d-a571-c4dd3d55f0f3	Trusted Hosts	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	trusted-hosts	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	anonymous
80e6ad95-bf73-4962-84b6-3f86ff009de9	Consent Required	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	consent-required	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	anonymous
edd049a4-8352-4c39-942e-6d9854ce4420	Full Scope Disabled	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	scope	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	anonymous
2d329ce7-8370-4121-b975-a81af1a9a1e4	Max Clients Limit	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	max-clients	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	anonymous
07ca313c-3c6c-4b28-bc13-7fd2cef02fd4	Allowed Protocol Mapper Types	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	allowed-protocol-mappers	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	anonymous
edbb4659-4e2b-495e-b2c0-4f1599f1bf43	Allowed Client Scopes	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	allowed-client-templates	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	anonymous
ea713218-3092-4493-9b11-5d34877336f5	Allowed Protocol Mapper Types	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	allowed-protocol-mappers	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	authenticated
448bb02e-d097-41bb-bb7c-96a0abfd6383	Allowed Client Scopes	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	allowed-client-templates	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	authenticated
c6f077e3-b361-4a84-9986-87f3561915f1	rsa-generated	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	rsa-generated	org.keycloak.keys.KeyProvider	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N
275ed2ca-487a-4ee9-a9ce-12b557ab032b	rsa-enc-generated	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	rsa-enc-generated	org.keycloak.keys.KeyProvider	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N
a7c56771-9859-41c0-8534-85e6629beceb	hmac-generated-hs512	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	hmac-generated	org.keycloak.keys.KeyProvider	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N
27618a8d-fa0a-43d6-8e59-d516c9d6e3b3	aes-generated	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	aes-generated	org.keycloak.keys.KeyProvider	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N
f389cdc8-7642-4d71-aae1-3960fcc8a13c	\N	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	declarative-user-profile	org.keycloak.userprofile.UserProfileProvider	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N
d42c0ae4-569a-4c6b-ab15-118854624a49	rsa-generated	1bcb83bb-6f51-410d-972e-8077c4359429	rsa-generated	org.keycloak.keys.KeyProvider	1bcb83bb-6f51-410d-972e-8077c4359429	\N
e819ba24-fd1e-48b9-ac16-677134392fa0	rsa-enc-generated	1bcb83bb-6f51-410d-972e-8077c4359429	rsa-enc-generated	org.keycloak.keys.KeyProvider	1bcb83bb-6f51-410d-972e-8077c4359429	\N
6fe3cf7b-426f-45da-ad1c-aaa1f94308bc	hmac-generated-hs512	1bcb83bb-6f51-410d-972e-8077c4359429	hmac-generated	org.keycloak.keys.KeyProvider	1bcb83bb-6f51-410d-972e-8077c4359429	\N
4293f817-2107-4e0f-990b-bcb1fa37cdeb	aes-generated	1bcb83bb-6f51-410d-972e-8077c4359429	aes-generated	org.keycloak.keys.KeyProvider	1bcb83bb-6f51-410d-972e-8077c4359429	\N
62d45534-2a0b-4d2e-9ed8-4760c488901b	Trusted Hosts	1bcb83bb-6f51-410d-972e-8077c4359429	trusted-hosts	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	anonymous
1decbced-7db5-457d-b66b-cc9ae3b20a63	Consent Required	1bcb83bb-6f51-410d-972e-8077c4359429	consent-required	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	anonymous
becdcfdf-654e-4997-9a43-0e87af31642c	Full Scope Disabled	1bcb83bb-6f51-410d-972e-8077c4359429	scope	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	anonymous
09772419-7748-496f-b84b-899a7eb9b955	Max Clients Limit	1bcb83bb-6f51-410d-972e-8077c4359429	max-clients	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	anonymous
6989b996-7433-4076-8ae5-7306a575ea2a	Allowed Protocol Mapper Types	1bcb83bb-6f51-410d-972e-8077c4359429	allowed-protocol-mappers	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	anonymous
cce130c9-3a19-4aa3-8b3e-2976107312cd	Allowed Client Scopes	1bcb83bb-6f51-410d-972e-8077c4359429	allowed-client-templates	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	anonymous
705c5b86-ca1b-4abc-9cc5-68d2a33b4131	Allowed Protocol Mapper Types	1bcb83bb-6f51-410d-972e-8077c4359429	allowed-protocol-mappers	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	authenticated
53cd1630-d5d4-424c-b4e4-59708f069371	Allowed Client Scopes	1bcb83bb-6f51-410d-972e-8077c4359429	allowed-client-templates	org.keycloak.services.clientregistration.policy.ClientRegistrationPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	authenticated
5480c506-17e5-4518-b843-a945f4140128	\N	1bcb83bb-6f51-410d-972e-8077c4359429	declarative-user-profile	org.keycloak.userprofile.UserProfileProvider	1bcb83bb-6f51-410d-972e-8077c4359429	\N
\.


--
-- TOC entry 4222 (class 0 OID 17516)
-- Dependencies: 286
-- Data for Name: component_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.component_config (id, component_id, name, value) FROM stdin;
2eede11b-4b75-4c9d-93e0-31921f1f9011	ea713218-3092-4493-9b11-5d34877336f5	allowed-protocol-mapper-types	oidc-address-mapper
8380ba1f-cb4b-44b7-aa19-f347afd8a5c6	ea713218-3092-4493-9b11-5d34877336f5	allowed-protocol-mapper-types	oidc-usermodel-property-mapper
1d0370c5-f122-41eb-9ba4-6a6627dd12bc	ea713218-3092-4493-9b11-5d34877336f5	allowed-protocol-mapper-types	oidc-full-name-mapper
94307ce5-921e-4ce9-93a9-275276f21092	ea713218-3092-4493-9b11-5d34877336f5	allowed-protocol-mapper-types	oidc-sha256-pairwise-sub-mapper
987f0585-67ae-4081-ad13-d632a4d1f0f8	ea713218-3092-4493-9b11-5d34877336f5	allowed-protocol-mapper-types	oidc-usermodel-attribute-mapper
b28712bc-6262-4fa9-86dd-0b484d0fcf40	ea713218-3092-4493-9b11-5d34877336f5	allowed-protocol-mapper-types	saml-user-attribute-mapper
c727b77d-961c-4995-afda-16661b81a1b4	ea713218-3092-4493-9b11-5d34877336f5	allowed-protocol-mapper-types	saml-user-property-mapper
07ff2b72-9991-4743-b4e7-2c0daa175fde	ea713218-3092-4493-9b11-5d34877336f5	allowed-protocol-mapper-types	saml-role-list-mapper
f33029b0-8ded-42ee-88ff-8447f6b6c1e9	2d329ce7-8370-4121-b975-a81af1a9a1e4	max-clients	200
d2155d52-4766-49bd-834a-2bb98f8954f2	edbb4659-4e2b-495e-b2c0-4f1599f1bf43	allow-default-scopes	true
56d4a14d-4619-4424-b6dc-4fc8d6056c3e	07ca313c-3c6c-4b28-bc13-7fd2cef02fd4	allowed-protocol-mapper-types	saml-user-property-mapper
5ea8a94a-4232-4253-9b9d-ccbc10bc89dd	07ca313c-3c6c-4b28-bc13-7fd2cef02fd4	allowed-protocol-mapper-types	oidc-full-name-mapper
b25c0e3e-9ebe-44dd-8d85-b75fdc2fc617	07ca313c-3c6c-4b28-bc13-7fd2cef02fd4	allowed-protocol-mapper-types	oidc-usermodel-property-mapper
d49ed22e-1a09-4f3a-8e2c-c5b99536b7e6	07ca313c-3c6c-4b28-bc13-7fd2cef02fd4	allowed-protocol-mapper-types	oidc-address-mapper
aaa2ab73-69f3-4ab1-9aa5-39c24c8026da	07ca313c-3c6c-4b28-bc13-7fd2cef02fd4	allowed-protocol-mapper-types	saml-role-list-mapper
e55a0d18-c325-41a4-a6f9-34438d14c8a1	07ca313c-3c6c-4b28-bc13-7fd2cef02fd4	allowed-protocol-mapper-types	saml-user-attribute-mapper
b5818856-23ab-429d-abcc-f13b07540797	07ca313c-3c6c-4b28-bc13-7fd2cef02fd4	allowed-protocol-mapper-types	oidc-usermodel-attribute-mapper
32f53101-1079-4d4b-b68d-6dc6efe640ca	07ca313c-3c6c-4b28-bc13-7fd2cef02fd4	allowed-protocol-mapper-types	oidc-sha256-pairwise-sub-mapper
b813555e-7678-4bbe-8b55-03201560f6d0	448bb02e-d097-41bb-bb7c-96a0abfd6383	allow-default-scopes	true
3e4dd65f-c1fd-4bbf-8b59-70415cecebc6	d67068cd-10f0-413d-a571-c4dd3d55f0f3	host-sending-registration-request-must-match	true
38d52515-4e1c-44cd-902c-0700cdc04f63	d67068cd-10f0-413d-a571-c4dd3d55f0f3	client-uris-must-match	true
70d1d911-a620-419a-bb67-da96e5853998	27618a8d-fa0a-43d6-8e59-d516c9d6e3b3	kid	68524a1c-22e5-4a37-9dd9-e3fccd4c7952
023458ab-c29f-4c18-8b4e-4070ed4c0cdd	27618a8d-fa0a-43d6-8e59-d516c9d6e3b3	priority	100
7eb1d637-167a-4781-a20f-5890755df570	27618a8d-fa0a-43d6-8e59-d516c9d6e3b3	secret	OCxNVLIvF2OSZtgUa9wRUA
77e38d31-3ab3-40aa-99ee-b25a3d9dd8e6	c6f077e3-b361-4a84-9986-87f3561915f1	certificate	MIICmzCCAYMCBgGYZPumTjANBgkqhkiG9w0BAQsFADARMQ8wDQYDVQQDDAZtYXN0ZXIwHhcNMjUwODAxMDkzMjUwWhcNMzUwODAxMDkzNDMwWjARMQ8wDQYDVQQDDAZtYXN0ZXIwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDop39rEu81bcIDiGE+geU+RkjIWwmxe8/BLEYsZwx4ZRPgbnpJfbKZM5VxcMZQNhiWVcDmZXhaq9kNQNuCu9Af+bt7zcisKIRo4hrgx+6Nf/RSqCofvqwkBTwkbD7iXhGwuelEje3Rn1cncn83098NyD7ZWvODdHMoFZa+cohAu5ac2CIHf/RKO5UAXkqyXfujS3SU00c/6MjQnIDVuCDzZIoiJGECZYSsJG59ZxYDmwOsNkc64V9a95KIC6X9BzKELa+LkGK1aHLJkxbS/8rJjn/8fP6BTOnJfeCodCPfLChmfwEM4rW/QHWznIsl0X3binFP2CRNiR/SZQbsgWJbAgMBAAEwDQYJKoZIhvcNAQELBQADggEBAMosNpHhwSMoudtql/t4OiKDuj/Au6doysfRVaYrP+BSU+HmsulUF8o+UTDY97OiReSq4XiCKvNV2ngHjWemHsN5HnkubLUBDHrFX1gMtzk9uFNWilG0n3Krxpu+UySxizmT0jo/Jxu+5rP4Rc81EMRAfTz9EvuZWsVU/T17Ghu0a73IHwCgS+iFDaVnSm5d5XUhntsQBn3aAz7UYhfNQGQygpbIJBUOfrN+4W77AZ18bXYfvDNNsEjyvL3LLEszDQsYQBkdFu5YiHWQvDD0ZzEzXvWZIjRqjdpien4qXwSzmU2fRBaYcp9cw5UN0CwopNMm3YoklhmVRMZri97Tlf0=
b66e283d-0ab1-4ffa-948b-51e65738b685	c6f077e3-b361-4a84-9986-87f3561915f1	keyUse	SIG
22bb10d7-f6e2-4575-b5b7-5dee612ed536	c6f077e3-b361-4a84-9986-87f3561915f1	privateKey	MIIEowIBAAKCAQEA6Kd/axLvNW3CA4hhPoHlPkZIyFsJsXvPwSxGLGcMeGUT4G56SX2ymTOVcXDGUDYYllXA5mV4WqvZDUDbgrvQH/m7e83IrCiEaOIa4MfujX/0UqgqH76sJAU8JGw+4l4RsLnpRI3t0Z9XJ3J/N9PfDcg+2Vrzg3RzKBWWvnKIQLuWnNgiB3/0SjuVAF5Ksl37o0t0lNNHP+jI0JyA1bgg82SKIiRhAmWErCRufWcWA5sDrDZHOuFfWveSiAul/QcyhC2vi5BitWhyyZMW0v/KyY5//Hz+gUzpyX3gqHQj3ywoZn8BDOK1v0B1s5yLJdF924pxT9gkTYkf0mUG7IFiWwIDAQABAoIBABLEyHOz4FZYF2m/i9DGG99NPjvjlEGcMmNsFfEpEwl3ZgYaA4WAPXM8hRbUuTrpUkx2eIxdrTrrWCRIkizWx/B8bhKLxF+XGS945iAyIN2LcANnC5bqA+sX/QUXMm+ClTzDkrz0EjZx+070Xqz7S6hJ+kAApVp6f3vBWhrdFH7cO+KEePHgcmXf2Qt1rCRzquBC+a4/xShSoFMUFxlhn+4cBwepKSvQawWS8ZjKRXE5a+v5dHhddZHGHgpVkcE0T+o2uIbmN/5Cxxg2GOPLyiY/V9HP7aMVccg5Lq7QDnd60yZGE6Wg2wG1QOUTAsfYotYa3IzZkEz8kZKGG77PePECgYEA+2JAJs/4d/rml+4fRwlFrK4AiUqR08jVpR0E7TJiXvAxwoj62fPZuSGVXJEki5ZOoBFx7/bYRdI0/2yPnXKmNLFwMT4tRv/R5/uotnlks9A/ihddYcFxxLyNKtJP4JN3vfLd7CI03UHBi8ycJqSDYPY7Oi/+235yIfiZi87IPskCgYEA7O0zPnTPkhVosx44QTJ5Ow+LOXqCXQq4RV5BO+zmOuIhDQxxS4yOd1aOh3Pkfx712rM/44yStuPavtuWkEZL023+lVIMPy2ur9jAogN7In8FvsjoDVOuKx6EyQLlG9r3pELb0d5Ot30MIKZ2HDhQpWf2hn/1UAufOGMI0R14dgMCgYBhGCzREOjWZqlkK0wXYuRvcvGwFYZHt72VPaUmfJx0MO/3RHMndcE1eVw4WbM02wRZXQOV/NDB/xQLSAIcfSjnF1XPH0Xbs3l+0bvzZuXXceroymd+F3p605zu2jaR9kN8sW64AupKP1ijR49UeZxo+hIcQmd8ndU1e9DKqD69yQKBgQDYt8P27kxsGK8zoIHeiTfBIV+9jOUwDD0wHp5XHUQfOnb3A5mifIhYetFW71hkO0Kx8OKQ1yXgqx2NFTls8RAs7uE/XtSa+cNfUEosKDj6/euYbxdsEU4wXPMjF0XQrAN3ujxuhQXF61kriHhjKdQC6MzkjKksteTHZDdNaMKmVwKBgDNHlu2xALs89YgDzpYfLqst7xkoB5DW9v4mYPW/hgjFB9KaDwzps7JRtPCed/v1UwrZrNr1FjgxIJsC0fPjXgvS5fxFos1h2obKL0WHChLUvS8ufA2TLSc1rUmiH+jxByuBP4lb5C17FwbpYlF908yv1UMLS1VR6T8AvX2lFxTe
000f79bb-86dc-4d4e-b3c7-a0602169bf24	c6f077e3-b361-4a84-9986-87f3561915f1	priority	100
e330dfac-1f12-4fea-938d-8b20b6829cc6	f389cdc8-7642-4d71-aae1-3960fcc8a13c	kc.user.profile.config	{"attributes":[{"name":"username","displayName":"${username}","validations":{"length":{"min":3,"max":255},"username-prohibited-characters":{},"up-username-not-idn-homograph":{}},"permissions":{"view":["admin","user"],"edit":["admin","user"]},"multivalued":false},{"name":"email","displayName":"${email}","validations":{"email":{},"length":{"max":255}},"permissions":{"view":["admin","user"],"edit":["admin","user"]},"multivalued":false},{"name":"firstName","displayName":"${firstName}","validations":{"length":{"max":255},"person-name-prohibited-characters":{}},"permissions":{"view":["admin","user"],"edit":["admin","user"]},"multivalued":false},{"name":"lastName","displayName":"${lastName}","validations":{"length":{"max":255},"person-name-prohibited-characters":{}},"permissions":{"view":["admin","user"],"edit":["admin","user"]},"multivalued":false}],"groups":[{"name":"user-metadata","displayHeader":"User metadata","displayDescription":"Attributes, which refer to user metadata"}]}
7c189abe-452e-4958-823c-92b3f099e07d	275ed2ca-487a-4ee9-a9ce-12b557ab032b	privateKey	MIIEpAIBAAKCAQEA3V6/KnmZAYP8AmqDlDBxjWdamhBXce7DI5id0UfCHblt5naUpUackEab9rV+DY2MZYC0TWit8AsjOmnCa+FysVxevkxQtBiAIIcQ1NbYVnqY9ZecuL1gGcUnMvM/hCQ7nZDmz1PYhgXg4vrrn31qle2o7mr7Wy6ysypqM+3hT0ntmnVo1HVjwgNJ/mUBWAPISbMhhEX/F3jHo22mtdLpll2ma5H7N+8XTDzSv88mfw1Awd6VkyPK9k6GrOEAmoTFS6rp1Y++RGEXeHZrCouVOK2+XEhxcI8XnBe6pf1bK/tXuHrfusDsKoe0enLX3+3kzXoqAu1LRuIIiYuOtDnsXwIDAQABAoIBAAdz/a6x6l9dMLpqGKUAHoqavLesLi2FUuebZ1DZACWdDSCxxF5uKq5G2lBrmuNc7SmrtCZZDGYlEdoRHn0gWYq0J/JzQlJV8tZlhcEMvLropMa8BIhJw/cKJsA15WEPETzEWKkJUyRsBT7PIOi9Qb3rgwEH9A+FWnnixzGZZDkVnZJRq1yeEYHYGQ9EdMdP9U2l7/vTc1kLNTwIKPazLcy1hdoUUXXGiJQtBqZvBU2Zt0nYhQkEB9G7cS0xP03jFqJF98/4VLguai3dfxvX3sqOC4hOm6G1dlWXS0PO+o+4oO95j1p5eAPukjIadcZY39+E/tBSY+Ip6gURNJ9x8MECgYEA9w0T0DYO4xq3AtLWQ21j+C4UvFiE3ICH1WgIKBs0AzXV2e2Q2v8/oiKQnwYtClMnl1D00Gj5PHChded3H+qNl3lkrLTDRy4Neqvcayh//jBJkIimyhV0LqFTxPWE01+hqtKC5sDYxYsAkQ33gHffHn+iwlq9/kKpQ8XraqfgWJ8CgYEA5WOHG6GKgzdTXc9RtaXavPO81drHjdn+qZM8iioQXkwwfG0Lrd876vUpPBVT8UmnfNLqP4EsmzIrMNXW4TGhnHabJ/Yk95Qxk5nhTCCVO6zP1hL2agCKOiFE/dteyfcGH0hpsWMoiyqvUyVmpVY7L1+Mw8x5wy1uwUkjr7hJFEECgYEAqD8ntyyZhuCysiGT+eeNpX78cCh4kxKVbyiiJW4hRe5r7TZ06C/czjZr92DlW5q1478meqiC1D6ANyOjZQD/3KC3h0azls4uL9TLD89NG3cXIm9t50IlFXyY6BGsm6cNGqCytSOwfRflEuRIIh2Avex+k43oxjfvvlATu1cI2lECgYEAwg/VeZbMq/3eVXrTUa2ixEAf/Xw9XtCSFbwud2TmvCAQ1PMb8ljtQ/VskRwqC/OX8o4tBtStfA6RY5tFUva+FrRwxvoChEGnP4vrgo/+MKIeoqkgNsWGK2+CApHYO1z0nPqSEDQnVLUj5l+52x58q7NWTz9ps6l4DaVmv35UtQECgYB7Q6FHx/aySmby39SwWnKBH93NwsfW9zaCgnC1/acmqvJoNIOf5vBAL0O+QFa22I1H1d4u9U8yUmQ+xTVjdEiNz3cJIc32ho37bc9TAjR86chpk9WChDOcvSjqCljNOHaqlS3a8z4oncFGqK00hH+qv/+l9Zrd9PXylT9pt0Hayg==
b45e186d-24e2-4cec-bf8c-b000a55a5405	275ed2ca-487a-4ee9-a9ce-12b557ab032b	priority	100
cf8c7c5b-e6da-4a6d-881d-90886a5f5160	275ed2ca-487a-4ee9-a9ce-12b557ab032b	algorithm	RSA-OAEP
5c94e26a-796e-4edc-974a-6a5063d9ff81	275ed2ca-487a-4ee9-a9ce-12b557ab032b	certificate	MIICmzCCAYMCBgGYZPumgTANBgkqhkiG9w0BAQsFADARMQ8wDQYDVQQDDAZtYXN0ZXIwHhcNMjUwODAxMDkzMjUwWhcNMzUwODAxMDkzNDMwWjARMQ8wDQYDVQQDDAZtYXN0ZXIwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDdXr8qeZkBg/wCaoOUMHGNZ1qaEFdx7sMjmJ3RR8IduW3mdpSlRpyQRpv2tX4NjYxlgLRNaK3wCyM6acJr4XKxXF6+TFC0GIAghxDU1thWepj1l5y4vWAZxScy8z+EJDudkObPU9iGBeDi+uuffWqV7ajuavtbLrKzKmoz7eFPSe2adWjUdWPCA0n+ZQFYA8hJsyGERf8XeMejbaa10umWXaZrkfs37xdMPNK/zyZ/DUDB3pWTI8r2Toas4QCahMVLqunVj75EYRd4dmsKi5U4rb5cSHFwjxecF7ql/Vsr+1e4et+6wOwqh7R6ctff7eTNeioC7UtG4giJi460OexfAgMBAAEwDQYJKoZIhvcNAQELBQADggEBAIvk6apHHg/qRwcaybTomM/EaZVQjo//Dd3VnEYGKvB2Q1JW+9dNaH/Z2mvyBKYkvA1MxFjp6ev6tPb570rPL1YyLHaRSoeDIBzDenwwJaORKyL6FNOddofLexgJC9unk2PoQjn92zDtD9a1f2os5S601DYR8+SFxOSBZB/KLxLePSg47CfK/bPmXmTWx8nfjsxZlZnqTfCh6J2PdZSd3JBSViiHv6FS2bQR/6vKDV+mYYukpkBNVdU+fMXH7t4zw4FNJe+RtPy7guQOpEwKpN2Wa7u9pM7L0u5XH3rD0l0tQAK+2uiDOrciGBfoW5x+4zNUeyMMng58WneDrbsQMak=
60af0dc0-495a-411f-be08-c8033c38d8a2	275ed2ca-487a-4ee9-a9ce-12b557ab032b	keyUse	ENC
05894dfb-a685-44bd-9d17-356b808609d7	a7c56771-9859-41c0-8534-85e6629beceb	secret	jIbA91aMaSMZyT1tD_61Vmq96bOddYPqDh90teIfR3c6RQ59M66xc6XfGu0j2746QffRPYIveof0HzbrN5-4Ts5YX4Hl1_Wn5OPh6pOSAErQx5VJmkPfCbh7Y8JEOqS6qTmH2-g_rJjfqV5ll8xCB7duAegZWTOL-8eK7OobqGU
fe9551e9-54e4-426f-bb32-cd55702b0f85	a7c56771-9859-41c0-8534-85e6629beceb	priority	100
0e0f11ec-0e39-4043-bd3b-9b8fc4972dda	a7c56771-9859-41c0-8534-85e6629beceb	kid	ab8faa88-ac73-431a-99d3-b2e503be778f
b55186e4-77df-416e-bb94-598b80281d86	a7c56771-9859-41c0-8534-85e6629beceb	algorithm	HS512
03470178-e1d0-4ae9-8111-f3fc453a7591	6fe3cf7b-426f-45da-ad1c-aaa1f94308bc	priority	100
632d565c-a1ec-4f5b-a5c1-5626d27f117c	6fe3cf7b-426f-45da-ad1c-aaa1f94308bc	kid	b8a60f41-1801-4a91-8d15-20f6b898b1f8
5eadcae2-1224-4870-9ee4-5cb17985023c	6fe3cf7b-426f-45da-ad1c-aaa1f94308bc	secret	rLN7RCVKbA76FRRDGbwFyZjYYG9bZSK2rZkWQ2yjHdKrK7z4U4WsTG_dHUUnySC90kchAmPJuRVuu653VUB4xi1RyBq1DR1rYBea41upyy-iCW03sWyp0TFlQrK73n-kJWiFbKHQ94K6Hd1AKOV8rz3Ar7MnEsWQVssX7tO3rn4
e15014db-0994-4a53-b445-6884a89711df	6fe3cf7b-426f-45da-ad1c-aaa1f94308bc	algorithm	HS512
c95283e0-b9c6-479d-8c68-ec5d712d9247	d42c0ae4-569a-4c6b-ab15-118854624a49	certificate	MIICmTCCAYECBgGYZP7ENTANBgkqhkiG9w0BAQsFADAQMQ4wDAYDVQQDDAVyZWxtYTAeFw0yNTA4MDEwOTM2MTRaFw0zNTA4MDEwOTM3NTRaMBAxDjAMBgNVBAMMBXJlbG1hMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxtRAMwBOHn2lz+cYWDCBSYZUfCLJTRnx/tkmy2TxxrFfEl1Fn45X+2ueziQEllgiVxVSfPfX0Q2CraeqYNkKdGVJeemu0JZXtzAQnzYReUfrZwybdhaf579T21g5Ihc3MC7H+NNU3IGruQCtH77iHqG3U9gwWP7MNmRSfGVAOce9BjCSxhs37yQsjiD+ZeGc6Zl/HGFdzezNvzDKjewbdDt7M4HhlM4YNSWPy8Xisgk6mV3okhNXFhVEZvaoRwbyli/bJ5OTGgWmu67PCT5edT8DjNhAwCs2dAk74WPyVR1xImejp8usPVkigqRJFuX7qBMqexBEyaBxLRBz2Zn1PQIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQDEXRhnpMxaUqyIabmxVxpLG5Fbhr1jQowDgk9s5biOtYv3WfPnQGJL34priME0gBq2S9JjhnsFnHU4Wp+UZm+koGW05zfeMhNyZmmn60OSBHUj4mNpISGeho3cYrP2lCQBIVoiFQ3A310aNoYFWViPJAqR10y1I0eDkcJV6kcCkG4oHhJyOqDx0it6DvTBCDJVYQARtd9iAzDQVb04Z4mMuyM/BTv+DMXfO6R6YMZvg7MFQN6Oi1RSJn4kM6qEsxLuY8R0wrS2dDauMNYjY2ZEOoSDOr8BYCJ2ZEhTcfEfbDZytWXtlkOEhuUG2qJ49qbZEJBNMsahNN038IbeRj1+
9c134d7a-2c0b-42bf-ba7b-a9515bc137f9	d42c0ae4-569a-4c6b-ab15-118854624a49	priority	100
712fd048-16c8-43f8-bc8d-7e7f4c082894	d42c0ae4-569a-4c6b-ab15-118854624a49	privateKey	MIIEowIBAAKCAQEAxtRAMwBOHn2lz+cYWDCBSYZUfCLJTRnx/tkmy2TxxrFfEl1Fn45X+2ueziQEllgiVxVSfPfX0Q2CraeqYNkKdGVJeemu0JZXtzAQnzYReUfrZwybdhaf579T21g5Ihc3MC7H+NNU3IGruQCtH77iHqG3U9gwWP7MNmRSfGVAOce9BjCSxhs37yQsjiD+ZeGc6Zl/HGFdzezNvzDKjewbdDt7M4HhlM4YNSWPy8Xisgk6mV3okhNXFhVEZvaoRwbyli/bJ5OTGgWmu67PCT5edT8DjNhAwCs2dAk74WPyVR1xImejp8usPVkigqRJFuX7qBMqexBEyaBxLRBz2Zn1PQIDAQABAoIBACiklXT3cICKFZ1gc1j/nUX+yAFSCQ0SKyX5lJUKDuQbOMBjwYoiijiSILWppihnXqmnaaOy8/I/TUGbHthKNXlihPj/PCVWPhrrn9IztrGwcl2vtY2XhVtVgFo7d/5QeY8HWQgzhsrGVOhty3XMSqORUgy5CfT/LJZHvpbACbdlkHyEv2Ncqd7KA948rYmQrhEDgzdfuU7s9nF7ilalst7YgQLNWDTQKn73A8MALjyF5WP2vSNyOZ+kGAk+XyLEKhsHUkLJ5fLA79ovK1luNOaeFhhNSSV0cWXo0KXKydX3GWkqFfPNhyOaFMAZIzYHk1x7oYHW0paCJgT2abutLv8CgYEA/0sr9V97LbaUZ7xzOLQr5Or0nKBGTqowlYzk+UghzqGzBHxfBx/LETbIcL3vB2bMR8DUqr8HgZMRCWCuKNatZE0aNB0Ffxcwc9FrLPbKu74SPh4TBLWD2W2OySvHXs0qlCEpP5LP4pHa/jizsmucK7GH4vByjCoxtZyHgCdU3BsCgYEAx2EVmt3lWubrKCeh3xEg+kggxsweQamD9EXdVHcQM8JtGgnErBNMSYBpvSCWNtzSL4fYCZXLHlS6rzQjXGFB1Xbt+ch7IGkDacHHxkECxz8jlcacAcDNsyUbUe9EICwBNRNK7K23YvQ6jewr+ZEkHvrG5rRuTWwBI6u9FC5F2YcCgYAx1oD8Y+luISiTn8x2k2XGSL/8G6XeEYduipTgvV4sMpD6HTWH9IL2F1XF/K4/KahTkadytdxHRWicBg251z7+R7cH2WqLRwGkeouLuZoWUKLLEZ/w2Wal+adYSn2fWjontNmSmZyPxJ2V54fdGvjf5sZ6XSpfhpUxuR8Su5rrZQKBgB11P67xCBklMytsp6MAseGo1Ibq07GZ7TAjPm9jZBZxrJMDTHasJ0JY4caa0PbsK3XwWUAZTLh/hlGqn2rfx81VVBXqxB5mxaVTh0PIiTONJaHUhWpQ5VOJsP6rHmKufcMsxY4woH7TXO9pLJokErUtEkQ19nIj+gseLcKUj6dzAoGBAIClKpfLg8Ss1Ey2jFRQyAhCU7CRyPulBpqkSxlpxLygHOj3bdfE2qMDLHNwZdZ8zWNEb+BTe8JLZuUaUqPyLY8aXkyGj+pD3urpH9RA8EhBW+XyRladX8dlUoyvr3mJkS8tnz6hJBaZ5ZdZ+xPf5ZRd5JbQCcBVsjSyFN/Mve46
6a272985-f121-4d5b-85c4-a00c5a4c4cce	d42c0ae4-569a-4c6b-ab15-118854624a49	keyUse	SIG
2f1a8e9f-9d34-44c5-a131-db836754c860	4293f817-2107-4e0f-990b-bcb1fa37cdeb	priority	100
0b114f3c-b461-4e23-b113-d1670326dd2f	4293f817-2107-4e0f-990b-bcb1fa37cdeb	secret	2umeZtNpy1JXqLzxJQPyTg
bd5d4b85-0214-488d-b5d7-c27d7062570b	4293f817-2107-4e0f-990b-bcb1fa37cdeb	kid	e4d6e720-3220-42cf-b823-348dbccb8cdc
379fd8ae-5681-416b-a7e7-d847b97808f8	e819ba24-fd1e-48b9-ac16-677134392fa0	certificate	MIICmTCCAYECBgGYZP7E5DANBgkqhkiG9w0BAQsFADAQMQ4wDAYDVQQDDAVyZWxtYTAeFw0yNTA4MDEwOTM2MTRaFw0zNTA4MDEwOTM3NTRaMBAxDjAMBgNVBAMMBXJlbG1hMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxVQdeBGcrtKdczKh13RMXhi0elCMcs4CyS/KFwr/XfL7StKFXoT4oPl6HFYsXbyNVgsqX8Fn3+t+LU/1s79359MMF1hz9crhRE7LUnkro0Rub0GQlOIG4Jsy/G7Y9644kDzqm5OtV7C9mH2Q21BmzWvChhqH1e3fldlYbd9S2D/uekd45tBVGYW10WJnfH1VIg9wZYIe9TJr5Ldg9YrQa1bPo6xctpArqheZNDSmD/01v+FyDKgsFYdoXpgiKO1IEDjBNLnk3lGCTqGit+50OhExItb33DSQfseoxHmLuUzsNtWMqNWiVHSWAwVkN7GOdpzqxprRyRihSOpmncY5rQIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQCiTqYJCgkWqdim6htJz0ZkX1tXrGnHzyF9eVzRa8R1XOJfug0XL/pXIFwHipAhYLBfBmD66kubADznrgiD9mArLB5FYBZsAY1Zf0DRXrQM/DL2R+o63+uIo0ixnTl4bRpEEP4kmCJL09O7ijQYzeTR0qW5o4fGJRKDWVEf7a5ztrd3AeiQRwpS3pUEfA/NQACYw0KwpqVFhfAR9FlQKcP4khwdS17H8p1vkAIHhY7rJkeljzT+5+0lKNJajB946l0Iot2lPbTisxj3QWB99DPSA+1oeBgtAT5LA8/bvxwBZCqSUOJfoHJWF2apspnVOhyASnlDDZVUZRMeEaovzpGg
30800bfe-baf2-411e-8901-cdd62576d058	e819ba24-fd1e-48b9-ac16-677134392fa0	keyUse	ENC
3dda58c2-c3f7-4e59-a7f5-07f3b8398324	e819ba24-fd1e-48b9-ac16-677134392fa0	algorithm	RSA-OAEP
1f61e11a-897c-4432-857a-b45015c1d25c	e819ba24-fd1e-48b9-ac16-677134392fa0	privateKey	MIIEpAIBAAKCAQEAxVQdeBGcrtKdczKh13RMXhi0elCMcs4CyS/KFwr/XfL7StKFXoT4oPl6HFYsXbyNVgsqX8Fn3+t+LU/1s79359MMF1hz9crhRE7LUnkro0Rub0GQlOIG4Jsy/G7Y9644kDzqm5OtV7C9mH2Q21BmzWvChhqH1e3fldlYbd9S2D/uekd45tBVGYW10WJnfH1VIg9wZYIe9TJr5Ldg9YrQa1bPo6xctpArqheZNDSmD/01v+FyDKgsFYdoXpgiKO1IEDjBNLnk3lGCTqGit+50OhExItb33DSQfseoxHmLuUzsNtWMqNWiVHSWAwVkN7GOdpzqxprRyRihSOpmncY5rQIDAQABAoIBACcqzEOgGU3BC9QJBYM2uA7IUBBkKxCBkB9/XxlGS0xKVbMzCJprgc8v3ZtAxoMfkR02T6WchcH2JAAZ+i7AryviYejDW5b6u05b5nMbDnyUcWgfZzOKDqvn2k2tMd8to1gL41RYkmRQtj1NlSJW+eTFwiFspDAkDEhZ0PnUBUVbR9gwZY2gVsGngnuooZDfizwsGtj9ZxDBTIDdP+gFoe7mnWnJmdJWJnlnm7g+1hQW+TkofxHugxp8Q+YXamEF63hU+h1tAOCgKza0CxGRmRJxnOZfUvXcEVrze4JKS0Lrt3Rvh0hXGLoLAjH8uXdJWmBxZOkJUdxgwsvftpL8ZzECgYEA7pTRlf9YuNy1bGfetbf2F92u9r1MK4SKN6wC+trYtthZoDZN349mkVMA5kF/bURp9wkY6yYTLKHYPFt9iwslFeIJh6Nk+c87RbVoZB1WXUyzSnMLDhIPDAd5pkS4lGwFX1uqJFAzLuExvUgiowOWZ8NzxqChmy0OjnrlZ17JN50CgYEA07xEESk/x+1IQJKWwTskLIUuREYRRskxvbxm53vg+SLhyOb3Ku1QcdpRJopVup/a/c6vXyTbcOp6qdzKeW43px4ryrLMweVjq40Kqro+7WgqTH2eUYItSWgSITlE+24vQMVdWcah44drIGJjH0XZ2a6VsWvY2hS6ddI0yl9O1VECgYEAg6t8M+U1yOh98/MrVWzOk4NUEXaNeCLwySLUji+DlP24ERSHS89vktqVT35sXhh4xs6tgvGDeYlktoLjuX4QHJCgI8yIaNn241Tn5QDP7jPrazYsrrpGtaPWfm3E1Jz9JhCHyPy+EqxT8BvEieTr+B5FYiS6q9JlCztGKhOEx30CgYAQyKywm491vqZqj/QpTxHYAZqgSNmXvd0WiDe0A8ralNxG+5/+JB+rVr32CyMujvtiAR8/sIFAmyWsD1dxdr4sniSkhbXbbN6Qu7ZrwCW61fX8aDzDg0kpt3kwbwS71YkjXkG/GxK8IYDpaUMoOHZDXiZ5kO34o4oASvBqVlG+cQKBgQDfdqy7lRtAJECcM8lrIWfyl60j1hmZPTlYpDKn5SyLYxTT96LpN9d3vlyZxDWXG9qlnp2sx1dFT69ZaX9tVcJlXU49U0Kg4lRSediBZJgU35sHRUEbBcWc+GeuREmF5KTExOk+fU5DoMDyCIcWzdSbTSlyc72gn3SoRptto7oOjA==
16a25218-0e9d-4be0-947b-406effa65f03	e819ba24-fd1e-48b9-ac16-677134392fa0	priority	100
461eb8fd-4cf2-4233-beda-393fb8517fc5	62d45534-2a0b-4d2e-9ed8-4760c488901b	host-sending-registration-request-must-match	true
df059c7e-32ae-4786-97fe-6ae582a66874	62d45534-2a0b-4d2e-9ed8-4760c488901b	client-uris-must-match	true
3a3c1f57-1f4e-43a0-8ef6-a51db3233d45	09772419-7748-496f-b84b-899a7eb9b955	max-clients	200
e6832353-81ed-4978-abac-3fa7ce0738ec	6989b996-7433-4076-8ae5-7306a575ea2a	allowed-protocol-mapper-types	oidc-sha256-pairwise-sub-mapper
5ddfe37a-6a9d-40d4-925a-d5ff7121a79f	6989b996-7433-4076-8ae5-7306a575ea2a	allowed-protocol-mapper-types	oidc-usermodel-property-mapper
e5eb7d4e-366a-47f8-8e38-2a29d6198e6c	6989b996-7433-4076-8ae5-7306a575ea2a	allowed-protocol-mapper-types	saml-role-list-mapper
199665e9-b958-4a67-838b-7a5b40fb950a	6989b996-7433-4076-8ae5-7306a575ea2a	allowed-protocol-mapper-types	saml-user-property-mapper
1c92d01f-d99c-43ee-9bba-4cf77cbc1847	6989b996-7433-4076-8ae5-7306a575ea2a	allowed-protocol-mapper-types	oidc-address-mapper
95653f17-d76f-4431-8764-f74cff27ac16	6989b996-7433-4076-8ae5-7306a575ea2a	allowed-protocol-mapper-types	oidc-usermodel-attribute-mapper
dfb9284d-87d2-47c3-88e3-9d7df76e11c6	6989b996-7433-4076-8ae5-7306a575ea2a	allowed-protocol-mapper-types	saml-user-attribute-mapper
206add80-74a8-4dd5-a970-9eada639d77c	6989b996-7433-4076-8ae5-7306a575ea2a	allowed-protocol-mapper-types	oidc-full-name-mapper
02937ec5-d678-4e6d-95eb-69f2ff5a4c00	705c5b86-ca1b-4abc-9cc5-68d2a33b4131	allowed-protocol-mapper-types	oidc-usermodel-attribute-mapper
9ca88ad3-d2c0-435a-8390-f365d0a95b53	705c5b86-ca1b-4abc-9cc5-68d2a33b4131	allowed-protocol-mapper-types	oidc-full-name-mapper
04695a00-28db-4a28-877e-8fe60c469653	705c5b86-ca1b-4abc-9cc5-68d2a33b4131	allowed-protocol-mapper-types	saml-user-attribute-mapper
bc5f616b-1445-482d-9413-d4d43b9833dd	705c5b86-ca1b-4abc-9cc5-68d2a33b4131	allowed-protocol-mapper-types	oidc-sha256-pairwise-sub-mapper
3d26be4a-eec7-4b9f-bdc5-d5269e7ca9d0	705c5b86-ca1b-4abc-9cc5-68d2a33b4131	allowed-protocol-mapper-types	saml-user-property-mapper
d8998081-86b3-4c86-a556-d7335ae21d67	705c5b86-ca1b-4abc-9cc5-68d2a33b4131	allowed-protocol-mapper-types	oidc-address-mapper
26e4f531-166f-41e6-a286-7b000768a526	705c5b86-ca1b-4abc-9cc5-68d2a33b4131	allowed-protocol-mapper-types	saml-role-list-mapper
115e6bf8-cedc-46bd-81b4-57ff554799b5	705c5b86-ca1b-4abc-9cc5-68d2a33b4131	allowed-protocol-mapper-types	oidc-usermodel-property-mapper
a0041c0b-8e2d-415a-b784-a738df699c6d	cce130c9-3a19-4aa3-8b3e-2976107312cd	allow-default-scopes	true
209cfaf2-3066-49bd-98b3-d0851ab4081c	53cd1630-d5d4-424c-b4e4-59708f069371	allow-default-scopes	true
a0eeac6c-931d-4a37-bf30-34a60a8bb0b4	5480c506-17e5-4518-b843-a945f4140128	kc.user.profile.config	{"attributes":[{"name":"username","displayName":"${username}","validations":{"length":{"min":3,"max":255},"username-prohibited-characters":{},"up-username-not-idn-homograph":{}},"permissions":{"view":["admin","user"],"edit":["admin","user"]},"multivalued":false},{"name":"email","displayName":"${email}","validations":{"email":{},"length":{"max":255}},"required":{"roles":["user"]},"permissions":{"view":["admin","user"],"edit":["admin","user"]},"multivalued":false},{"name":"firstName","displayName":"${firstName}","validations":{"length":{"max":255},"person-name-prohibited-characters":{}},"required":{"roles":["user"]},"permissions":{"view":["admin","user"],"edit":["admin","user"]},"multivalued":false},{"name":"lastName","displayName":"${lastName}","validations":{"length":{"max":255},"person-name-prohibited-characters":{}},"required":{"roles":["user"]},"permissions":{"view":["admin","user"],"edit":["admin","user"]},"multivalued":false},{"name":"tenantId","displayName":"${tenantId}","validations":{},"annotations":{},"permissions":{"view":["admin","user"],"edit":["admin"]},"multivalued":false}],"groups":[{"name":"user-metadata","displayHeader":"User metadata","displayDescription":"Attributes, which refer to user metadata"}]}
\.


--
-- TOC entry 4156 (class 0 OID 16418)
-- Dependencies: 220
-- Data for Name: composite_role; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.composite_role (composite, child_role) FROM stdin;
4afc773e-1b6d-43f3-8b05-003cf2f29f59	4471eb15-347d-498b-afab-ed77573880f4
4afc773e-1b6d-43f3-8b05-003cf2f29f59	2b6df356-d878-488b-8f7c-94f3efa1d778
4afc773e-1b6d-43f3-8b05-003cf2f29f59	315a3a83-49d7-4eb0-a367-1f88ac9adcd1
4afc773e-1b6d-43f3-8b05-003cf2f29f59	d2bbdaf0-ea14-46f7-8cb4-21287d3031ad
4afc773e-1b6d-43f3-8b05-003cf2f29f59	9c453c82-9b85-44d6-94a7-21f7a6513fd2
4afc773e-1b6d-43f3-8b05-003cf2f29f59	fad21920-7b7e-4383-8a9d-c15fd751b3c1
4afc773e-1b6d-43f3-8b05-003cf2f29f59	5836db42-308a-495d-8a62-19e175a76bbe
4afc773e-1b6d-43f3-8b05-003cf2f29f59	d2cfaad6-f23c-4cff-90af-6925482d1832
4afc773e-1b6d-43f3-8b05-003cf2f29f59	ce2fd79c-5f14-4c1b-bc91-bfa3b672d7d2
4afc773e-1b6d-43f3-8b05-003cf2f29f59	c81320e7-0cd8-4eba-8708-51d4002c0319
4afc773e-1b6d-43f3-8b05-003cf2f29f59	1e29dd2a-15fa-469a-8b86-61f9ac3bcece
4afc773e-1b6d-43f3-8b05-003cf2f29f59	55294547-337c-4752-990b-0c5efdcd3615
4afc773e-1b6d-43f3-8b05-003cf2f29f59	e4a6b5d2-e78a-4caa-b779-745088aec4f5
4afc773e-1b6d-43f3-8b05-003cf2f29f59	34745d81-2d45-430e-ab98-d3e1fd43023c
4afc773e-1b6d-43f3-8b05-003cf2f29f59	938b5b0b-4c9e-47c3-9d4d-5ae026fae484
4afc773e-1b6d-43f3-8b05-003cf2f29f59	b55ac2b0-a257-4550-b41f-41cf252cd199
4afc773e-1b6d-43f3-8b05-003cf2f29f59	6a6995ca-e767-4bff-8595-dcd4fe7e8646
4afc773e-1b6d-43f3-8b05-003cf2f29f59	aa4998a4-1e82-4780-bfcc-914122c8ea85
24925405-e2df-4f44-bf4d-d5fa744940eb	e26c0213-98e9-456d-bb87-f7d312e3659d
9c453c82-9b85-44d6-94a7-21f7a6513fd2	b55ac2b0-a257-4550-b41f-41cf252cd199
d2bbdaf0-ea14-46f7-8cb4-21287d3031ad	aa4998a4-1e82-4780-bfcc-914122c8ea85
d2bbdaf0-ea14-46f7-8cb4-21287d3031ad	938b5b0b-4c9e-47c3-9d4d-5ae026fae484
24925405-e2df-4f44-bf4d-d5fa744940eb	af51e7ce-a9f4-4496-b26d-7484186e701a
af51e7ce-a9f4-4496-b26d-7484186e701a	09fca297-5f0d-47ef-9fff-1587d42f7963
f7e88885-32ef-4288-9bef-358615cb95b4	c2c3dc9b-97d9-4e3a-85f8-670569baced6
4afc773e-1b6d-43f3-8b05-003cf2f29f59	9bdd1a2d-75c8-41d3-a30b-5b8ab85446e8
24925405-e2df-4f44-bf4d-d5fa744940eb	2e74a5cf-4b39-44c1-af0f-10cd748b824a
24925405-e2df-4f44-bf4d-d5fa744940eb	a1f34ffe-8ad1-4652-92b0-56e379c12bdd
4afc773e-1b6d-43f3-8b05-003cf2f29f59	13cb3e3e-2b13-4fcf-a213-df43c69e6c25
4afc773e-1b6d-43f3-8b05-003cf2f29f59	54c661ba-df7d-44f9-9d42-4a1984b05929
4afc773e-1b6d-43f3-8b05-003cf2f29f59	82123da4-c05a-438a-9d70-ccec8c9c2929
4afc773e-1b6d-43f3-8b05-003cf2f29f59	ec643a18-1b51-43b8-9f1f-a24edab0bbf7
4afc773e-1b6d-43f3-8b05-003cf2f29f59	6ac69d11-4a22-4486-8b9f-b6693ae27451
4afc773e-1b6d-43f3-8b05-003cf2f29f59	4048e81f-1ad4-4204-b616-163d5be753a8
4afc773e-1b6d-43f3-8b05-003cf2f29f59	32a2526c-0e96-4fc9-a7a3-0d7aae8a6c7e
4afc773e-1b6d-43f3-8b05-003cf2f29f59	dce4698f-17cf-429d-a13d-c39fa854a95b
4afc773e-1b6d-43f3-8b05-003cf2f29f59	56a7543d-5aa4-4af0-aa08-efa12b77c534
4afc773e-1b6d-43f3-8b05-003cf2f29f59	0efb0a8b-382c-4ad7-8df6-d40840bf1d57
4afc773e-1b6d-43f3-8b05-003cf2f29f59	9976f021-4f64-4045-be49-38c34af66a49
4afc773e-1b6d-43f3-8b05-003cf2f29f59	71dc8d93-e088-4292-8286-9be391eb1051
4afc773e-1b6d-43f3-8b05-003cf2f29f59	c143823c-5d99-4e2e-b205-01490c945d7f
4afc773e-1b6d-43f3-8b05-003cf2f29f59	4aac88e0-a9b8-4603-9ea0-6383771b32a8
4afc773e-1b6d-43f3-8b05-003cf2f29f59	8cee43c4-0ef0-41ed-8932-3bd131f0688b
4afc773e-1b6d-43f3-8b05-003cf2f29f59	ed2ae8d7-cda9-4bda-9534-e4b08a5e620c
4afc773e-1b6d-43f3-8b05-003cf2f29f59	019ff951-c22e-4e14-80db-f12b8a695cf9
82123da4-c05a-438a-9d70-ccec8c9c2929	019ff951-c22e-4e14-80db-f12b8a695cf9
82123da4-c05a-438a-9d70-ccec8c9c2929	4aac88e0-a9b8-4603-9ea0-6383771b32a8
ec643a18-1b51-43b8-9f1f-a24edab0bbf7	8cee43c4-0ef0-41ed-8932-3bd131f0688b
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	471814d4-9805-436b-bee7-cde1b6f26602
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	0bb47142-0a8b-4f8d-a879-6fde07a1c14d
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	da3c4458-92c0-4ad1-b669-c29e08ddd60d
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	813bda42-1366-41a5-8981-a762cc5b9339
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	8bf6c28b-a4b1-4591-a3a7-e49cb02dcef7
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	c2e3792f-bdbd-456c-bb35-bff1ad0dc14e
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	94217f58-5cd7-4915-94aa-1e6fff4d6ee6
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	e6e31dd1-9758-4fb5-843f-3377668edfa4
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	3bf5a748-8765-4683-acfd-2dd6c9bddfb4
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	69db5f58-882b-4a6a-a946-5b5c59635c27
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	e85dc812-0a9f-432d-9690-e96f1981556f
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	fadc6525-7ee4-4de8-afa0-f95798e7eb9f
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	e5269f15-3b43-447c-bc63-7da9a6167e3b
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	ca29d6e4-9644-48fd-b0d4-9047ec73ff3e
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	ac948911-7f4e-43b5-b8c5-65d64a4a5a98
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	caef55ae-e2ff-429f-ba83-7ceac4f3ca0e
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	09cc5765-0d40-494a-beea-9471dcceae4e
76f8cb98-3d7b-47e6-abed-d3ebcd7203bc	6f706bc4-76ef-4c07-aa36-f9c48a92304e
813bda42-1366-41a5-8981-a762cc5b9339	ac948911-7f4e-43b5-b8c5-65d64a4a5a98
da3c4458-92c0-4ad1-b669-c29e08ddd60d	ca29d6e4-9644-48fd-b0d4-9047ec73ff3e
da3c4458-92c0-4ad1-b669-c29e08ddd60d	09cc5765-0d40-494a-beea-9471dcceae4e
76f8cb98-3d7b-47e6-abed-d3ebcd7203bc	fcfbb7b5-abf7-4cb0-a873-2bb4998f9514
fcfbb7b5-abf7-4cb0-a873-2bb4998f9514	ac23bde3-a9bd-4966-8efe-2c3c3b0f1f2e
f08e2a57-934b-47c6-8bdc-5322b4c4cd69	09d60347-7145-4e7f-9f68-d0ae2bc98203
4afc773e-1b6d-43f3-8b05-003cf2f29f59	1969f546-97e5-49fe-8153-0f02883c18a7
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	1db0683a-1343-44b5-97e6-2b1cc7cff357
76f8cb98-3d7b-47e6-abed-d3ebcd7203bc	94735068-d221-4f3b-bdd0-075e38c1d87f
76f8cb98-3d7b-47e6-abed-d3ebcd7203bc	73a69fd4-267e-4c33-9421-aa970a21f443
\.


--
-- TOC entry 4157 (class 0 OID 16421)
-- Dependencies: 221
-- Data for Name: credential; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.credential (id, salt, type, user_id, created_date, user_label, secret_data, credential_data, priority, version) FROM stdin;
17492f35-90a1-44d7-b006-9fdda6757be2	\N	password	ebf0197c-6d8a-4074-9cef-a1b7e6f34277	1754041360086	My password	{"value":"f8bPWS/sHH+eDuDmxrlKrSCFwhVQSn9ulfA0YQrRgFI=","salt":"LtRlhDtv7GkJ0Cp+CvhT5A==","additionalParameters":{}}	{"hashIterations":5,"algorithm":"argon2","additionalParameters":{"hashLength":["32"],"memory":["7168"],"type":["id"],"version":["1.3"],"parallelism":["1"]}}	10	1
eda63575-7bdb-4441-abf2-5532213cc55c	\N	password	5212ba59-34d9-477f-b395-6cbf89826932	1754042148748	My password	{"value":"YtvE99GvBJFSqlp657nLlkm72LJ+hl2cq9CWEWAPUOM=","salt":"lChaRxdHQNnnvaxtSG6CRA==","additionalParameters":{}}	{"hashIterations":5,"algorithm":"argon2","additionalParameters":{"hashLength":["32"],"memory":["7168"],"type":["id"],"version":["1.3"],"parallelism":["1"]}}	10	1
\.


--
-- TOC entry 4154 (class 0 OID 16391)
-- Dependencies: 218
-- Data for Name: databasechangelog; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.databasechangelog (id, author, filename, dateexecuted, orderexecuted, exectype, md5sum, description, comments, tag, liquibase, contexts, labels, deployment_id) FROM stdin;
1.0.0.Final-KEYCLOAK-5461	sthorger@redhat.com	META-INF/jpa-changelog-1.0.0.Final.xml	2025-08-01 09:34:21.753996	1	EXECUTED	9:6f1016664e21e16d26517a4418f5e3df	createTable tableName=APPLICATION_DEFAULT_ROLES; createTable tableName=CLIENT; createTable tableName=CLIENT_SESSION; createTable tableName=CLIENT_SESSION_ROLE; createTable tableName=COMPOSITE_ROLE; createTable tableName=CREDENTIAL; createTable tab...		\N	4.29.1	\N	\N	4040861400
1.0.0.Final-KEYCLOAK-5461	sthorger@redhat.com	META-INF/db2-jpa-changelog-1.0.0.Final.xml	2025-08-01 09:34:21.765309	2	MARK_RAN	9:828775b1596a07d1200ba1d49e5e3941	createTable tableName=APPLICATION_DEFAULT_ROLES; createTable tableName=CLIENT; createTable tableName=CLIENT_SESSION; createTable tableName=CLIENT_SESSION_ROLE; createTable tableName=COMPOSITE_ROLE; createTable tableName=CREDENTIAL; createTable tab...		\N	4.29.1	\N	\N	4040861400
1.1.0.Beta1	sthorger@redhat.com	META-INF/jpa-changelog-1.1.0.Beta1.xml	2025-08-01 09:34:21.792899	3	EXECUTED	9:5f090e44a7d595883c1fb61f4b41fd38	delete tableName=CLIENT_SESSION_ROLE; delete tableName=CLIENT_SESSION; delete tableName=USER_SESSION; createTable tableName=CLIENT_ATTRIBUTES; createTable tableName=CLIENT_SESSION_NOTE; createTable tableName=APP_NODE_REGISTRATIONS; addColumn table...		\N	4.29.1	\N	\N	4040861400
1.1.0.Final	sthorger@redhat.com	META-INF/jpa-changelog-1.1.0.Final.xml	2025-08-01 09:34:21.79722	4	EXECUTED	9:c07e577387a3d2c04d1adc9aaad8730e	renameColumn newColumnName=EVENT_TIME, oldColumnName=TIME, tableName=EVENT_ENTITY		\N	4.29.1	\N	\N	4040861400
1.2.0.Beta1	psilva@redhat.com	META-INF/jpa-changelog-1.2.0.Beta1.xml	2025-08-01 09:34:21.865802	5	EXECUTED	9:b68ce996c655922dbcd2fe6b6ae72686	delete tableName=CLIENT_SESSION_ROLE; delete tableName=CLIENT_SESSION_NOTE; delete tableName=CLIENT_SESSION; delete tableName=USER_SESSION; createTable tableName=PROTOCOL_MAPPER; createTable tableName=PROTOCOL_MAPPER_CONFIG; createTable tableName=...		\N	4.29.1	\N	\N	4040861400
1.2.0.Beta1	psilva@redhat.com	META-INF/db2-jpa-changelog-1.2.0.Beta1.xml	2025-08-01 09:34:21.870869	6	MARK_RAN	9:543b5c9989f024fe35c6f6c5a97de88e	delete tableName=CLIENT_SESSION_ROLE; delete tableName=CLIENT_SESSION_NOTE; delete tableName=CLIENT_SESSION; delete tableName=USER_SESSION; createTable tableName=PROTOCOL_MAPPER; createTable tableName=PROTOCOL_MAPPER_CONFIG; createTable tableName=...		\N	4.29.1	\N	\N	4040861400
1.2.0.RC1	bburke@redhat.com	META-INF/jpa-changelog-1.2.0.CR1.xml	2025-08-01 09:34:21.92668	7	EXECUTED	9:765afebbe21cf5bbca048e632df38336	delete tableName=CLIENT_SESSION_ROLE; delete tableName=CLIENT_SESSION_NOTE; delete tableName=CLIENT_SESSION; delete tableName=USER_SESSION_NOTE; delete tableName=USER_SESSION; createTable tableName=MIGRATION_MODEL; createTable tableName=IDENTITY_P...		\N	4.29.1	\N	\N	4040861400
1.2.0.RC1	bburke@redhat.com	META-INF/db2-jpa-changelog-1.2.0.CR1.xml	2025-08-01 09:34:21.930911	8	MARK_RAN	9:db4a145ba11a6fdaefb397f6dbf829a1	delete tableName=CLIENT_SESSION_ROLE; delete tableName=CLIENT_SESSION_NOTE; delete tableName=CLIENT_SESSION; delete tableName=USER_SESSION_NOTE; delete tableName=USER_SESSION; createTable tableName=MIGRATION_MODEL; createTable tableName=IDENTITY_P...		\N	4.29.1	\N	\N	4040861400
1.2.0.Final	keycloak	META-INF/jpa-changelog-1.2.0.Final.xml	2025-08-01 09:34:21.937489	9	EXECUTED	9:9d05c7be10cdb873f8bcb41bc3a8ab23	update tableName=CLIENT; update tableName=CLIENT; update tableName=CLIENT		\N	4.29.1	\N	\N	4040861400
1.3.0	bburke@redhat.com	META-INF/jpa-changelog-1.3.0.xml	2025-08-01 09:34:22.000723	10	EXECUTED	9:18593702353128d53111f9b1ff0b82b8	delete tableName=CLIENT_SESSION_ROLE; delete tableName=CLIENT_SESSION_PROT_MAPPER; delete tableName=CLIENT_SESSION_NOTE; delete tableName=CLIENT_SESSION; delete tableName=USER_SESSION_NOTE; delete tableName=USER_SESSION; createTable tableName=ADMI...		\N	4.29.1	\N	\N	4040861400
1.4.0	bburke@redhat.com	META-INF/jpa-changelog-1.4.0.xml	2025-08-01 09:34:22.030884	11	EXECUTED	9:6122efe5f090e41a85c0f1c9e52cbb62	delete tableName=CLIENT_SESSION_AUTH_STATUS; delete tableName=CLIENT_SESSION_ROLE; delete tableName=CLIENT_SESSION_PROT_MAPPER; delete tableName=CLIENT_SESSION_NOTE; delete tableName=CLIENT_SESSION; delete tableName=USER_SESSION_NOTE; delete table...		\N	4.29.1	\N	\N	4040861400
1.4.0	bburke@redhat.com	META-INF/db2-jpa-changelog-1.4.0.xml	2025-08-01 09:34:22.034595	12	MARK_RAN	9:e1ff28bf7568451453f844c5d54bb0b5	delete tableName=CLIENT_SESSION_AUTH_STATUS; delete tableName=CLIENT_SESSION_ROLE; delete tableName=CLIENT_SESSION_PROT_MAPPER; delete tableName=CLIENT_SESSION_NOTE; delete tableName=CLIENT_SESSION; delete tableName=USER_SESSION_NOTE; delete table...		\N	4.29.1	\N	\N	4040861400
1.5.0	bburke@redhat.com	META-INF/jpa-changelog-1.5.0.xml	2025-08-01 09:34:22.047956	13	EXECUTED	9:7af32cd8957fbc069f796b61217483fd	delete tableName=CLIENT_SESSION_AUTH_STATUS; delete tableName=CLIENT_SESSION_ROLE; delete tableName=CLIENT_SESSION_PROT_MAPPER; delete tableName=CLIENT_SESSION_NOTE; delete tableName=CLIENT_SESSION; delete tableName=USER_SESSION_NOTE; delete table...		\N	4.29.1	\N	\N	4040861400
1.6.1_from15	mposolda@redhat.com	META-INF/jpa-changelog-1.6.1.xml	2025-08-01 09:34:22.057433	14	EXECUTED	9:6005e15e84714cd83226bf7879f54190	addColumn tableName=REALM; addColumn tableName=KEYCLOAK_ROLE; addColumn tableName=CLIENT; createTable tableName=OFFLINE_USER_SESSION; createTable tableName=OFFLINE_CLIENT_SESSION; addPrimaryKey constraintName=CONSTRAINT_OFFL_US_SES_PK2, tableName=...		\N	4.29.1	\N	\N	4040861400
1.6.1_from16-pre	mposolda@redhat.com	META-INF/jpa-changelog-1.6.1.xml	2025-08-01 09:34:22.058928	15	MARK_RAN	9:bf656f5a2b055d07f314431cae76f06c	delete tableName=OFFLINE_CLIENT_SESSION; delete tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
1.6.1_from16	mposolda@redhat.com	META-INF/jpa-changelog-1.6.1.xml	2025-08-01 09:34:22.060937	16	MARK_RAN	9:f8dadc9284440469dcf71e25ca6ab99b	dropPrimaryKey constraintName=CONSTRAINT_OFFLINE_US_SES_PK, tableName=OFFLINE_USER_SESSION; dropPrimaryKey constraintName=CONSTRAINT_OFFLINE_CL_SES_PK, tableName=OFFLINE_CLIENT_SESSION; addColumn tableName=OFFLINE_USER_SESSION; update tableName=OF...		\N	4.29.1	\N	\N	4040861400
1.6.1	mposolda@redhat.com	META-INF/jpa-changelog-1.6.1.xml	2025-08-01 09:34:22.063381	17	EXECUTED	9:d41d8cd98f00b204e9800998ecf8427e	empty		\N	4.29.1	\N	\N	4040861400
1.7.0	bburke@redhat.com	META-INF/jpa-changelog-1.7.0.xml	2025-08-01 09:34:22.084117	18	EXECUTED	9:3368ff0be4c2855ee2dd9ca813b38d8e	createTable tableName=KEYCLOAK_GROUP; createTable tableName=GROUP_ROLE_MAPPING; createTable tableName=GROUP_ATTRIBUTE; createTable tableName=USER_GROUP_MEMBERSHIP; createTable tableName=REALM_DEFAULT_GROUPS; addColumn tableName=IDENTITY_PROVIDER; ...		\N	4.29.1	\N	\N	4040861400
1.8.0	mposolda@redhat.com	META-INF/jpa-changelog-1.8.0.xml	2025-08-01 09:34:22.104598	19	EXECUTED	9:8ac2fb5dd030b24c0570a763ed75ed20	addColumn tableName=IDENTITY_PROVIDER; createTable tableName=CLIENT_TEMPLATE; createTable tableName=CLIENT_TEMPLATE_ATTRIBUTES; createTable tableName=TEMPLATE_SCOPE_MAPPING; dropNotNullConstraint columnName=CLIENT_ID, tableName=PROTOCOL_MAPPER; ad...		\N	4.29.1	\N	\N	4040861400
1.8.0-2	keycloak	META-INF/jpa-changelog-1.8.0.xml	2025-08-01 09:34:22.109056	20	EXECUTED	9:f91ddca9b19743db60e3057679810e6c	dropDefaultValue columnName=ALGORITHM, tableName=CREDENTIAL; update tableName=CREDENTIAL		\N	4.29.1	\N	\N	4040861400
1.8.0	mposolda@redhat.com	META-INF/db2-jpa-changelog-1.8.0.xml	2025-08-01 09:34:22.111954	21	MARK_RAN	9:831e82914316dc8a57dc09d755f23c51	addColumn tableName=IDENTITY_PROVIDER; createTable tableName=CLIENT_TEMPLATE; createTable tableName=CLIENT_TEMPLATE_ATTRIBUTES; createTable tableName=TEMPLATE_SCOPE_MAPPING; dropNotNullConstraint columnName=CLIENT_ID, tableName=PROTOCOL_MAPPER; ad...		\N	4.29.1	\N	\N	4040861400
1.8.0-2	keycloak	META-INF/db2-jpa-changelog-1.8.0.xml	2025-08-01 09:34:22.114734	22	MARK_RAN	9:f91ddca9b19743db60e3057679810e6c	dropDefaultValue columnName=ALGORITHM, tableName=CREDENTIAL; update tableName=CREDENTIAL		\N	4.29.1	\N	\N	4040861400
1.9.0	mposolda@redhat.com	META-INF/jpa-changelog-1.9.0.xml	2025-08-01 09:34:22.176444	23	EXECUTED	9:bc3d0f9e823a69dc21e23e94c7a94bb1	update tableName=REALM; update tableName=REALM; update tableName=REALM; update tableName=REALM; update tableName=CREDENTIAL; update tableName=CREDENTIAL; update tableName=CREDENTIAL; update tableName=REALM; update tableName=REALM; customChange; dr...		\N	4.29.1	\N	\N	4040861400
1.9.1	keycloak	META-INF/jpa-changelog-1.9.1.xml	2025-08-01 09:34:22.182328	24	EXECUTED	9:c9999da42f543575ab790e76439a2679	modifyDataType columnName=PRIVATE_KEY, tableName=REALM; modifyDataType columnName=PUBLIC_KEY, tableName=REALM; modifyDataType columnName=CERTIFICATE, tableName=REALM		\N	4.29.1	\N	\N	4040861400
1.9.1	keycloak	META-INF/db2-jpa-changelog-1.9.1.xml	2025-08-01 09:34:22.184062	25	MARK_RAN	9:0d6c65c6f58732d81569e77b10ba301d	modifyDataType columnName=PRIVATE_KEY, tableName=REALM; modifyDataType columnName=CERTIFICATE, tableName=REALM		\N	4.29.1	\N	\N	4040861400
1.9.2	keycloak	META-INF/jpa-changelog-1.9.2.xml	2025-08-01 09:34:22.453676	26	EXECUTED	9:fc576660fc016ae53d2d4778d84d86d0	createIndex indexName=IDX_USER_EMAIL, tableName=USER_ENTITY; createIndex indexName=IDX_USER_ROLE_MAPPING, tableName=USER_ROLE_MAPPING; createIndex indexName=IDX_USER_GROUP_MAPPING, tableName=USER_GROUP_MEMBERSHIP; createIndex indexName=IDX_USER_CO...		\N	4.29.1	\N	\N	4040861400
authz-2.0.0	psilva@redhat.com	META-INF/jpa-changelog-authz-2.0.0.xml	2025-08-01 09:34:22.479313	27	EXECUTED	9:43ed6b0da89ff77206289e87eaa9c024	createTable tableName=RESOURCE_SERVER; addPrimaryKey constraintName=CONSTRAINT_FARS, tableName=RESOURCE_SERVER; addUniqueConstraint constraintName=UK_AU8TT6T700S9V50BU18WS5HA6, tableName=RESOURCE_SERVER; createTable tableName=RESOURCE_SERVER_RESOU...		\N	4.29.1	\N	\N	4040861400
authz-2.5.1	psilva@redhat.com	META-INF/jpa-changelog-authz-2.5.1.xml	2025-08-01 09:34:22.482259	28	EXECUTED	9:44bae577f551b3738740281eceb4ea70	update tableName=RESOURCE_SERVER_POLICY		\N	4.29.1	\N	\N	4040861400
2.1.0-KEYCLOAK-5461	bburke@redhat.com	META-INF/jpa-changelog-2.1.0.xml	2025-08-01 09:34:22.505122	29	EXECUTED	9:bd88e1f833df0420b01e114533aee5e8	createTable tableName=BROKER_LINK; createTable tableName=FED_USER_ATTRIBUTE; createTable tableName=FED_USER_CONSENT; createTable tableName=FED_USER_CONSENT_ROLE; createTable tableName=FED_USER_CONSENT_PROT_MAPPER; createTable tableName=FED_USER_CR...		\N	4.29.1	\N	\N	4040861400
2.2.0	bburke@redhat.com	META-INF/jpa-changelog-2.2.0.xml	2025-08-01 09:34:22.513878	30	EXECUTED	9:a7022af5267f019d020edfe316ef4371	addColumn tableName=ADMIN_EVENT_ENTITY; createTable tableName=CREDENTIAL_ATTRIBUTE; createTable tableName=FED_CREDENTIAL_ATTRIBUTE; modifyDataType columnName=VALUE, tableName=CREDENTIAL; addForeignKeyConstraint baseTableName=FED_CREDENTIAL_ATTRIBU...		\N	4.29.1	\N	\N	4040861400
2.3.0	bburke@redhat.com	META-INF/jpa-changelog-2.3.0.xml	2025-08-01 09:34:22.528938	31	EXECUTED	9:fc155c394040654d6a79227e56f5e25a	createTable tableName=FEDERATED_USER; addPrimaryKey constraintName=CONSTR_FEDERATED_USER, tableName=FEDERATED_USER; dropDefaultValue columnName=TOTP, tableName=USER_ENTITY; dropColumn columnName=TOTP, tableName=USER_ENTITY; addColumn tableName=IDE...		\N	4.29.1	\N	\N	4040861400
2.4.0	bburke@redhat.com	META-INF/jpa-changelog-2.4.0.xml	2025-08-01 09:34:22.533586	32	EXECUTED	9:eac4ffb2a14795e5dc7b426063e54d88	customChange		\N	4.29.1	\N	\N	4040861400
2.5.0	bburke@redhat.com	META-INF/jpa-changelog-2.5.0.xml	2025-08-01 09:34:22.539594	33	EXECUTED	9:54937c05672568c4c64fc9524c1e9462	customChange; modifyDataType columnName=USER_ID, tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
2.5.0-unicode-oracle	hmlnarik@redhat.com	META-INF/jpa-changelog-2.5.0.xml	2025-08-01 09:34:22.541942	34	MARK_RAN	9:f9753208029f582525ed12011a19d054	modifyDataType columnName=DESCRIPTION, tableName=AUTHENTICATION_FLOW; modifyDataType columnName=DESCRIPTION, tableName=CLIENT_TEMPLATE; modifyDataType columnName=DESCRIPTION, tableName=RESOURCE_SERVER_POLICY; modifyDataType columnName=DESCRIPTION,...		\N	4.29.1	\N	\N	4040861400
2.5.0-unicode-other-dbs	hmlnarik@redhat.com	META-INF/jpa-changelog-2.5.0.xml	2025-08-01 09:34:22.556206	35	EXECUTED	9:33d72168746f81f98ae3a1e8e0ca3554	modifyDataType columnName=DESCRIPTION, tableName=AUTHENTICATION_FLOW; modifyDataType columnName=DESCRIPTION, tableName=CLIENT_TEMPLATE; modifyDataType columnName=DESCRIPTION, tableName=RESOURCE_SERVER_POLICY; modifyDataType columnName=DESCRIPTION,...		\N	4.29.1	\N	\N	4040861400
2.5.0-duplicate-email-support	slawomir@dabek.name	META-INF/jpa-changelog-2.5.0.xml	2025-08-01 09:34:22.560563	36	EXECUTED	9:61b6d3d7a4c0e0024b0c839da283da0c	addColumn tableName=REALM		\N	4.29.1	\N	\N	4040861400
2.5.0-unique-group-names	hmlnarik@redhat.com	META-INF/jpa-changelog-2.5.0.xml	2025-08-01 09:34:22.563838	37	EXECUTED	9:8dcac7bdf7378e7d823cdfddebf72fda	addUniqueConstraint constraintName=SIBLING_NAMES, tableName=KEYCLOAK_GROUP		\N	4.29.1	\N	\N	4040861400
2.5.1	bburke@redhat.com	META-INF/jpa-changelog-2.5.1.xml	2025-08-01 09:34:22.566705	38	EXECUTED	9:a2b870802540cb3faa72098db5388af3	addColumn tableName=FED_USER_CONSENT		\N	4.29.1	\N	\N	4040861400
3.0.0	bburke@redhat.com	META-INF/jpa-changelog-3.0.0.xml	2025-08-01 09:34:22.569619	39	EXECUTED	9:132a67499ba24bcc54fb5cbdcfe7e4c0	addColumn tableName=IDENTITY_PROVIDER		\N	4.29.1	\N	\N	4040861400
3.2.0-fix	keycloak	META-INF/jpa-changelog-3.2.0.xml	2025-08-01 09:34:22.570733	40	MARK_RAN	9:938f894c032f5430f2b0fafb1a243462	addNotNullConstraint columnName=REALM_ID, tableName=CLIENT_INITIAL_ACCESS		\N	4.29.1	\N	\N	4040861400
3.2.0-fix-with-keycloak-5416	keycloak	META-INF/jpa-changelog-3.2.0.xml	2025-08-01 09:34:22.572213	41	MARK_RAN	9:845c332ff1874dc5d35974b0babf3006	dropIndex indexName=IDX_CLIENT_INIT_ACC_REALM, tableName=CLIENT_INITIAL_ACCESS; addNotNullConstraint columnName=REALM_ID, tableName=CLIENT_INITIAL_ACCESS; createIndex indexName=IDX_CLIENT_INIT_ACC_REALM, tableName=CLIENT_INITIAL_ACCESS		\N	4.29.1	\N	\N	4040861400
3.2.0-fix-offline-sessions	hmlnarik	META-INF/jpa-changelog-3.2.0.xml	2025-08-01 09:34:22.575695	42	EXECUTED	9:fc86359c079781adc577c5a217e4d04c	customChange		\N	4.29.1	\N	\N	4040861400
3.2.0-fixed	keycloak	META-INF/jpa-changelog-3.2.0.xml	2025-08-01 09:34:24.085964	43	EXECUTED	9:59a64800e3c0d09b825f8a3b444fa8f4	addColumn tableName=REALM; dropPrimaryKey constraintName=CONSTRAINT_OFFL_CL_SES_PK2, tableName=OFFLINE_CLIENT_SESSION; dropColumn columnName=CLIENT_SESSION_ID, tableName=OFFLINE_CLIENT_SESSION; addPrimaryKey constraintName=CONSTRAINT_OFFL_CL_SES_P...		\N	4.29.1	\N	\N	4040861400
3.3.0	keycloak	META-INF/jpa-changelog-3.3.0.xml	2025-08-01 09:34:24.090388	44	EXECUTED	9:d48d6da5c6ccf667807f633fe489ce88	addColumn tableName=USER_ENTITY		\N	4.29.1	\N	\N	4040861400
authz-3.4.0.CR1-resource-server-pk-change-part1	glavoie@gmail.com	META-INF/jpa-changelog-authz-3.4.0.CR1.xml	2025-08-01 09:34:24.094272	45	EXECUTED	9:dde36f7973e80d71fceee683bc5d2951	addColumn tableName=RESOURCE_SERVER_POLICY; addColumn tableName=RESOURCE_SERVER_RESOURCE; addColumn tableName=RESOURCE_SERVER_SCOPE		\N	4.29.1	\N	\N	4040861400
authz-3.4.0.CR1-resource-server-pk-change-part2-KEYCLOAK-6095	hmlnarik@redhat.com	META-INF/jpa-changelog-authz-3.4.0.CR1.xml	2025-08-01 09:34:24.097638	46	EXECUTED	9:b855e9b0a406b34fa323235a0cf4f640	customChange		\N	4.29.1	\N	\N	4040861400
authz-3.4.0.CR1-resource-server-pk-change-part3-fixed	glavoie@gmail.com	META-INF/jpa-changelog-authz-3.4.0.CR1.xml	2025-08-01 09:34:24.0989	47	MARK_RAN	9:51abbacd7b416c50c4421a8cabf7927e	dropIndex indexName=IDX_RES_SERV_POL_RES_SERV, tableName=RESOURCE_SERVER_POLICY; dropIndex indexName=IDX_RES_SRV_RES_RES_SRV, tableName=RESOURCE_SERVER_RESOURCE; dropIndex indexName=IDX_RES_SRV_SCOPE_RES_SRV, tableName=RESOURCE_SERVER_SCOPE		\N	4.29.1	\N	\N	4040861400
authz-3.4.0.CR1-resource-server-pk-change-part3-fixed-nodropindex	glavoie@gmail.com	META-INF/jpa-changelog-authz-3.4.0.CR1.xml	2025-08-01 09:34:24.20167	48	EXECUTED	9:bdc99e567b3398bac83263d375aad143	addNotNullConstraint columnName=RESOURCE_SERVER_CLIENT_ID, tableName=RESOURCE_SERVER_POLICY; addNotNullConstraint columnName=RESOURCE_SERVER_CLIENT_ID, tableName=RESOURCE_SERVER_RESOURCE; addNotNullConstraint columnName=RESOURCE_SERVER_CLIENT_ID, ...		\N	4.29.1	\N	\N	4040861400
authn-3.4.0.CR1-refresh-token-max-reuse	glavoie@gmail.com	META-INF/jpa-changelog-authz-3.4.0.CR1.xml	2025-08-01 09:34:24.205402	49	EXECUTED	9:d198654156881c46bfba39abd7769e69	addColumn tableName=REALM		\N	4.29.1	\N	\N	4040861400
3.4.0	keycloak	META-INF/jpa-changelog-3.4.0.xml	2025-08-01 09:34:24.219923	50	EXECUTED	9:cfdd8736332ccdd72c5256ccb42335db	addPrimaryKey constraintName=CONSTRAINT_REALM_DEFAULT_ROLES, tableName=REALM_DEFAULT_ROLES; addPrimaryKey constraintName=CONSTRAINT_COMPOSITE_ROLE, tableName=COMPOSITE_ROLE; addPrimaryKey constraintName=CONSTR_REALM_DEFAULT_GROUPS, tableName=REALM...		\N	4.29.1	\N	\N	4040861400
3.4.0-KEYCLOAK-5230	hmlnarik@redhat.com	META-INF/jpa-changelog-3.4.0.xml	2025-08-01 09:34:24.886548	51	EXECUTED	9:7c84de3d9bd84d7f077607c1a4dcb714	createIndex indexName=IDX_FU_ATTRIBUTE, tableName=FED_USER_ATTRIBUTE; createIndex indexName=IDX_FU_CONSENT, tableName=FED_USER_CONSENT; createIndex indexName=IDX_FU_CONSENT_RU, tableName=FED_USER_CONSENT; createIndex indexName=IDX_FU_CREDENTIAL, t...		\N	4.29.1	\N	\N	4040861400
3.4.1	psilva@redhat.com	META-INF/jpa-changelog-3.4.1.xml	2025-08-01 09:34:24.892032	52	EXECUTED	9:5a6bb36cbefb6a9d6928452c0852af2d	modifyDataType columnName=VALUE, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
3.4.2	keycloak	META-INF/jpa-changelog-3.4.2.xml	2025-08-01 09:34:24.895812	53	EXECUTED	9:8f23e334dbc59f82e0a328373ca6ced0	update tableName=REALM		\N	4.29.1	\N	\N	4040861400
3.4.2-KEYCLOAK-5172	mkanis@redhat.com	META-INF/jpa-changelog-3.4.2.xml	2025-08-01 09:34:24.899048	54	EXECUTED	9:9156214268f09d970cdf0e1564d866af	update tableName=CLIENT		\N	4.29.1	\N	\N	4040861400
4.0.0-KEYCLOAK-6335	bburke@redhat.com	META-INF/jpa-changelog-4.0.0.xml	2025-08-01 09:34:24.906072	55	EXECUTED	9:db806613b1ed154826c02610b7dbdf74	createTable tableName=CLIENT_AUTH_FLOW_BINDINGS; addPrimaryKey constraintName=C_CLI_FLOW_BIND, tableName=CLIENT_AUTH_FLOW_BINDINGS		\N	4.29.1	\N	\N	4040861400
4.0.0-CLEANUP-UNUSED-TABLE	bburke@redhat.com	META-INF/jpa-changelog-4.0.0.xml	2025-08-01 09:34:24.912284	56	EXECUTED	9:229a041fb72d5beac76bb94a5fa709de	dropTable tableName=CLIENT_IDENTITY_PROV_MAPPING		\N	4.29.1	\N	\N	4040861400
4.0.0-KEYCLOAK-6228	bburke@redhat.com	META-INF/jpa-changelog-4.0.0.xml	2025-08-01 09:34:25.002947	57	EXECUTED	9:079899dade9c1e683f26b2aa9ca6ff04	dropUniqueConstraint constraintName=UK_JKUWUVD56ONTGSUHOGM8UEWRT, tableName=USER_CONSENT; dropNotNullConstraint columnName=CLIENT_ID, tableName=USER_CONSENT; addColumn tableName=USER_CONSENT; addUniqueConstraint constraintName=UK_JKUWUVD56ONTGSUHO...		\N	4.29.1	\N	\N	4040861400
4.0.0-KEYCLOAK-5579-fixed	mposolda@redhat.com	META-INF/jpa-changelog-4.0.0.xml	2025-08-01 09:34:25.740377	58	EXECUTED	9:139b79bcbbfe903bb1c2d2a4dbf001d9	dropForeignKeyConstraint baseTableName=CLIENT_TEMPLATE_ATTRIBUTES, constraintName=FK_CL_TEMPL_ATTR_TEMPL; renameTable newTableName=CLIENT_SCOPE_ATTRIBUTES, oldTableName=CLIENT_TEMPLATE_ATTRIBUTES; renameColumn newColumnName=SCOPE_ID, oldColumnName...		\N	4.29.1	\N	\N	4040861400
authz-4.0.0.CR1	psilva@redhat.com	META-INF/jpa-changelog-authz-4.0.0.CR1.xml	2025-08-01 09:34:25.762008	59	EXECUTED	9:b55738ad889860c625ba2bf483495a04	createTable tableName=RESOURCE_SERVER_PERM_TICKET; addPrimaryKey constraintName=CONSTRAINT_FAPMT, tableName=RESOURCE_SERVER_PERM_TICKET; addForeignKeyConstraint baseTableName=RESOURCE_SERVER_PERM_TICKET, constraintName=FK_FRSRHO213XCX4WNKOG82SSPMT...		\N	4.29.1	\N	\N	4040861400
authz-4.0.0.Beta3	psilva@redhat.com	META-INF/jpa-changelog-authz-4.0.0.Beta3.xml	2025-08-01 09:34:25.768177	60	EXECUTED	9:e0057eac39aa8fc8e09ac6cfa4ae15fe	addColumn tableName=RESOURCE_SERVER_POLICY; addColumn tableName=RESOURCE_SERVER_PERM_TICKET; addForeignKeyConstraint baseTableName=RESOURCE_SERVER_PERM_TICKET, constraintName=FK_FRSRPO2128CX4WNKOG82SSRFY, referencedTableName=RESOURCE_SERVER_POLICY		\N	4.29.1	\N	\N	4040861400
authz-4.2.0.Final	mhajas@redhat.com	META-INF/jpa-changelog-authz-4.2.0.Final.xml	2025-08-01 09:34:25.776274	61	EXECUTED	9:42a33806f3a0443fe0e7feeec821326c	createTable tableName=RESOURCE_URIS; addForeignKeyConstraint baseTableName=RESOURCE_URIS, constraintName=FK_RESOURCE_SERVER_URIS, referencedTableName=RESOURCE_SERVER_RESOURCE; customChange; dropColumn columnName=URI, tableName=RESOURCE_SERVER_RESO...		\N	4.29.1	\N	\N	4040861400
authz-4.2.0.Final-KEYCLOAK-9944	hmlnarik@redhat.com	META-INF/jpa-changelog-authz-4.2.0.Final.xml	2025-08-01 09:34:25.780347	62	EXECUTED	9:9968206fca46eecc1f51db9c024bfe56	addPrimaryKey constraintName=CONSTRAINT_RESOUR_URIS_PK, tableName=RESOURCE_URIS		\N	4.29.1	\N	\N	4040861400
4.2.0-KEYCLOAK-6313	wadahiro@gmail.com	META-INF/jpa-changelog-4.2.0.xml	2025-08-01 09:34:25.783893	63	EXECUTED	9:92143a6daea0a3f3b8f598c97ce55c3d	addColumn tableName=REQUIRED_ACTION_PROVIDER		\N	4.29.1	\N	\N	4040861400
4.3.0-KEYCLOAK-7984	wadahiro@gmail.com	META-INF/jpa-changelog-4.3.0.xml	2025-08-01 09:34:25.786976	64	EXECUTED	9:82bab26a27195d889fb0429003b18f40	update tableName=REQUIRED_ACTION_PROVIDER		\N	4.29.1	\N	\N	4040861400
4.6.0-KEYCLOAK-7950	psilva@redhat.com	META-INF/jpa-changelog-4.6.0.xml	2025-08-01 09:34:25.789991	65	EXECUTED	9:e590c88ddc0b38b0ae4249bbfcb5abc3	update tableName=RESOURCE_SERVER_RESOURCE		\N	4.29.1	\N	\N	4040861400
4.6.0-KEYCLOAK-8377	keycloak	META-INF/jpa-changelog-4.6.0.xml	2025-08-01 09:34:25.84085	66	EXECUTED	9:5c1f475536118dbdc38d5d7977950cc0	createTable tableName=ROLE_ATTRIBUTE; addPrimaryKey constraintName=CONSTRAINT_ROLE_ATTRIBUTE_PK, tableName=ROLE_ATTRIBUTE; addForeignKeyConstraint baseTableName=ROLE_ATTRIBUTE, constraintName=FK_ROLE_ATTRIBUTE_ID, referencedTableName=KEYCLOAK_ROLE...		\N	4.29.1	\N	\N	4040861400
4.6.0-KEYCLOAK-8555	gideonray@gmail.com	META-INF/jpa-changelog-4.6.0.xml	2025-08-01 09:34:25.87857	67	EXECUTED	9:e7c9f5f9c4d67ccbbcc215440c718a17	createIndex indexName=IDX_COMPONENT_PROVIDER_TYPE, tableName=COMPONENT		\N	4.29.1	\N	\N	4040861400
4.7.0-KEYCLOAK-1267	sguilhen@redhat.com	META-INF/jpa-changelog-4.7.0.xml	2025-08-01 09:34:25.882589	68	EXECUTED	9:88e0bfdda924690d6f4e430c53447dd5	addColumn tableName=REALM		\N	4.29.1	\N	\N	4040861400
4.7.0-KEYCLOAK-7275	keycloak	META-INF/jpa-changelog-4.7.0.xml	2025-08-01 09:34:25.923709	69	EXECUTED	9:f53177f137e1c46b6a88c59ec1cb5218	renameColumn newColumnName=CREATED_ON, oldColumnName=LAST_SESSION_REFRESH, tableName=OFFLINE_USER_SESSION; addNotNullConstraint columnName=CREATED_ON, tableName=OFFLINE_USER_SESSION; addColumn tableName=OFFLINE_USER_SESSION; customChange; createIn...		\N	4.29.1	\N	\N	4040861400
4.8.0-KEYCLOAK-8835	sguilhen@redhat.com	META-INF/jpa-changelog-4.8.0.xml	2025-08-01 09:34:25.92908	70	EXECUTED	9:a74d33da4dc42a37ec27121580d1459f	addNotNullConstraint columnName=SSO_MAX_LIFESPAN_REMEMBER_ME, tableName=REALM; addNotNullConstraint columnName=SSO_IDLE_TIMEOUT_REMEMBER_ME, tableName=REALM		\N	4.29.1	\N	\N	4040861400
authz-7.0.0-KEYCLOAK-10443	psilva@redhat.com	META-INF/jpa-changelog-authz-7.0.0.xml	2025-08-01 09:34:25.932393	71	EXECUTED	9:fd4ade7b90c3b67fae0bfcfcb42dfb5f	addColumn tableName=RESOURCE_SERVER		\N	4.29.1	\N	\N	4040861400
8.0.0-adding-credential-columns	keycloak	META-INF/jpa-changelog-8.0.0.xml	2025-08-01 09:34:25.937976	72	EXECUTED	9:aa072ad090bbba210d8f18781b8cebf4	addColumn tableName=CREDENTIAL; addColumn tableName=FED_USER_CREDENTIAL		\N	4.29.1	\N	\N	4040861400
8.0.0-updating-credential-data-not-oracle-fixed	keycloak	META-INF/jpa-changelog-8.0.0.xml	2025-08-01 09:34:25.944543	73	EXECUTED	9:1ae6be29bab7c2aa376f6983b932be37	update tableName=CREDENTIAL; update tableName=CREDENTIAL; update tableName=CREDENTIAL; update tableName=FED_USER_CREDENTIAL; update tableName=FED_USER_CREDENTIAL; update tableName=FED_USER_CREDENTIAL		\N	4.29.1	\N	\N	4040861400
8.0.0-updating-credential-data-oracle-fixed	keycloak	META-INF/jpa-changelog-8.0.0.xml	2025-08-01 09:34:25.946788	74	MARK_RAN	9:14706f286953fc9a25286dbd8fb30d97	update tableName=CREDENTIAL; update tableName=CREDENTIAL; update tableName=CREDENTIAL; update tableName=FED_USER_CREDENTIAL; update tableName=FED_USER_CREDENTIAL; update tableName=FED_USER_CREDENTIAL		\N	4.29.1	\N	\N	4040861400
8.0.0-credential-cleanup-fixed	keycloak	META-INF/jpa-changelog-8.0.0.xml	2025-08-01 09:34:25.961133	75	EXECUTED	9:2b9cc12779be32c5b40e2e67711a218b	dropDefaultValue columnName=COUNTER, tableName=CREDENTIAL; dropDefaultValue columnName=DIGITS, tableName=CREDENTIAL; dropDefaultValue columnName=PERIOD, tableName=CREDENTIAL; dropDefaultValue columnName=ALGORITHM, tableName=CREDENTIAL; dropColumn ...		\N	4.29.1	\N	\N	4040861400
8.0.0-resource-tag-support	keycloak	META-INF/jpa-changelog-8.0.0.xml	2025-08-01 09:34:26.000458	76	EXECUTED	9:91fa186ce7a5af127a2d7a91ee083cc5	addColumn tableName=MIGRATION_MODEL; createIndex indexName=IDX_UPDATE_TIME, tableName=MIGRATION_MODEL		\N	4.29.1	\N	\N	4040861400
9.0.0-always-display-client	keycloak	META-INF/jpa-changelog-9.0.0.xml	2025-08-01 09:34:26.00619	77	EXECUTED	9:6335e5c94e83a2639ccd68dd24e2e5ad	addColumn tableName=CLIENT		\N	4.29.1	\N	\N	4040861400
9.0.0-drop-constraints-for-column-increase	keycloak	META-INF/jpa-changelog-9.0.0.xml	2025-08-01 09:34:26.008009	78	MARK_RAN	9:6bdb5658951e028bfe16fa0a8228b530	dropUniqueConstraint constraintName=UK_FRSR6T700S9V50BU18WS5PMT, tableName=RESOURCE_SERVER_PERM_TICKET; dropUniqueConstraint constraintName=UK_FRSR6T700S9V50BU18WS5HA6, tableName=RESOURCE_SERVER_RESOURCE; dropPrimaryKey constraintName=CONSTRAINT_O...		\N	4.29.1	\N	\N	4040861400
9.0.0-increase-column-size-federated-fk	keycloak	META-INF/jpa-changelog-9.0.0.xml	2025-08-01 09:34:26.020646	79	EXECUTED	9:d5bc15a64117ccad481ce8792d4c608f	modifyDataType columnName=CLIENT_ID, tableName=FED_USER_CONSENT; modifyDataType columnName=CLIENT_REALM_CONSTRAINT, tableName=KEYCLOAK_ROLE; modifyDataType columnName=OWNER, tableName=RESOURCE_SERVER_POLICY; modifyDataType columnName=CLIENT_ID, ta...		\N	4.29.1	\N	\N	4040861400
9.0.0-recreate-constraints-after-column-increase	keycloak	META-INF/jpa-changelog-9.0.0.xml	2025-08-01 09:34:26.022405	80	MARK_RAN	9:077cba51999515f4d3e7ad5619ab592c	addNotNullConstraint columnName=CLIENT_ID, tableName=OFFLINE_CLIENT_SESSION; addNotNullConstraint columnName=OWNER, tableName=RESOURCE_SERVER_PERM_TICKET; addNotNullConstraint columnName=REQUESTER, tableName=RESOURCE_SERVER_PERM_TICKET; addNotNull...		\N	4.29.1	\N	\N	4040861400
9.0.1-add-index-to-client.client_id	keycloak	META-INF/jpa-changelog-9.0.1.xml	2025-08-01 09:34:26.099092	81	EXECUTED	9:be969f08a163bf47c6b9e9ead8ac2afb	createIndex indexName=IDX_CLIENT_ID, tableName=CLIENT		\N	4.29.1	\N	\N	4040861400
9.0.1-KEYCLOAK-12579-drop-constraints	keycloak	META-INF/jpa-changelog-9.0.1.xml	2025-08-01 09:34:26.100957	82	MARK_RAN	9:6d3bb4408ba5a72f39bd8a0b301ec6e3	dropUniqueConstraint constraintName=SIBLING_NAMES, tableName=KEYCLOAK_GROUP		\N	4.29.1	\N	\N	4040861400
9.0.1-KEYCLOAK-12579-add-not-null-constraint	keycloak	META-INF/jpa-changelog-9.0.1.xml	2025-08-01 09:34:26.104731	83	EXECUTED	9:966bda61e46bebf3cc39518fbed52fa7	addNotNullConstraint columnName=PARENT_GROUP, tableName=KEYCLOAK_GROUP		\N	4.29.1	\N	\N	4040861400
9.0.1-KEYCLOAK-12579-recreate-constraints	keycloak	META-INF/jpa-changelog-9.0.1.xml	2025-08-01 09:34:26.106086	84	MARK_RAN	9:8dcac7bdf7378e7d823cdfddebf72fda	addUniqueConstraint constraintName=SIBLING_NAMES, tableName=KEYCLOAK_GROUP		\N	4.29.1	\N	\N	4040861400
9.0.1-add-index-to-events	keycloak	META-INF/jpa-changelog-9.0.1.xml	2025-08-01 09:34:26.143836	85	EXECUTED	9:7d93d602352a30c0c317e6a609b56599	createIndex indexName=IDX_EVENT_TIME, tableName=EVENT_ENTITY		\N	4.29.1	\N	\N	4040861400
map-remove-ri	keycloak	META-INF/jpa-changelog-11.0.0.xml	2025-08-01 09:34:26.147862	86	EXECUTED	9:71c5969e6cdd8d7b6f47cebc86d37627	dropForeignKeyConstraint baseTableName=REALM, constraintName=FK_TRAF444KK6QRKMS7N56AIWQ5Y; dropForeignKeyConstraint baseTableName=KEYCLOAK_ROLE, constraintName=FK_KJHO5LE2C0RAL09FL8CM9WFW9		\N	4.29.1	\N	\N	4040861400
map-remove-ri	keycloak	META-INF/jpa-changelog-12.0.0.xml	2025-08-01 09:34:26.153965	87	EXECUTED	9:a9ba7d47f065f041b7da856a81762021	dropForeignKeyConstraint baseTableName=REALM_DEFAULT_GROUPS, constraintName=FK_DEF_GROUPS_GROUP; dropForeignKeyConstraint baseTableName=REALM_DEFAULT_ROLES, constraintName=FK_H4WPD7W4HSOOLNI3H0SW7BTJE; dropForeignKeyConstraint baseTableName=CLIENT...		\N	4.29.1	\N	\N	4040861400
12.1.0-add-realm-localization-table	keycloak	META-INF/jpa-changelog-12.0.0.xml	2025-08-01 09:34:26.158919	88	EXECUTED	9:fffabce2bc01e1a8f5110d5278500065	createTable tableName=REALM_LOCALIZATIONS; addPrimaryKey tableName=REALM_LOCALIZATIONS		\N	4.29.1	\N	\N	4040861400
default-roles	keycloak	META-INF/jpa-changelog-13.0.0.xml	2025-08-01 09:34:26.164077	89	EXECUTED	9:fa8a5b5445e3857f4b010bafb5009957	addColumn tableName=REALM; customChange		\N	4.29.1	\N	\N	4040861400
default-roles-cleanup	keycloak	META-INF/jpa-changelog-13.0.0.xml	2025-08-01 09:34:26.168214	90	EXECUTED	9:67ac3241df9a8582d591c5ed87125f39	dropTable tableName=REALM_DEFAULT_ROLES; dropTable tableName=CLIENT_DEFAULT_ROLES		\N	4.29.1	\N	\N	4040861400
13.0.0-KEYCLOAK-16844	keycloak	META-INF/jpa-changelog-13.0.0.xml	2025-08-01 09:34:26.201565	91	EXECUTED	9:ad1194d66c937e3ffc82386c050ba089	createIndex indexName=IDX_OFFLINE_USS_PRELOAD, tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
map-remove-ri-13.0.0	keycloak	META-INF/jpa-changelog-13.0.0.xml	2025-08-01 09:34:26.206732	92	EXECUTED	9:d9be619d94af5a2f5d07b9f003543b91	dropForeignKeyConstraint baseTableName=DEFAULT_CLIENT_SCOPE, constraintName=FK_R_DEF_CLI_SCOPE_SCOPE; dropForeignKeyConstraint baseTableName=CLIENT_SCOPE_CLIENT, constraintName=FK_C_CLI_SCOPE_SCOPE; dropForeignKeyConstraint baseTableName=CLIENT_SC...		\N	4.29.1	\N	\N	4040861400
13.0.0-KEYCLOAK-17992-drop-constraints	keycloak	META-INF/jpa-changelog-13.0.0.xml	2025-08-01 09:34:26.208049	93	MARK_RAN	9:544d201116a0fcc5a5da0925fbbc3bde	dropPrimaryKey constraintName=C_CLI_SCOPE_BIND, tableName=CLIENT_SCOPE_CLIENT; dropIndex indexName=IDX_CLSCOPE_CL, tableName=CLIENT_SCOPE_CLIENT; dropIndex indexName=IDX_CL_CLSCOPE, tableName=CLIENT_SCOPE_CLIENT		\N	4.29.1	\N	\N	4040861400
13.0.0-increase-column-size-federated	keycloak	META-INF/jpa-changelog-13.0.0.xml	2025-08-01 09:34:26.21343	94	EXECUTED	9:43c0c1055b6761b4b3e89de76d612ccf	modifyDataType columnName=CLIENT_ID, tableName=CLIENT_SCOPE_CLIENT; modifyDataType columnName=SCOPE_ID, tableName=CLIENT_SCOPE_CLIENT		\N	4.29.1	\N	\N	4040861400
13.0.0-KEYCLOAK-17992-recreate-constraints	keycloak	META-INF/jpa-changelog-13.0.0.xml	2025-08-01 09:34:26.215143	95	MARK_RAN	9:8bd711fd0330f4fe980494ca43ab1139	addNotNullConstraint columnName=CLIENT_ID, tableName=CLIENT_SCOPE_CLIENT; addNotNullConstraint columnName=SCOPE_ID, tableName=CLIENT_SCOPE_CLIENT; addPrimaryKey constraintName=C_CLI_SCOPE_BIND, tableName=CLIENT_SCOPE_CLIENT; createIndex indexName=...		\N	4.29.1	\N	\N	4040861400
json-string-accomodation-fixed	keycloak	META-INF/jpa-changelog-13.0.0.xml	2025-08-01 09:34:26.22047	96	EXECUTED	9:e07d2bc0970c348bb06fb63b1f82ddbf	addColumn tableName=REALM_ATTRIBUTE; update tableName=REALM_ATTRIBUTE; dropColumn columnName=VALUE, tableName=REALM_ATTRIBUTE; renameColumn newColumnName=VALUE, oldColumnName=VALUE_NEW, tableName=REALM_ATTRIBUTE		\N	4.29.1	\N	\N	4040861400
14.0.0-KEYCLOAK-11019	keycloak	META-INF/jpa-changelog-14.0.0.xml	2025-08-01 09:34:26.316693	97	EXECUTED	9:24fb8611e97f29989bea412aa38d12b7	createIndex indexName=IDX_OFFLINE_CSS_PRELOAD, tableName=OFFLINE_CLIENT_SESSION; createIndex indexName=IDX_OFFLINE_USS_BY_USER, tableName=OFFLINE_USER_SESSION; createIndex indexName=IDX_OFFLINE_USS_BY_USERSESS, tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
14.0.0-KEYCLOAK-18286	keycloak	META-INF/jpa-changelog-14.0.0.xml	2025-08-01 09:34:26.318278	98	MARK_RAN	9:259f89014ce2506ee84740cbf7163aa7	createIndex indexName=IDX_CLIENT_ATT_BY_NAME_VALUE, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
14.0.0-KEYCLOAK-18286-revert	keycloak	META-INF/jpa-changelog-14.0.0.xml	2025-08-01 09:34:26.327228	99	MARK_RAN	9:04baaf56c116ed19951cbc2cca584022	dropIndex indexName=IDX_CLIENT_ATT_BY_NAME_VALUE, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
14.0.0-KEYCLOAK-18286-supported-dbs	keycloak	META-INF/jpa-changelog-14.0.0.xml	2025-08-01 09:34:26.366204	100	EXECUTED	9:60ca84a0f8c94ec8c3504a5a3bc88ee8	createIndex indexName=IDX_CLIENT_ATT_BY_NAME_VALUE, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
14.0.0-KEYCLOAK-18286-unsupported-dbs	keycloak	META-INF/jpa-changelog-14.0.0.xml	2025-08-01 09:34:26.367875	101	MARK_RAN	9:d3d977031d431db16e2c181ce49d73e9	createIndex indexName=IDX_CLIENT_ATT_BY_NAME_VALUE, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
KEYCLOAK-17267-add-index-to-user-attributes	keycloak	META-INF/jpa-changelog-14.0.0.xml	2025-08-01 09:34:26.403334	102	EXECUTED	9:0b305d8d1277f3a89a0a53a659ad274c	createIndex indexName=IDX_USER_ATTRIBUTE_NAME, tableName=USER_ATTRIBUTE		\N	4.29.1	\N	\N	4040861400
KEYCLOAK-18146-add-saml-art-binding-identifier	keycloak	META-INF/jpa-changelog-14.0.0.xml	2025-08-01 09:34:26.406955	103	EXECUTED	9:2c374ad2cdfe20e2905a84c8fac48460	customChange		\N	4.29.1	\N	\N	4040861400
15.0.0-KEYCLOAK-18467	keycloak	META-INF/jpa-changelog-15.0.0.xml	2025-08-01 09:34:26.411802	104	EXECUTED	9:47a760639ac597360a8219f5b768b4de	addColumn tableName=REALM_LOCALIZATIONS; update tableName=REALM_LOCALIZATIONS; dropColumn columnName=TEXTS, tableName=REALM_LOCALIZATIONS; renameColumn newColumnName=TEXTS, oldColumnName=TEXTS_NEW, tableName=REALM_LOCALIZATIONS; addNotNullConstrai...		\N	4.29.1	\N	\N	4040861400
17.0.0-9562	keycloak	META-INF/jpa-changelog-17.0.0.xml	2025-08-01 09:34:26.452243	105	EXECUTED	9:a6272f0576727dd8cad2522335f5d99e	createIndex indexName=IDX_USER_SERVICE_ACCOUNT, tableName=USER_ENTITY		\N	4.29.1	\N	\N	4040861400
18.0.0-10625-IDX_ADMIN_EVENT_TIME	keycloak	META-INF/jpa-changelog-18.0.0.xml	2025-08-01 09:34:26.49223	106	EXECUTED	9:015479dbd691d9cc8669282f4828c41d	createIndex indexName=IDX_ADMIN_EVENT_TIME, tableName=ADMIN_EVENT_ENTITY		\N	4.29.1	\N	\N	4040861400
18.0.15-30992-index-consent	keycloak	META-INF/jpa-changelog-18.0.15.xml	2025-08-01 09:34:26.537166	107	EXECUTED	9:80071ede7a05604b1f4906f3bf3b00f0	createIndex indexName=IDX_USCONSENT_SCOPE_ID, tableName=USER_CONSENT_CLIENT_SCOPE		\N	4.29.1	\N	\N	4040861400
19.0.0-10135	keycloak	META-INF/jpa-changelog-19.0.0.xml	2025-08-01 09:34:26.540658	108	EXECUTED	9:9518e495fdd22f78ad6425cc30630221	customChange		\N	4.29.1	\N	\N	4040861400
20.0.0-12964-supported-dbs	keycloak	META-INF/jpa-changelog-20.0.0.xml	2025-08-01 09:34:26.582935	109	EXECUTED	9:e5f243877199fd96bcc842f27a1656ac	createIndex indexName=IDX_GROUP_ATT_BY_NAME_VALUE, tableName=GROUP_ATTRIBUTE		\N	4.29.1	\N	\N	4040861400
20.0.0-12964-unsupported-dbs	keycloak	META-INF/jpa-changelog-20.0.0.xml	2025-08-01 09:34:26.584891	110	MARK_RAN	9:1a6fcaa85e20bdeae0a9ce49b41946a5	createIndex indexName=IDX_GROUP_ATT_BY_NAME_VALUE, tableName=GROUP_ATTRIBUTE		\N	4.29.1	\N	\N	4040861400
client-attributes-string-accomodation-fixed	keycloak	META-INF/jpa-changelog-20.0.0.xml	2025-08-01 09:34:26.591167	111	EXECUTED	9:3f332e13e90739ed0c35b0b25b7822ca	addColumn tableName=CLIENT_ATTRIBUTES; update tableName=CLIENT_ATTRIBUTES; dropColumn columnName=VALUE, tableName=CLIENT_ATTRIBUTES; renameColumn newColumnName=VALUE, oldColumnName=VALUE_NEW, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
21.0.2-17277	keycloak	META-INF/jpa-changelog-21.0.2.xml	2025-08-01 09:34:26.594935	112	EXECUTED	9:7ee1f7a3fb8f5588f171fb9a6ab623c0	customChange		\N	4.29.1	\N	\N	4040861400
21.1.0-19404	keycloak	META-INF/jpa-changelog-21.1.0.xml	2025-08-01 09:34:26.603905	113	EXECUTED	9:3d7e830b52f33676b9d64f7f2b2ea634	modifyDataType columnName=DECISION_STRATEGY, tableName=RESOURCE_SERVER_POLICY; modifyDataType columnName=LOGIC, tableName=RESOURCE_SERVER_POLICY; modifyDataType columnName=POLICY_ENFORCE_MODE, tableName=RESOURCE_SERVER		\N	4.29.1	\N	\N	4040861400
21.1.0-19404-2	keycloak	META-INF/jpa-changelog-21.1.0.xml	2025-08-01 09:34:26.606763	114	MARK_RAN	9:627d032e3ef2c06c0e1f73d2ae25c26c	addColumn tableName=RESOURCE_SERVER_POLICY; update tableName=RESOURCE_SERVER_POLICY; dropColumn columnName=DECISION_STRATEGY, tableName=RESOURCE_SERVER_POLICY; renameColumn newColumnName=DECISION_STRATEGY, oldColumnName=DECISION_STRATEGY_NEW, tabl...		\N	4.29.1	\N	\N	4040861400
22.0.0-17484-updated	keycloak	META-INF/jpa-changelog-22.0.0.xml	2025-08-01 09:34:26.61232	115	EXECUTED	9:90af0bfd30cafc17b9f4d6eccd92b8b3	customChange		\N	4.29.1	\N	\N	4040861400
22.0.5-24031	keycloak	META-INF/jpa-changelog-22.0.0.xml	2025-08-01 09:34:26.613608	116	MARK_RAN	9:a60d2d7b315ec2d3eba9e2f145f9df28	customChange		\N	4.29.1	\N	\N	4040861400
23.0.0-12062	keycloak	META-INF/jpa-changelog-23.0.0.xml	2025-08-01 09:34:26.618679	117	EXECUTED	9:2168fbe728fec46ae9baf15bf80927b8	addColumn tableName=COMPONENT_CONFIG; update tableName=COMPONENT_CONFIG; dropColumn columnName=VALUE, tableName=COMPONENT_CONFIG; renameColumn newColumnName=VALUE, oldColumnName=VALUE_NEW, tableName=COMPONENT_CONFIG		\N	4.29.1	\N	\N	4040861400
23.0.0-17258	keycloak	META-INF/jpa-changelog-23.0.0.xml	2025-08-01 09:34:26.621406	118	EXECUTED	9:36506d679a83bbfda85a27ea1864dca8	addColumn tableName=EVENT_ENTITY		\N	4.29.1	\N	\N	4040861400
24.0.0-9758	keycloak	META-INF/jpa-changelog-24.0.0.xml	2025-08-01 09:34:26.756361	119	EXECUTED	9:502c557a5189f600f0f445a9b49ebbce	addColumn tableName=USER_ATTRIBUTE; addColumn tableName=FED_USER_ATTRIBUTE; createIndex indexName=USER_ATTR_LONG_VALUES, tableName=USER_ATTRIBUTE; createIndex indexName=FED_USER_ATTR_LONG_VALUES, tableName=FED_USER_ATTRIBUTE; createIndex indexName...		\N	4.29.1	\N	\N	4040861400
24.0.0-9758-2	keycloak	META-INF/jpa-changelog-24.0.0.xml	2025-08-01 09:34:26.75975	120	EXECUTED	9:bf0fdee10afdf597a987adbf291db7b2	customChange		\N	4.29.1	\N	\N	4040861400
24.0.0-26618-drop-index-if-present	keycloak	META-INF/jpa-changelog-24.0.0.xml	2025-08-01 09:34:26.763579	121	MARK_RAN	9:04baaf56c116ed19951cbc2cca584022	dropIndex indexName=IDX_CLIENT_ATT_BY_NAME_VALUE, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
24.0.0-26618-reindex	keycloak	META-INF/jpa-changelog-24.0.0.xml	2025-08-01 09:34:26.795047	122	EXECUTED	9:08707c0f0db1cef6b352db03a60edc7f	createIndex indexName=IDX_CLIENT_ATT_BY_NAME_VALUE, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
24.0.2-27228	keycloak	META-INF/jpa-changelog-24.0.2.xml	2025-08-01 09:34:26.797903	123	EXECUTED	9:eaee11f6b8aa25d2cc6a84fb86fc6238	customChange		\N	4.29.1	\N	\N	4040861400
24.0.2-27967-drop-index-if-present	keycloak	META-INF/jpa-changelog-24.0.2.xml	2025-08-01 09:34:26.799129	124	MARK_RAN	9:04baaf56c116ed19951cbc2cca584022	dropIndex indexName=IDX_CLIENT_ATT_BY_NAME_VALUE, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
24.0.2-27967-reindex	keycloak	META-INF/jpa-changelog-24.0.2.xml	2025-08-01 09:34:26.800881	125	MARK_RAN	9:d3d977031d431db16e2c181ce49d73e9	createIndex indexName=IDX_CLIENT_ATT_BY_NAME_VALUE, tableName=CLIENT_ATTRIBUTES		\N	4.29.1	\N	\N	4040861400
25.0.0-28265-tables	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:26.805056	126	EXECUTED	9:deda2df035df23388af95bbd36c17cef	addColumn tableName=OFFLINE_USER_SESSION; addColumn tableName=OFFLINE_CLIENT_SESSION		\N	4.29.1	\N	\N	4040861400
25.0.0-28265-index-creation	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:26.839646	127	EXECUTED	9:3e96709818458ae49f3c679ae58d263a	createIndex indexName=IDX_OFFLINE_USS_BY_LAST_SESSION_REFRESH, tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
25.0.0-28265-index-cleanup-uss-createdon	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.000651	128	EXECUTED	9:78ab4fc129ed5e8265dbcc3485fba92f	dropIndex indexName=IDX_OFFLINE_USS_CREATEDON, tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
25.0.0-28265-index-cleanup-uss-preload	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.132394	129	EXECUTED	9:de5f7c1f7e10994ed8b62e621d20eaab	dropIndex indexName=IDX_OFFLINE_USS_PRELOAD, tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
25.0.0-28265-index-cleanup-uss-by-usersess	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.271586	130	EXECUTED	9:6eee220d024e38e89c799417ec33667f	dropIndex indexName=IDX_OFFLINE_USS_BY_USERSESS, tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
25.0.0-28265-index-cleanup-css-preload	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.40244	131	EXECUTED	9:5411d2fb2891d3e8d63ddb55dfa3c0c9	dropIndex indexName=IDX_OFFLINE_CSS_PRELOAD, tableName=OFFLINE_CLIENT_SESSION		\N	4.29.1	\N	\N	4040861400
25.0.0-28265-index-2-mysql	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.403998	132	MARK_RAN	9:b7ef76036d3126bb83c2423bf4d449d6	createIndex indexName=IDX_OFFLINE_USS_BY_BROKER_SESSION_ID, tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
25.0.0-28265-index-2-not-mysql	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.436636	133	EXECUTED	9:23396cf51ab8bc1ae6f0cac7f9f6fcf7	createIndex indexName=IDX_OFFLINE_USS_BY_BROKER_SESSION_ID, tableName=OFFLINE_USER_SESSION		\N	4.29.1	\N	\N	4040861400
25.0.0-org	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.44475	134	EXECUTED	9:5c859965c2c9b9c72136c360649af157	createTable tableName=ORG; addUniqueConstraint constraintName=UK_ORG_NAME, tableName=ORG; addUniqueConstraint constraintName=UK_ORG_GROUP, tableName=ORG; createTable tableName=ORG_DOMAIN		\N	4.29.1	\N	\N	4040861400
unique-consentuser	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.451595	135	EXECUTED	9:5857626a2ea8767e9a6c66bf3a2cb32f	customChange; dropUniqueConstraint constraintName=UK_JKUWUVD56ONTGSUHOGM8UEWRT, tableName=USER_CONSENT; addUniqueConstraint constraintName=UK_LOCAL_CONSENT, tableName=USER_CONSENT; addUniqueConstraint constraintName=UK_EXTERNAL_CONSENT, tableName=...		\N	4.29.1	\N	\N	4040861400
unique-consentuser-mysql	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.453312	136	MARK_RAN	9:b79478aad5adaa1bc428e31563f55e8e	customChange; dropUniqueConstraint constraintName=UK_JKUWUVD56ONTGSUHOGM8UEWRT, tableName=USER_CONSENT; addUniqueConstraint constraintName=UK_LOCAL_CONSENT, tableName=USER_CONSENT; addUniqueConstraint constraintName=UK_EXTERNAL_CONSENT, tableName=...		\N	4.29.1	\N	\N	4040861400
25.0.0-28861-index-creation	keycloak	META-INF/jpa-changelog-25.0.0.xml	2025-08-01 09:34:27.518284	137	EXECUTED	9:b9acb58ac958d9ada0fe12a5d4794ab1	createIndex indexName=IDX_PERM_TICKET_REQUESTER, tableName=RESOURCE_SERVER_PERM_TICKET; createIndex indexName=IDX_PERM_TICKET_OWNER, tableName=RESOURCE_SERVER_PERM_TICKET		\N	4.29.1	\N	\N	4040861400
26.0.0-org-alias	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.524107	138	EXECUTED	9:6ef7d63e4412b3c2d66ed179159886a4	addColumn tableName=ORG; update tableName=ORG; addNotNullConstraint columnName=ALIAS, tableName=ORG; addUniqueConstraint constraintName=UK_ORG_ALIAS, tableName=ORG		\N	4.29.1	\N	\N	4040861400
26.0.0-org-group	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.53077	139	EXECUTED	9:da8e8087d80ef2ace4f89d8c5b9ca223	addColumn tableName=KEYCLOAK_GROUP; update tableName=KEYCLOAK_GROUP; addNotNullConstraint columnName=TYPE, tableName=KEYCLOAK_GROUP; customChange		\N	4.29.1	\N	\N	4040861400
26.0.0-org-indexes	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.571431	140	EXECUTED	9:79b05dcd610a8c7f25ec05135eec0857	createIndex indexName=IDX_ORG_DOMAIN_ORG_ID, tableName=ORG_DOMAIN		\N	4.29.1	\N	\N	4040861400
26.0.0-org-group-membership	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.581064	141	EXECUTED	9:a6ace2ce583a421d89b01ba2a28dc2d4	addColumn tableName=USER_GROUP_MEMBERSHIP; update tableName=USER_GROUP_MEMBERSHIP; addNotNullConstraint columnName=MEMBERSHIP_TYPE, tableName=USER_GROUP_MEMBERSHIP		\N	4.29.1	\N	\N	4040861400
31296-persist-revoked-access-tokens	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.586649	142	EXECUTED	9:64ef94489d42a358e8304b0e245f0ed4	createTable tableName=REVOKED_TOKEN; addPrimaryKey constraintName=CONSTRAINT_RT, tableName=REVOKED_TOKEN		\N	4.29.1	\N	\N	4040861400
31725-index-persist-revoked-access-tokens	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.622904	143	EXECUTED	9:b994246ec2bf7c94da881e1d28782c7b	createIndex indexName=IDX_REV_TOKEN_ON_EXPIRE, tableName=REVOKED_TOKEN		\N	4.29.1	\N	\N	4040861400
26.0.0-idps-for-login	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.699689	144	EXECUTED	9:51f5fffadf986983d4bd59582c6c1604	addColumn tableName=IDENTITY_PROVIDER; createIndex indexName=IDX_IDP_REALM_ORG, tableName=IDENTITY_PROVIDER; createIndex indexName=IDX_IDP_FOR_LOGIN, tableName=IDENTITY_PROVIDER; customChange		\N	4.29.1	\N	\N	4040861400
26.0.0-32583-drop-redundant-index-on-client-session	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.833961	145	EXECUTED	9:24972d83bf27317a055d234187bb4af9	dropIndex indexName=IDX_US_SESS_ID_ON_CL_SESS, tableName=OFFLINE_CLIENT_SESSION		\N	4.29.1	\N	\N	4040861400
26.0.0.32582-remove-tables-user-session-user-session-note-and-client-session	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.842903	146	EXECUTED	9:febdc0f47f2ed241c59e60f58c3ceea5	dropTable tableName=CLIENT_SESSION_ROLE; dropTable tableName=CLIENT_SESSION_NOTE; dropTable tableName=CLIENT_SESSION_PROT_MAPPER; dropTable tableName=CLIENT_SESSION_AUTH_STATUS; dropTable tableName=CLIENT_USER_SESSION_NOTE; dropTable tableName=CLI...		\N	4.29.1	\N	\N	4040861400
26.0.0-33201-org-redirect-url	keycloak	META-INF/jpa-changelog-26.0.0.xml	2025-08-01 09:34:27.846269	147	EXECUTED	9:4d0e22b0ac68ebe9794fa9cb752ea660	addColumn tableName=ORG		\N	4.29.1	\N	\N	4040861400
29399-jdbc-ping-default	keycloak	META-INF/jpa-changelog-26.1.0.xml	2025-08-01 09:34:27.851888	148	EXECUTED	9:007dbe99d7203fca403b89d4edfdf21e	createTable tableName=JGROUPS_PING; addPrimaryKey constraintName=CONSTRAINT_JGROUPS_PING, tableName=JGROUPS_PING		\N	4.29.1	\N	\N	4040861400
26.1.0-34013	keycloak	META-INF/jpa-changelog-26.1.0.xml	2025-08-01 09:34:27.857795	149	EXECUTED	9:e6b686a15759aef99a6d758a5c4c6a26	addColumn tableName=ADMIN_EVENT_ENTITY		\N	4.29.1	\N	\N	4040861400
26.1.0-34380	keycloak	META-INF/jpa-changelog-26.1.0.xml	2025-08-01 09:34:27.860795	150	EXECUTED	9:ac8b9edb7c2b6c17a1c7a11fcf5ccf01	dropTable tableName=USERNAME_LOGIN_FAILURE		\N	4.29.1	\N	\N	4040861400
26.2.0-36750	keycloak	META-INF/jpa-changelog-26.2.0.xml	2025-08-01 09:34:27.865148	151	EXECUTED	9:b49ce951c22f7eb16480ff085640a33a	createTable tableName=SERVER_CONFIG		\N	4.29.1	\N	\N	4040861400
26.2.0-26106	keycloak	META-INF/jpa-changelog-26.2.0.xml	2025-08-01 09:34:27.868007	152	EXECUTED	9:b5877d5dab7d10ff3a9d209d7beb6680	addColumn tableName=CREDENTIAL		\N	4.29.1	\N	\N	4040861400
26.2.6-39866-duplicate	keycloak	META-INF/jpa-changelog-26.2.6.xml	2025-08-01 09:34:27.870966	153	EXECUTED	9:1dc67ccee24f30331db2cba4f372e40e	customChange		\N	4.29.1	\N	\N	4040861400
26.2.6-39866-uk	keycloak	META-INF/jpa-changelog-26.2.6.xml	2025-08-01 09:34:27.873902	154	EXECUTED	9:b70b76f47210cf0a5f4ef0e219eac7cd	addUniqueConstraint constraintName=UK_MIGRATION_VERSION, tableName=MIGRATION_MODEL		\N	4.29.1	\N	\N	4040861400
26.2.6-40088-duplicate	keycloak	META-INF/jpa-changelog-26.2.6.xml	2025-08-01 09:34:27.876788	155	EXECUTED	9:cc7e02ed69ab31979afb1982f9670e8f	customChange		\N	4.29.1	\N	\N	4040861400
26.2.6-40088-uk	keycloak	META-INF/jpa-changelog-26.2.6.xml	2025-08-01 09:34:27.879396	156	EXECUTED	9:5bb848128da7bc4595cc507383325241	addUniqueConstraint constraintName=UK_MIGRATION_UPDATE_TIME, tableName=MIGRATION_MODEL		\N	4.29.1	\N	\N	4040861400
26.3.0-groups-description	keycloak	META-INF/jpa-changelog-26.3.0.xml	2025-08-01 09:34:27.882625	157	EXECUTED	9:e1a3c05574326fb5b246b73b9a4c4d49	addColumn tableName=KEYCLOAK_GROUP		\N	4.29.1	\N	\N	4040861400
\.


--
-- TOC entry 4153 (class 0 OID 16386)
-- Dependencies: 217
-- Data for Name: databasechangeloglock; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.databasechangeloglock (id, locked, lockgranted, lockedby) FROM stdin;
1	f	\N	\N
1000	f	\N	\N
\.


--
-- TOC entry 4228 (class 0 OID 17782)
-- Dependencies: 292
-- Data for Name: default_client_scope; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.default_client_scope (realm_id, scope_id, default_scope) FROM stdin;
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	b388e2bc-bacc-4f48-93cf-52408edbb05d	f
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	79c36a51-655c-4e85-93d6-9830b3978dae	t
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	5fcc4963-a080-43db-a0b2-a0dec44ca4b0	t
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	c81ce17d-7ad4-432e-a19d-69719f95deb5	t
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	bee3d7eb-817c-4fde-998a-a6b0ca227c64	t
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	c8ceba4a-ba7a-4fbe-811e-5d60126fc508	f
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	37062dfb-3d8d-4878-9fff-b84f9cea09dd	f
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	13872a50-75a4-4d61-9b8d-29ee9925e3bd	t
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	1fff4531-7378-462d-899b-45c91f87398d	t
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3	f
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	399bea11-410b-47f6-aec7-14fc5292312b	t
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	6f5f268b-8c54-430d-ab31-18cf136b31b7	t
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	d631f21c-5c6b-4e79-bde9-3276d2eb2eff	f
1bcb83bb-6f51-410d-972e-8077c4359429	0c348ee7-e445-446d-a027-46f6033d5bf9	f
1bcb83bb-6f51-410d-972e-8077c4359429	0e236cce-035e-408a-b9c5-4c5b44f48fa5	t
1bcb83bb-6f51-410d-972e-8077c4359429	8adadaca-c13b-46f1-bec5-95a9ed2f85de	t
1bcb83bb-6f51-410d-972e-8077c4359429	1aab8122-bbd8-4659-8067-089a57fbc21b	t
1bcb83bb-6f51-410d-972e-8077c4359429	433e3125-9ba0-46a3-95cd-bb58d1932256	t
1bcb83bb-6f51-410d-972e-8077c4359429	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5	f
1bcb83bb-6f51-410d-972e-8077c4359429	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a	f
1bcb83bb-6f51-410d-972e-8077c4359429	1768dcff-3cc3-4d32-839a-13e3a381b09b	t
1bcb83bb-6f51-410d-972e-8077c4359429	c3787d89-cd29-4cfc-9e33-f484915204fc	t
1bcb83bb-6f51-410d-972e-8077c4359429	82b5f4a5-829c-49be-b4ed-561172ed59af	f
1bcb83bb-6f51-410d-972e-8077c4359429	15af2cf2-5174-41b9-b356-ef154a709ceb	t
1bcb83bb-6f51-410d-972e-8077c4359429	c03d35bc-76a3-46a6-a187-38ea2dad9020	t
1bcb83bb-6f51-410d-972e-8077c4359429	68cab4d6-3871-4a3d-94ca-294ca1c715a1	f
1bcb83bb-6f51-410d-972e-8077c4359429	da252e95-380c-4844-b05e-d50eaf94a2b2	t
\.


--
-- TOC entry 4158 (class 0 OID 16426)
-- Dependencies: 222
-- Data for Name: event_entity; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.event_entity (id, client_id, details_json, error, ip_address, realm_id, session_id, event_time, type, user_id, details_json_long_value) FROM stdin;
\.


--
-- TOC entry 4216 (class 0 OID 17481)
-- Dependencies: 280
-- Data for Name: fed_user_attribute; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.fed_user_attribute (id, name, user_id, realm_id, storage_provider_id, value, long_value_hash, long_value_hash_lower_case, long_value) FROM stdin;
\.


--
-- TOC entry 4217 (class 0 OID 17486)
-- Dependencies: 281
-- Data for Name: fed_user_consent; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.fed_user_consent (id, client_id, user_id, realm_id, storage_provider_id, created_date, last_updated_date, client_storage_provider, external_client_id) FROM stdin;
\.


--
-- TOC entry 4230 (class 0 OID 17808)
-- Dependencies: 294
-- Data for Name: fed_user_consent_cl_scope; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.fed_user_consent_cl_scope (user_consent_id, scope_id) FROM stdin;
\.


--
-- TOC entry 4218 (class 0 OID 17495)
-- Dependencies: 282
-- Data for Name: fed_user_credential; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.fed_user_credential (id, salt, type, created_date, user_id, realm_id, storage_provider_id, user_label, secret_data, credential_data, priority) FROM stdin;
\.


--
-- TOC entry 4219 (class 0 OID 17504)
-- Dependencies: 283
-- Data for Name: fed_user_group_membership; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.fed_user_group_membership (group_id, user_id, realm_id, storage_provider_id) FROM stdin;
\.


--
-- TOC entry 4220 (class 0 OID 17507)
-- Dependencies: 284
-- Data for Name: fed_user_required_action; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.fed_user_required_action (required_action, user_id, realm_id, storage_provider_id) FROM stdin;
\.


--
-- TOC entry 4221 (class 0 OID 17513)
-- Dependencies: 285
-- Data for Name: fed_user_role_mapping; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.fed_user_role_mapping (role_id, user_id, realm_id, storage_provider_id) FROM stdin;
\.


--
-- TOC entry 4178 (class 0 OID 16803)
-- Dependencies: 242
-- Data for Name: federated_identity; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.federated_identity (identity_provider, realm_id, federated_user_id, federated_username, token, user_id) FROM stdin;
\.


--
-- TOC entry 4224 (class 0 OID 17578)
-- Dependencies: 288
-- Data for Name: federated_user; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.federated_user (id, storage_provider_id, realm_id) FROM stdin;
\.


--
-- TOC entry 4200 (class 0 OID 17205)
-- Dependencies: 264
-- Data for Name: group_attribute; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.group_attribute (id, name, value, group_id) FROM stdin;
0d0ea640-431f-49e9-9eba-381496f07875	tenantId	aaa	bce44866-71b9-4f39-a015-eb9e0e0484b0
\.


--
-- TOC entry 4199 (class 0 OID 17202)
-- Dependencies: 263
-- Data for Name: group_role_mapping; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.group_role_mapping (role_id, group_id) FROM stdin;
\.


--
-- TOC entry 4179 (class 0 OID 16808)
-- Dependencies: 243
-- Data for Name: identity_provider; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.identity_provider (internal_id, enabled, provider_alias, provider_id, store_token, authenticate_by_default, realm_id, add_token_role, trust_email, first_broker_login_flow_id, post_broker_login_flow_id, provider_display_name, link_only, organization_id, hide_on_login) FROM stdin;
\.


--
-- TOC entry 4180 (class 0 OID 16817)
-- Dependencies: 244
-- Data for Name: identity_provider_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.identity_provider_config (identity_provider_id, value, name) FROM stdin;
\.


--
-- TOC entry 4184 (class 0 OID 16921)
-- Dependencies: 248
-- Data for Name: identity_provider_mapper; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.identity_provider_mapper (id, name, idp_alias, idp_mapper_name, realm_id) FROM stdin;
\.


--
-- TOC entry 4185 (class 0 OID 16926)
-- Dependencies: 249
-- Data for Name: idp_mapper_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.idp_mapper_config (idp_mapper_id, value, name) FROM stdin;
\.


--
-- TOC entry 4239 (class 0 OID 18007)
-- Dependencies: 303
-- Data for Name: jgroups_ping; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.jgroups_ping (address, name, cluster_name, ip, coord) FROM stdin;
uuid://5de08a5d-7109-4fd6-92a8-d516a4d5ea89	6fd814efaf82-4620	ISPN	172.18.0.4:7800	t
\.


--
-- TOC entry 4198 (class 0 OID 17199)
-- Dependencies: 262
-- Data for Name: keycloak_group; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.keycloak_group (id, name, parent_group, realm_id, type, description) FROM stdin;
bce44866-71b9-4f39-a015-eb9e0e0484b0	tenant	 	1bcb83bb-6f51-410d-972e-8077c4359429	0	
\.


--
-- TOC entry 4159 (class 0 OID 16434)
-- Dependencies: 223
-- Data for Name: keycloak_role; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.keycloak_role (id, client_realm_constraint, client_role, description, name, realm_id, client, realm) FROM stdin;
24925405-e2df-4f44-bf4d-d5fa744940eb	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f	${role_default-roles}	default-roles-master	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N	\N
4471eb15-347d-498b-afab-ed77573880f4	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f	${role_create-realm}	create-realm	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N	\N
4afc773e-1b6d-43f3-8b05-003cf2f29f59	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f	${role_admin}	admin	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N	\N
2b6df356-d878-488b-8f7c-94f3efa1d778	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_create-client}	create-client	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
315a3a83-49d7-4eb0-a367-1f88ac9adcd1	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_view-realm}	view-realm	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
d2bbdaf0-ea14-46f7-8cb4-21287d3031ad	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_view-users}	view-users	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
9c453c82-9b85-44d6-94a7-21f7a6513fd2	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_view-clients}	view-clients	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
fad21920-7b7e-4383-8a9d-c15fd751b3c1	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_view-events}	view-events	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
5836db42-308a-495d-8a62-19e175a76bbe	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_view-identity-providers}	view-identity-providers	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
d2cfaad6-f23c-4cff-90af-6925482d1832	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_view-authorization}	view-authorization	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
ce2fd79c-5f14-4c1b-bc91-bfa3b672d7d2	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_manage-realm}	manage-realm	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
c81320e7-0cd8-4eba-8708-51d4002c0319	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_manage-users}	manage-users	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
1e29dd2a-15fa-469a-8b86-61f9ac3bcece	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_manage-clients}	manage-clients	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
55294547-337c-4752-990b-0c5efdcd3615	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_manage-events}	manage-events	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
e4a6b5d2-e78a-4caa-b779-745088aec4f5	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_manage-identity-providers}	manage-identity-providers	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
34745d81-2d45-430e-ab98-d3e1fd43023c	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_manage-authorization}	manage-authorization	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
938b5b0b-4c9e-47c3-9d4d-5ae026fae484	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_query-users}	query-users	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
b55ac2b0-a257-4550-b41f-41cf252cd199	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_query-clients}	query-clients	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
6a6995ca-e767-4bff-8595-dcd4fe7e8646	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_query-realms}	query-realms	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
aa4998a4-1e82-4780-bfcc-914122c8ea85	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_query-groups}	query-groups	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
e26c0213-98e9-456d-bb87-f7d312e3659d	955b4b55-16f3-472b-a919-6e4969b3feb0	t	${role_view-profile}	view-profile	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	955b4b55-16f3-472b-a919-6e4969b3feb0	\N
af51e7ce-a9f4-4496-b26d-7484186e701a	955b4b55-16f3-472b-a919-6e4969b3feb0	t	${role_manage-account}	manage-account	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	955b4b55-16f3-472b-a919-6e4969b3feb0	\N
09fca297-5f0d-47ef-9fff-1587d42f7963	955b4b55-16f3-472b-a919-6e4969b3feb0	t	${role_manage-account-links}	manage-account-links	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	955b4b55-16f3-472b-a919-6e4969b3feb0	\N
59e7ed4f-b183-413b-8758-5b5d94ac0e3c	955b4b55-16f3-472b-a919-6e4969b3feb0	t	${role_view-applications}	view-applications	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	955b4b55-16f3-472b-a919-6e4969b3feb0	\N
c2c3dc9b-97d9-4e3a-85f8-670569baced6	955b4b55-16f3-472b-a919-6e4969b3feb0	t	${role_view-consent}	view-consent	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	955b4b55-16f3-472b-a919-6e4969b3feb0	\N
f7e88885-32ef-4288-9bef-358615cb95b4	955b4b55-16f3-472b-a919-6e4969b3feb0	t	${role_manage-consent}	manage-consent	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	955b4b55-16f3-472b-a919-6e4969b3feb0	\N
f0e24b7d-6967-46f1-b83e-8f6abc1963ff	955b4b55-16f3-472b-a919-6e4969b3feb0	t	${role_view-groups}	view-groups	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	955b4b55-16f3-472b-a919-6e4969b3feb0	\N
555d956c-9a24-4f27-bd70-db758eea0125	955b4b55-16f3-472b-a919-6e4969b3feb0	t	${role_delete-account}	delete-account	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	955b4b55-16f3-472b-a919-6e4969b3feb0	\N
c8e50396-a028-4666-96c5-b9f06f7a24e6	c5de8543-d5d7-4480-ac34-5ace58f94217	t	${role_read-token}	read-token	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	c5de8543-d5d7-4480-ac34-5ace58f94217	\N
9bdd1a2d-75c8-41d3-a30b-5b8ab85446e8	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	t	${role_impersonation}	impersonation	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	\N
2e74a5cf-4b39-44c1-af0f-10cd748b824a	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f	${role_offline-access}	offline_access	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N	\N
a1f34ffe-8ad1-4652-92b0-56e379c12bdd	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f	${role_uma_authorization}	uma_authorization	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	\N	\N
76f8cb98-3d7b-47e6-abed-d3ebcd7203bc	1bcb83bb-6f51-410d-972e-8077c4359429	f	${role_default-roles}	default-roles-relma	1bcb83bb-6f51-410d-972e-8077c4359429	\N	\N
13cb3e3e-2b13-4fcf-a213-df43c69e6c25	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_create-client}	create-client	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
54c661ba-df7d-44f9-9d42-4a1984b05929	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_view-realm}	view-realm	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
82123da4-c05a-438a-9d70-ccec8c9c2929	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_view-users}	view-users	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
ec643a18-1b51-43b8-9f1f-a24edab0bbf7	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_view-clients}	view-clients	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
6ac69d11-4a22-4486-8b9f-b6693ae27451	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_view-events}	view-events	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
4048e81f-1ad4-4204-b616-163d5be753a8	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_view-identity-providers}	view-identity-providers	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
32a2526c-0e96-4fc9-a7a3-0d7aae8a6c7e	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_view-authorization}	view-authorization	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
dce4698f-17cf-429d-a13d-c39fa854a95b	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_manage-realm}	manage-realm	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
56a7543d-5aa4-4af0-aa08-efa12b77c534	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_manage-users}	manage-users	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
0efb0a8b-382c-4ad7-8df6-d40840bf1d57	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_manage-clients}	manage-clients	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
9976f021-4f64-4045-be49-38c34af66a49	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_manage-events}	manage-events	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
71dc8d93-e088-4292-8286-9be391eb1051	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_manage-identity-providers}	manage-identity-providers	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
c143823c-5d99-4e2e-b205-01490c945d7f	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_manage-authorization}	manage-authorization	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
4aac88e0-a9b8-4603-9ea0-6383771b32a8	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_query-users}	query-users	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
8cee43c4-0ef0-41ed-8932-3bd131f0688b	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_query-clients}	query-clients	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
ed2ae8d7-cda9-4bda-9534-e4b08a5e620c	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_query-realms}	query-realms	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
019ff951-c22e-4e14-80db-f12b8a695cf9	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_query-groups}	query-groups	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
d41d1279-53ad-4dce-9aaa-ade0cf75a6e6	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_realm-admin}	realm-admin	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
471814d4-9805-436b-bee7-cde1b6f26602	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_create-client}	create-client	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
0bb47142-0a8b-4f8d-a879-6fde07a1c14d	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_view-realm}	view-realm	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
da3c4458-92c0-4ad1-b669-c29e08ddd60d	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_view-users}	view-users	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
813bda42-1366-41a5-8981-a762cc5b9339	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_view-clients}	view-clients	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
8bf6c28b-a4b1-4591-a3a7-e49cb02dcef7	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_view-events}	view-events	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
c2e3792f-bdbd-456c-bb35-bff1ad0dc14e	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_view-identity-providers}	view-identity-providers	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
94217f58-5cd7-4915-94aa-1e6fff4d6ee6	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_view-authorization}	view-authorization	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
e6e31dd1-9758-4fb5-843f-3377668edfa4	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_manage-realm}	manage-realm	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
3bf5a748-8765-4683-acfd-2dd6c9bddfb4	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_manage-users}	manage-users	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
69db5f58-882b-4a6a-a946-5b5c59635c27	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_manage-clients}	manage-clients	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
e85dc812-0a9f-432d-9690-e96f1981556f	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_manage-events}	manage-events	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
fadc6525-7ee4-4de8-afa0-f95798e7eb9f	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_manage-identity-providers}	manage-identity-providers	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
e5269f15-3b43-447c-bc63-7da9a6167e3b	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_manage-authorization}	manage-authorization	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
ca29d6e4-9644-48fd-b0d4-9047ec73ff3e	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_query-users}	query-users	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
ac948911-7f4e-43b5-b8c5-65d64a4a5a98	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_query-clients}	query-clients	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
caef55ae-e2ff-429f-ba83-7ceac4f3ca0e	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_query-realms}	query-realms	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
09cc5765-0d40-494a-beea-9471dcceae4e	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_query-groups}	query-groups	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
6f706bc4-76ef-4c07-aa36-f9c48a92304e	a647b04c-25f4-4103-b180-ab357a7e456a	t	${role_view-profile}	view-profile	1bcb83bb-6f51-410d-972e-8077c4359429	a647b04c-25f4-4103-b180-ab357a7e456a	\N
fcfbb7b5-abf7-4cb0-a873-2bb4998f9514	a647b04c-25f4-4103-b180-ab357a7e456a	t	${role_manage-account}	manage-account	1bcb83bb-6f51-410d-972e-8077c4359429	a647b04c-25f4-4103-b180-ab357a7e456a	\N
ac23bde3-a9bd-4966-8efe-2c3c3b0f1f2e	a647b04c-25f4-4103-b180-ab357a7e456a	t	${role_manage-account-links}	manage-account-links	1bcb83bb-6f51-410d-972e-8077c4359429	a647b04c-25f4-4103-b180-ab357a7e456a	\N
47c87139-4157-40a2-89ee-5b7c2c2b34d5	a647b04c-25f4-4103-b180-ab357a7e456a	t	${role_view-applications}	view-applications	1bcb83bb-6f51-410d-972e-8077c4359429	a647b04c-25f4-4103-b180-ab357a7e456a	\N
09d60347-7145-4e7f-9f68-d0ae2bc98203	a647b04c-25f4-4103-b180-ab357a7e456a	t	${role_view-consent}	view-consent	1bcb83bb-6f51-410d-972e-8077c4359429	a647b04c-25f4-4103-b180-ab357a7e456a	\N
f08e2a57-934b-47c6-8bdc-5322b4c4cd69	a647b04c-25f4-4103-b180-ab357a7e456a	t	${role_manage-consent}	manage-consent	1bcb83bb-6f51-410d-972e-8077c4359429	a647b04c-25f4-4103-b180-ab357a7e456a	\N
390ee334-47d6-4db3-bb73-812758808e33	a647b04c-25f4-4103-b180-ab357a7e456a	t	${role_view-groups}	view-groups	1bcb83bb-6f51-410d-972e-8077c4359429	a647b04c-25f4-4103-b180-ab357a7e456a	\N
8381a86d-4ee8-4ea1-a0df-250796a1e1cc	a647b04c-25f4-4103-b180-ab357a7e456a	t	${role_delete-account}	delete-account	1bcb83bb-6f51-410d-972e-8077c4359429	a647b04c-25f4-4103-b180-ab357a7e456a	\N
1969f546-97e5-49fe-8153-0f02883c18a7	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	t	${role_impersonation}	impersonation	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	\N
1db0683a-1343-44b5-97e6-2b1cc7cff357	8c641705-f255-4386-ae8b-799b374d8ba5	t	${role_impersonation}	impersonation	1bcb83bb-6f51-410d-972e-8077c4359429	8c641705-f255-4386-ae8b-799b374d8ba5	\N
4d5f1898-7029-4020-83c7-16254524d91c	1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	t	${role_read-token}	read-token	1bcb83bb-6f51-410d-972e-8077c4359429	1338ab2b-02b0-4ed1-b3f4-29ed9d9d6c14	\N
94735068-d221-4f3b-bdd0-075e38c1d87f	1bcb83bb-6f51-410d-972e-8077c4359429	f	${role_offline-access}	offline_access	1bcb83bb-6f51-410d-972e-8077c4359429	\N	\N
73a69fd4-267e-4c33-9421-aa970a21f443	1bcb83bb-6f51-410d-972e-8077c4359429	f	${role_uma_authorization}	uma_authorization	1bcb83bb-6f51-410d-972e-8077c4359429	\N	\N
\.


--
-- TOC entry 4183 (class 0 OID 16918)
-- Dependencies: 247
-- Data for Name: migration_model; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.migration_model (id, version, update_time) FROM stdin;
nszf8	26.3.2	1754040869
\.


--
-- TOC entry 4197 (class 0 OID 17190)
-- Dependencies: 261
-- Data for Name: offline_client_session; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.offline_client_session (user_session_id, client_id, offline_flag, "timestamp", data, client_storage_provider, external_client_id, version) FROM stdin;
\.


--
-- TOC entry 4196 (class 0 OID 17185)
-- Dependencies: 260
-- Data for Name: offline_user_session; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.offline_user_session (user_session_id, user_id, realm_id, created_on, offline_flag, data, last_session_refresh, broker_session_id, version) FROM stdin;
\.


--
-- TOC entry 4236 (class 0 OID 17970)
-- Dependencies: 300
-- Data for Name: org; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.org (id, enabled, realm_id, group_id, name, description, alias, redirect_url) FROM stdin;
\.


--
-- TOC entry 4237 (class 0 OID 17981)
-- Dependencies: 301
-- Data for Name: org_domain; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.org_domain (id, name, verified, org_id) FROM stdin;
\.


--
-- TOC entry 4210 (class 0 OID 17404)
-- Dependencies: 274
-- Data for Name: policy_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.policy_config (policy_id, name, value) FROM stdin;
\.


--
-- TOC entry 4176 (class 0 OID 16792)
-- Dependencies: 240
-- Data for Name: protocol_mapper; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.protocol_mapper (id, name, protocol, protocol_mapper_name, client_id, client_scope_id) FROM stdin;
f5c50eae-e156-4545-817f-d397132327f9	audience resolve	openid-connect	oidc-audience-resolve-mapper	31102431-0756-40bd-bffb-0757ab96be0d	\N
a1acba4f-9fb1-435f-9e94-5b91c3af7edd	locale	openid-connect	oidc-usermodel-attribute-mapper	07df0e9c-0213-48a6-b89f-658593bde218	\N
e1af2d33-8c8e-4b6f-989c-b92ae83b023a	role list	saml	saml-role-list-mapper	\N	79c36a51-655c-4e85-93d6-9830b3978dae
fc2d9dd0-fea0-4a9c-bd7d-49e0e620b869	organization	saml	saml-organization-membership-mapper	\N	5fcc4963-a080-43db-a0b2-a0dec44ca4b0
39575146-e29f-4d49-ba98-e0660b54a0b5	full name	openid-connect	oidc-full-name-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
3c34854f-faa6-4d55-9f8a-f55873fadd72	family name	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
a029aa90-6323-4825-a37f-cd336d4252e3	given name	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
051da8aa-37ab-40fe-9063-de016770e1f2	middle name	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
3f900954-4c48-45cd-bae2-e74a3ec9f359	nickname	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
4477938f-4b47-4d03-9dcb-1d3b5227df3f	username	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
39488642-33be-4650-a6a0-91248d60f580	profile	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
a02fc4de-9b0d-495d-8719-bfb341cbb722	picture	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
31cfa8b9-b315-46f5-b2d6-bf6e312317cc	website	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
9f351f19-7d91-40c5-a10a-5dfe33a20a99	gender	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
0b0a4dae-2771-4c0f-b8e1-21090285dcfe	birthdate	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
3f3c5c7d-2687-4c1c-9805-cf20f8e3ce6f	zoneinfo	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
f5fe6a3c-81a8-480e-9986-8d1d867de7b9	locale	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
c58fb3ed-9bb1-4b39-99f4-42cbed0f9f4d	updated at	openid-connect	oidc-usermodel-attribute-mapper	\N	c81ce17d-7ad4-432e-a19d-69719f95deb5
c659f014-e11e-4a15-bc68-927a710d79c1	email	openid-connect	oidc-usermodel-attribute-mapper	\N	bee3d7eb-817c-4fde-998a-a6b0ca227c64
8f3db9a9-5384-4680-be21-4fa111c08b90	email verified	openid-connect	oidc-usermodel-property-mapper	\N	bee3d7eb-817c-4fde-998a-a6b0ca227c64
9b84f95f-dd36-462c-b6de-bfe91586c8a2	address	openid-connect	oidc-address-mapper	\N	c8ceba4a-ba7a-4fbe-811e-5d60126fc508
f032af6f-3d67-419c-9d17-9e94213805b7	phone number	openid-connect	oidc-usermodel-attribute-mapper	\N	37062dfb-3d8d-4878-9fff-b84f9cea09dd
2bb2df5b-ae62-4752-b738-2ae254cc339c	phone number verified	openid-connect	oidc-usermodel-attribute-mapper	\N	37062dfb-3d8d-4878-9fff-b84f9cea09dd
0273f296-2e30-4573-a1d9-61ce7630f189	realm roles	openid-connect	oidc-usermodel-realm-role-mapper	\N	13872a50-75a4-4d61-9b8d-29ee9925e3bd
125fb530-b45f-479d-aa7e-708883a6bdb1	client roles	openid-connect	oidc-usermodel-client-role-mapper	\N	13872a50-75a4-4d61-9b8d-29ee9925e3bd
29562b16-3b74-4f85-bbea-053d671f99cd	audience resolve	openid-connect	oidc-audience-resolve-mapper	\N	13872a50-75a4-4d61-9b8d-29ee9925e3bd
f6da39f4-7c8e-4363-8e57-7c950269fb67	allowed web origins	openid-connect	oidc-allowed-origins-mapper	\N	1fff4531-7378-462d-899b-45c91f87398d
9b78d02d-de41-4e21-80ed-a94f1e95e347	upn	openid-connect	oidc-usermodel-attribute-mapper	\N	910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3
b6bdb781-2582-4d37-bfcb-c98853e2a575	groups	openid-connect	oidc-usermodel-realm-role-mapper	\N	910c7681-ceb5-4ba1-bdfd-b0f17cfaf8d3
9c785a92-3e4a-45e0-b4ae-38ddce1e39dc	acr loa level	openid-connect	oidc-acr-mapper	\N	399bea11-410b-47f6-aec7-14fc5292312b
fd80aaa4-847c-4eab-94c5-1b0a189d730e	auth_time	openid-connect	oidc-usersessionmodel-note-mapper	\N	6f5f268b-8c54-430d-ab31-18cf136b31b7
d597233c-b09b-4466-939d-a27cb7e8c442	sub	openid-connect	oidc-sub-mapper	\N	6f5f268b-8c54-430d-ab31-18cf136b31b7
3375d80b-bf74-4aa0-ab99-51c20b9915ee	Client ID	openid-connect	oidc-usersessionmodel-note-mapper	\N	bb5760a9-c212-489e-9dd3-255eae1434a4
b937c8d8-7ac6-48af-a91f-284cab27fa97	Client Host	openid-connect	oidc-usersessionmodel-note-mapper	\N	bb5760a9-c212-489e-9dd3-255eae1434a4
6ac76831-679e-4c1d-af72-5712fa3092db	Client IP Address	openid-connect	oidc-usersessionmodel-note-mapper	\N	bb5760a9-c212-489e-9dd3-255eae1434a4
84dd823a-50f4-4994-9768-40c54d84213f	organization	openid-connect	oidc-organization-membership-mapper	\N	d631f21c-5c6b-4e79-bde9-3276d2eb2eff
385798bd-8f2e-4b7e-be34-1ee27aa58ad0	audience resolve	openid-connect	oidc-audience-resolve-mapper	a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	\N
253bbc57-e291-4be0-ba86-a6749782470d	role list	saml	saml-role-list-mapper	\N	0e236cce-035e-408a-b9c5-4c5b44f48fa5
dc0b545e-c486-4547-96bc-f00e9f922fa8	organization	saml	saml-organization-membership-mapper	\N	8adadaca-c13b-46f1-bec5-95a9ed2f85de
b27286ab-5a72-487b-a801-3d5efaca23a2	full name	openid-connect	oidc-full-name-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
a9355b10-c7fc-4a3d-98d3-bea1b0feeafd	family name	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
4e95e4a5-00fe-453f-99fa-d163da3c6803	given name	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
b1f4725e-bc15-4b0a-8672-3b53a75e353b	middle name	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
9c9142b8-7b1d-4f12-9143-0732cd598733	nickname	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
4554ed41-0c61-4eff-b495-5c83151f708c	username	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
757533ab-b763-42bd-a814-0ff784e5f156	profile	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
59734a17-c925-4276-a35c-cd926e5b4c91	picture	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
9a1c4230-a2e5-4a5e-85f3-04bf8d41fce0	website	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
6e29b0c8-d767-430b-8007-33c713c60fe7	gender	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
c4256356-7492-45d8-bf44-15d7ba40b11b	birthdate	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
f1f511d0-d915-4f7b-b551-f61411c0f177	zoneinfo	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
4f57c857-3d72-4793-b5f7-433a6c8e410c	locale	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
e9496210-64bb-488f-b9ca-4a2d221b77db	updated at	openid-connect	oidc-usermodel-attribute-mapper	\N	1aab8122-bbd8-4659-8067-089a57fbc21b
bb959414-fc03-4d13-ba9d-88cd2825c34b	email	openid-connect	oidc-usermodel-attribute-mapper	\N	433e3125-9ba0-46a3-95cd-bb58d1932256
50e48917-494f-414c-a3ff-5c5073b34fbf	email verified	openid-connect	oidc-usermodel-property-mapper	\N	433e3125-9ba0-46a3-95cd-bb58d1932256
c295b695-048b-4439-a6e8-6efbf8cfb179	address	openid-connect	oidc-address-mapper	\N	3cefbeb3-3f5f-46d0-ace3-5281ae0cb0e5
c5ab88f8-b582-44c8-83bf-a0dd0d56924e	phone number	openid-connect	oidc-usermodel-attribute-mapper	\N	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a
9c1c21ad-da5f-42ba-a502-2335eaf17ddd	phone number verified	openid-connect	oidc-usermodel-attribute-mapper	\N	d8a291bc-7ffd-4a19-bfc0-c4a65c672a5a
366c745c-a780-4e72-9f45-ff1b08900e87	realm roles	openid-connect	oidc-usermodel-realm-role-mapper	\N	1768dcff-3cc3-4d32-839a-13e3a381b09b
8977adb4-c253-4baa-a906-b3bac35caa6b	client roles	openid-connect	oidc-usermodel-client-role-mapper	\N	1768dcff-3cc3-4d32-839a-13e3a381b09b
02e36241-4d69-4fd2-94fa-2b167694041b	audience resolve	openid-connect	oidc-audience-resolve-mapper	\N	1768dcff-3cc3-4d32-839a-13e3a381b09b
25d4db89-e2e2-4564-8695-b7286c621501	allowed web origins	openid-connect	oidc-allowed-origins-mapper	\N	c3787d89-cd29-4cfc-9e33-f484915204fc
780d9991-71e6-4327-b315-de6328b15416	upn	openid-connect	oidc-usermodel-attribute-mapper	\N	82b5f4a5-829c-49be-b4ed-561172ed59af
026458da-c162-4193-8e5c-ebbdbaf04fb1	groups	openid-connect	oidc-usermodel-realm-role-mapper	\N	82b5f4a5-829c-49be-b4ed-561172ed59af
4aa8363c-ce8b-40ca-9c9a-c1782583475f	acr loa level	openid-connect	oidc-acr-mapper	\N	15af2cf2-5174-41b9-b356-ef154a709ceb
26a2f556-4268-4982-9dac-63c7a850f469	auth_time	openid-connect	oidc-usersessionmodel-note-mapper	\N	c03d35bc-76a3-46a6-a187-38ea2dad9020
b2a4a419-c721-4e3a-ad52-3b7d38a33c74	sub	openid-connect	oidc-sub-mapper	\N	c03d35bc-76a3-46a6-a187-38ea2dad9020
26c8804c-c690-4cc6-ab14-6107d5be2c53	Client ID	openid-connect	oidc-usersessionmodel-note-mapper	\N	f04da0a3-b1fa-4ccd-9cb7-104583bcb867
c0a03313-788a-4e8e-916e-3f3c3ba0c491	Client Host	openid-connect	oidc-usersessionmodel-note-mapper	\N	f04da0a3-b1fa-4ccd-9cb7-104583bcb867
42f3760c-f8b4-4213-a619-29326c654b21	Client IP Address	openid-connect	oidc-usersessionmodel-note-mapper	\N	f04da0a3-b1fa-4ccd-9cb7-104583bcb867
52aea26e-d214-49c5-a960-98839507fa60	organization	openid-connect	oidc-organization-membership-mapper	\N	68cab4d6-3871-4a3d-94ca-294ca1c715a1
91c2cfc0-0f21-41d0-9c2e-f610a01df15d	locale	openid-connect	oidc-usermodel-attribute-mapper	d1827d3b-91a1-4de9-b940-e3ac92e6ee63	\N
4706c8f8-6942-4a44-b6dc-e33a76a3ebd3	tenant_id	openid-connect	oidc-usermodel-attribute-mapper	\N	da252e95-380c-4844-b05e-d50eaf94a2b2
\.


--
-- TOC entry 4177 (class 0 OID 16798)
-- Dependencies: 241
-- Data for Name: protocol_mapper_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.protocol_mapper_config (protocol_mapper_id, value, name) FROM stdin;
a1acba4f-9fb1-435f-9e94-5b91c3af7edd	true	introspection.token.claim
a1acba4f-9fb1-435f-9e94-5b91c3af7edd	true	userinfo.token.claim
a1acba4f-9fb1-435f-9e94-5b91c3af7edd	locale	user.attribute
a1acba4f-9fb1-435f-9e94-5b91c3af7edd	true	id.token.claim
a1acba4f-9fb1-435f-9e94-5b91c3af7edd	true	access.token.claim
a1acba4f-9fb1-435f-9e94-5b91c3af7edd	locale	claim.name
a1acba4f-9fb1-435f-9e94-5b91c3af7edd	String	jsonType.label
e1af2d33-8c8e-4b6f-989c-b92ae83b023a	false	single
e1af2d33-8c8e-4b6f-989c-b92ae83b023a	Basic	attribute.nameformat
e1af2d33-8c8e-4b6f-989c-b92ae83b023a	Role	attribute.name
051da8aa-37ab-40fe-9063-de016770e1f2	true	introspection.token.claim
051da8aa-37ab-40fe-9063-de016770e1f2	true	userinfo.token.claim
051da8aa-37ab-40fe-9063-de016770e1f2	middleName	user.attribute
051da8aa-37ab-40fe-9063-de016770e1f2	true	id.token.claim
051da8aa-37ab-40fe-9063-de016770e1f2	true	access.token.claim
051da8aa-37ab-40fe-9063-de016770e1f2	middle_name	claim.name
051da8aa-37ab-40fe-9063-de016770e1f2	String	jsonType.label
0b0a4dae-2771-4c0f-b8e1-21090285dcfe	true	introspection.token.claim
0b0a4dae-2771-4c0f-b8e1-21090285dcfe	true	userinfo.token.claim
0b0a4dae-2771-4c0f-b8e1-21090285dcfe	birthdate	user.attribute
0b0a4dae-2771-4c0f-b8e1-21090285dcfe	true	id.token.claim
0b0a4dae-2771-4c0f-b8e1-21090285dcfe	true	access.token.claim
0b0a4dae-2771-4c0f-b8e1-21090285dcfe	birthdate	claim.name
0b0a4dae-2771-4c0f-b8e1-21090285dcfe	String	jsonType.label
31cfa8b9-b315-46f5-b2d6-bf6e312317cc	true	introspection.token.claim
31cfa8b9-b315-46f5-b2d6-bf6e312317cc	true	userinfo.token.claim
31cfa8b9-b315-46f5-b2d6-bf6e312317cc	website	user.attribute
31cfa8b9-b315-46f5-b2d6-bf6e312317cc	true	id.token.claim
31cfa8b9-b315-46f5-b2d6-bf6e312317cc	true	access.token.claim
31cfa8b9-b315-46f5-b2d6-bf6e312317cc	website	claim.name
31cfa8b9-b315-46f5-b2d6-bf6e312317cc	String	jsonType.label
39488642-33be-4650-a6a0-91248d60f580	true	introspection.token.claim
39488642-33be-4650-a6a0-91248d60f580	true	userinfo.token.claim
39488642-33be-4650-a6a0-91248d60f580	profile	user.attribute
39488642-33be-4650-a6a0-91248d60f580	true	id.token.claim
39488642-33be-4650-a6a0-91248d60f580	true	access.token.claim
39488642-33be-4650-a6a0-91248d60f580	profile	claim.name
39488642-33be-4650-a6a0-91248d60f580	String	jsonType.label
39575146-e29f-4d49-ba98-e0660b54a0b5	true	introspection.token.claim
39575146-e29f-4d49-ba98-e0660b54a0b5	true	userinfo.token.claim
39575146-e29f-4d49-ba98-e0660b54a0b5	true	id.token.claim
39575146-e29f-4d49-ba98-e0660b54a0b5	true	access.token.claim
3c34854f-faa6-4d55-9f8a-f55873fadd72	true	introspection.token.claim
3c34854f-faa6-4d55-9f8a-f55873fadd72	true	userinfo.token.claim
3c34854f-faa6-4d55-9f8a-f55873fadd72	lastName	user.attribute
3c34854f-faa6-4d55-9f8a-f55873fadd72	true	id.token.claim
3c34854f-faa6-4d55-9f8a-f55873fadd72	true	access.token.claim
3c34854f-faa6-4d55-9f8a-f55873fadd72	family_name	claim.name
3c34854f-faa6-4d55-9f8a-f55873fadd72	String	jsonType.label
3f3c5c7d-2687-4c1c-9805-cf20f8e3ce6f	true	introspection.token.claim
3f3c5c7d-2687-4c1c-9805-cf20f8e3ce6f	true	userinfo.token.claim
3f3c5c7d-2687-4c1c-9805-cf20f8e3ce6f	zoneinfo	user.attribute
3f3c5c7d-2687-4c1c-9805-cf20f8e3ce6f	true	id.token.claim
3f3c5c7d-2687-4c1c-9805-cf20f8e3ce6f	true	access.token.claim
3f3c5c7d-2687-4c1c-9805-cf20f8e3ce6f	zoneinfo	claim.name
3f3c5c7d-2687-4c1c-9805-cf20f8e3ce6f	String	jsonType.label
3f900954-4c48-45cd-bae2-e74a3ec9f359	true	introspection.token.claim
3f900954-4c48-45cd-bae2-e74a3ec9f359	true	userinfo.token.claim
3f900954-4c48-45cd-bae2-e74a3ec9f359	nickname	user.attribute
3f900954-4c48-45cd-bae2-e74a3ec9f359	true	id.token.claim
3f900954-4c48-45cd-bae2-e74a3ec9f359	true	access.token.claim
3f900954-4c48-45cd-bae2-e74a3ec9f359	nickname	claim.name
3f900954-4c48-45cd-bae2-e74a3ec9f359	String	jsonType.label
4477938f-4b47-4d03-9dcb-1d3b5227df3f	true	introspection.token.claim
4477938f-4b47-4d03-9dcb-1d3b5227df3f	true	userinfo.token.claim
4477938f-4b47-4d03-9dcb-1d3b5227df3f	username	user.attribute
4477938f-4b47-4d03-9dcb-1d3b5227df3f	true	id.token.claim
4477938f-4b47-4d03-9dcb-1d3b5227df3f	true	access.token.claim
4477938f-4b47-4d03-9dcb-1d3b5227df3f	preferred_username	claim.name
4477938f-4b47-4d03-9dcb-1d3b5227df3f	String	jsonType.label
9f351f19-7d91-40c5-a10a-5dfe33a20a99	true	introspection.token.claim
9f351f19-7d91-40c5-a10a-5dfe33a20a99	true	userinfo.token.claim
9f351f19-7d91-40c5-a10a-5dfe33a20a99	gender	user.attribute
9f351f19-7d91-40c5-a10a-5dfe33a20a99	true	id.token.claim
9f351f19-7d91-40c5-a10a-5dfe33a20a99	true	access.token.claim
9f351f19-7d91-40c5-a10a-5dfe33a20a99	gender	claim.name
9f351f19-7d91-40c5-a10a-5dfe33a20a99	String	jsonType.label
a029aa90-6323-4825-a37f-cd336d4252e3	true	introspection.token.claim
a029aa90-6323-4825-a37f-cd336d4252e3	true	userinfo.token.claim
a029aa90-6323-4825-a37f-cd336d4252e3	firstName	user.attribute
a029aa90-6323-4825-a37f-cd336d4252e3	true	id.token.claim
a029aa90-6323-4825-a37f-cd336d4252e3	true	access.token.claim
a029aa90-6323-4825-a37f-cd336d4252e3	given_name	claim.name
a029aa90-6323-4825-a37f-cd336d4252e3	String	jsonType.label
a02fc4de-9b0d-495d-8719-bfb341cbb722	true	introspection.token.claim
a02fc4de-9b0d-495d-8719-bfb341cbb722	true	userinfo.token.claim
a02fc4de-9b0d-495d-8719-bfb341cbb722	picture	user.attribute
a02fc4de-9b0d-495d-8719-bfb341cbb722	true	id.token.claim
a02fc4de-9b0d-495d-8719-bfb341cbb722	true	access.token.claim
a02fc4de-9b0d-495d-8719-bfb341cbb722	picture	claim.name
a02fc4de-9b0d-495d-8719-bfb341cbb722	String	jsonType.label
c58fb3ed-9bb1-4b39-99f4-42cbed0f9f4d	true	introspection.token.claim
c58fb3ed-9bb1-4b39-99f4-42cbed0f9f4d	true	userinfo.token.claim
c58fb3ed-9bb1-4b39-99f4-42cbed0f9f4d	updatedAt	user.attribute
c58fb3ed-9bb1-4b39-99f4-42cbed0f9f4d	true	id.token.claim
c58fb3ed-9bb1-4b39-99f4-42cbed0f9f4d	true	access.token.claim
c58fb3ed-9bb1-4b39-99f4-42cbed0f9f4d	updated_at	claim.name
c58fb3ed-9bb1-4b39-99f4-42cbed0f9f4d	long	jsonType.label
f5fe6a3c-81a8-480e-9986-8d1d867de7b9	true	introspection.token.claim
f5fe6a3c-81a8-480e-9986-8d1d867de7b9	true	userinfo.token.claim
f5fe6a3c-81a8-480e-9986-8d1d867de7b9	locale	user.attribute
f5fe6a3c-81a8-480e-9986-8d1d867de7b9	true	id.token.claim
f5fe6a3c-81a8-480e-9986-8d1d867de7b9	true	access.token.claim
f5fe6a3c-81a8-480e-9986-8d1d867de7b9	locale	claim.name
f5fe6a3c-81a8-480e-9986-8d1d867de7b9	String	jsonType.label
8f3db9a9-5384-4680-be21-4fa111c08b90	true	introspection.token.claim
8f3db9a9-5384-4680-be21-4fa111c08b90	true	userinfo.token.claim
8f3db9a9-5384-4680-be21-4fa111c08b90	emailVerified	user.attribute
8f3db9a9-5384-4680-be21-4fa111c08b90	true	id.token.claim
8f3db9a9-5384-4680-be21-4fa111c08b90	true	access.token.claim
8f3db9a9-5384-4680-be21-4fa111c08b90	email_verified	claim.name
8f3db9a9-5384-4680-be21-4fa111c08b90	boolean	jsonType.label
c659f014-e11e-4a15-bc68-927a710d79c1	true	introspection.token.claim
c659f014-e11e-4a15-bc68-927a710d79c1	true	userinfo.token.claim
c659f014-e11e-4a15-bc68-927a710d79c1	email	user.attribute
c659f014-e11e-4a15-bc68-927a710d79c1	true	id.token.claim
c659f014-e11e-4a15-bc68-927a710d79c1	true	access.token.claim
c659f014-e11e-4a15-bc68-927a710d79c1	email	claim.name
c659f014-e11e-4a15-bc68-927a710d79c1	String	jsonType.label
9b84f95f-dd36-462c-b6de-bfe91586c8a2	formatted	user.attribute.formatted
9b84f95f-dd36-462c-b6de-bfe91586c8a2	country	user.attribute.country
9b84f95f-dd36-462c-b6de-bfe91586c8a2	true	introspection.token.claim
9b84f95f-dd36-462c-b6de-bfe91586c8a2	postal_code	user.attribute.postal_code
9b84f95f-dd36-462c-b6de-bfe91586c8a2	true	userinfo.token.claim
9b84f95f-dd36-462c-b6de-bfe91586c8a2	street	user.attribute.street
9b84f95f-dd36-462c-b6de-bfe91586c8a2	true	id.token.claim
9b84f95f-dd36-462c-b6de-bfe91586c8a2	region	user.attribute.region
9b84f95f-dd36-462c-b6de-bfe91586c8a2	true	access.token.claim
9b84f95f-dd36-462c-b6de-bfe91586c8a2	locality	user.attribute.locality
2bb2df5b-ae62-4752-b738-2ae254cc339c	true	introspection.token.claim
2bb2df5b-ae62-4752-b738-2ae254cc339c	true	userinfo.token.claim
2bb2df5b-ae62-4752-b738-2ae254cc339c	phoneNumberVerified	user.attribute
2bb2df5b-ae62-4752-b738-2ae254cc339c	true	id.token.claim
2bb2df5b-ae62-4752-b738-2ae254cc339c	true	access.token.claim
2bb2df5b-ae62-4752-b738-2ae254cc339c	phone_number_verified	claim.name
2bb2df5b-ae62-4752-b738-2ae254cc339c	boolean	jsonType.label
f032af6f-3d67-419c-9d17-9e94213805b7	true	introspection.token.claim
f032af6f-3d67-419c-9d17-9e94213805b7	true	userinfo.token.claim
f032af6f-3d67-419c-9d17-9e94213805b7	phoneNumber	user.attribute
f032af6f-3d67-419c-9d17-9e94213805b7	true	id.token.claim
f032af6f-3d67-419c-9d17-9e94213805b7	true	access.token.claim
f032af6f-3d67-419c-9d17-9e94213805b7	phone_number	claim.name
f032af6f-3d67-419c-9d17-9e94213805b7	String	jsonType.label
0273f296-2e30-4573-a1d9-61ce7630f189	true	introspection.token.claim
0273f296-2e30-4573-a1d9-61ce7630f189	true	multivalued
0273f296-2e30-4573-a1d9-61ce7630f189	foo	user.attribute
0273f296-2e30-4573-a1d9-61ce7630f189	true	access.token.claim
0273f296-2e30-4573-a1d9-61ce7630f189	realm_access.roles	claim.name
0273f296-2e30-4573-a1d9-61ce7630f189	String	jsonType.label
125fb530-b45f-479d-aa7e-708883a6bdb1	true	introspection.token.claim
125fb530-b45f-479d-aa7e-708883a6bdb1	true	multivalued
125fb530-b45f-479d-aa7e-708883a6bdb1	foo	user.attribute
125fb530-b45f-479d-aa7e-708883a6bdb1	true	access.token.claim
125fb530-b45f-479d-aa7e-708883a6bdb1	resource_access.${client_id}.roles	claim.name
125fb530-b45f-479d-aa7e-708883a6bdb1	String	jsonType.label
29562b16-3b74-4f85-bbea-053d671f99cd	true	introspection.token.claim
29562b16-3b74-4f85-bbea-053d671f99cd	true	access.token.claim
f6da39f4-7c8e-4363-8e57-7c950269fb67	true	introspection.token.claim
f6da39f4-7c8e-4363-8e57-7c950269fb67	true	access.token.claim
9b78d02d-de41-4e21-80ed-a94f1e95e347	true	introspection.token.claim
9b78d02d-de41-4e21-80ed-a94f1e95e347	true	userinfo.token.claim
9b78d02d-de41-4e21-80ed-a94f1e95e347	username	user.attribute
9b78d02d-de41-4e21-80ed-a94f1e95e347	true	id.token.claim
9b78d02d-de41-4e21-80ed-a94f1e95e347	true	access.token.claim
9b78d02d-de41-4e21-80ed-a94f1e95e347	upn	claim.name
9b78d02d-de41-4e21-80ed-a94f1e95e347	String	jsonType.label
b6bdb781-2582-4d37-bfcb-c98853e2a575	true	introspection.token.claim
b6bdb781-2582-4d37-bfcb-c98853e2a575	true	multivalued
b6bdb781-2582-4d37-bfcb-c98853e2a575	foo	user.attribute
b6bdb781-2582-4d37-bfcb-c98853e2a575	true	id.token.claim
b6bdb781-2582-4d37-bfcb-c98853e2a575	true	access.token.claim
b6bdb781-2582-4d37-bfcb-c98853e2a575	groups	claim.name
b6bdb781-2582-4d37-bfcb-c98853e2a575	String	jsonType.label
9c785a92-3e4a-45e0-b4ae-38ddce1e39dc	true	introspection.token.claim
9c785a92-3e4a-45e0-b4ae-38ddce1e39dc	true	id.token.claim
9c785a92-3e4a-45e0-b4ae-38ddce1e39dc	true	access.token.claim
d597233c-b09b-4466-939d-a27cb7e8c442	true	introspection.token.claim
d597233c-b09b-4466-939d-a27cb7e8c442	true	access.token.claim
fd80aaa4-847c-4eab-94c5-1b0a189d730e	AUTH_TIME	user.session.note
fd80aaa4-847c-4eab-94c5-1b0a189d730e	true	introspection.token.claim
fd80aaa4-847c-4eab-94c5-1b0a189d730e	true	id.token.claim
fd80aaa4-847c-4eab-94c5-1b0a189d730e	true	access.token.claim
fd80aaa4-847c-4eab-94c5-1b0a189d730e	auth_time	claim.name
fd80aaa4-847c-4eab-94c5-1b0a189d730e	long	jsonType.label
3375d80b-bf74-4aa0-ab99-51c20b9915ee	client_id	user.session.note
3375d80b-bf74-4aa0-ab99-51c20b9915ee	true	introspection.token.claim
3375d80b-bf74-4aa0-ab99-51c20b9915ee	true	id.token.claim
3375d80b-bf74-4aa0-ab99-51c20b9915ee	true	access.token.claim
3375d80b-bf74-4aa0-ab99-51c20b9915ee	client_id	claim.name
3375d80b-bf74-4aa0-ab99-51c20b9915ee	String	jsonType.label
6ac76831-679e-4c1d-af72-5712fa3092db	clientAddress	user.session.note
6ac76831-679e-4c1d-af72-5712fa3092db	true	introspection.token.claim
6ac76831-679e-4c1d-af72-5712fa3092db	true	id.token.claim
6ac76831-679e-4c1d-af72-5712fa3092db	true	access.token.claim
6ac76831-679e-4c1d-af72-5712fa3092db	clientAddress	claim.name
6ac76831-679e-4c1d-af72-5712fa3092db	String	jsonType.label
b937c8d8-7ac6-48af-a91f-284cab27fa97	clientHost	user.session.note
b937c8d8-7ac6-48af-a91f-284cab27fa97	true	introspection.token.claim
b937c8d8-7ac6-48af-a91f-284cab27fa97	true	id.token.claim
b937c8d8-7ac6-48af-a91f-284cab27fa97	true	access.token.claim
b937c8d8-7ac6-48af-a91f-284cab27fa97	clientHost	claim.name
b937c8d8-7ac6-48af-a91f-284cab27fa97	String	jsonType.label
84dd823a-50f4-4994-9768-40c54d84213f	true	introspection.token.claim
84dd823a-50f4-4994-9768-40c54d84213f	true	multivalued
84dd823a-50f4-4994-9768-40c54d84213f	true	id.token.claim
84dd823a-50f4-4994-9768-40c54d84213f	true	access.token.claim
84dd823a-50f4-4994-9768-40c54d84213f	organization	claim.name
84dd823a-50f4-4994-9768-40c54d84213f	String	jsonType.label
253bbc57-e291-4be0-ba86-a6749782470d	false	single
253bbc57-e291-4be0-ba86-a6749782470d	Basic	attribute.nameformat
253bbc57-e291-4be0-ba86-a6749782470d	Role	attribute.name
4554ed41-0c61-4eff-b495-5c83151f708c	true	introspection.token.claim
4554ed41-0c61-4eff-b495-5c83151f708c	true	userinfo.token.claim
4554ed41-0c61-4eff-b495-5c83151f708c	username	user.attribute
4554ed41-0c61-4eff-b495-5c83151f708c	true	id.token.claim
4554ed41-0c61-4eff-b495-5c83151f708c	true	access.token.claim
4554ed41-0c61-4eff-b495-5c83151f708c	preferred_username	claim.name
4554ed41-0c61-4eff-b495-5c83151f708c	String	jsonType.label
4e95e4a5-00fe-453f-99fa-d163da3c6803	true	introspection.token.claim
4e95e4a5-00fe-453f-99fa-d163da3c6803	true	userinfo.token.claim
4e95e4a5-00fe-453f-99fa-d163da3c6803	firstName	user.attribute
4e95e4a5-00fe-453f-99fa-d163da3c6803	true	id.token.claim
4e95e4a5-00fe-453f-99fa-d163da3c6803	true	access.token.claim
4e95e4a5-00fe-453f-99fa-d163da3c6803	given_name	claim.name
4e95e4a5-00fe-453f-99fa-d163da3c6803	String	jsonType.label
4f57c857-3d72-4793-b5f7-433a6c8e410c	true	introspection.token.claim
4f57c857-3d72-4793-b5f7-433a6c8e410c	true	userinfo.token.claim
4f57c857-3d72-4793-b5f7-433a6c8e410c	locale	user.attribute
4f57c857-3d72-4793-b5f7-433a6c8e410c	true	id.token.claim
4f57c857-3d72-4793-b5f7-433a6c8e410c	true	access.token.claim
4f57c857-3d72-4793-b5f7-433a6c8e410c	locale	claim.name
4f57c857-3d72-4793-b5f7-433a6c8e410c	String	jsonType.label
59734a17-c925-4276-a35c-cd926e5b4c91	true	introspection.token.claim
59734a17-c925-4276-a35c-cd926e5b4c91	true	userinfo.token.claim
59734a17-c925-4276-a35c-cd926e5b4c91	picture	user.attribute
59734a17-c925-4276-a35c-cd926e5b4c91	true	id.token.claim
59734a17-c925-4276-a35c-cd926e5b4c91	true	access.token.claim
59734a17-c925-4276-a35c-cd926e5b4c91	picture	claim.name
59734a17-c925-4276-a35c-cd926e5b4c91	String	jsonType.label
6e29b0c8-d767-430b-8007-33c713c60fe7	true	introspection.token.claim
6e29b0c8-d767-430b-8007-33c713c60fe7	true	userinfo.token.claim
6e29b0c8-d767-430b-8007-33c713c60fe7	gender	user.attribute
6e29b0c8-d767-430b-8007-33c713c60fe7	true	id.token.claim
6e29b0c8-d767-430b-8007-33c713c60fe7	true	access.token.claim
6e29b0c8-d767-430b-8007-33c713c60fe7	gender	claim.name
6e29b0c8-d767-430b-8007-33c713c60fe7	String	jsonType.label
757533ab-b763-42bd-a814-0ff784e5f156	true	introspection.token.claim
757533ab-b763-42bd-a814-0ff784e5f156	true	userinfo.token.claim
757533ab-b763-42bd-a814-0ff784e5f156	profile	user.attribute
757533ab-b763-42bd-a814-0ff784e5f156	true	id.token.claim
757533ab-b763-42bd-a814-0ff784e5f156	true	access.token.claim
757533ab-b763-42bd-a814-0ff784e5f156	profile	claim.name
757533ab-b763-42bd-a814-0ff784e5f156	String	jsonType.label
9a1c4230-a2e5-4a5e-85f3-04bf8d41fce0	true	introspection.token.claim
9a1c4230-a2e5-4a5e-85f3-04bf8d41fce0	true	userinfo.token.claim
9a1c4230-a2e5-4a5e-85f3-04bf8d41fce0	website	user.attribute
9a1c4230-a2e5-4a5e-85f3-04bf8d41fce0	true	id.token.claim
9a1c4230-a2e5-4a5e-85f3-04bf8d41fce0	true	access.token.claim
9a1c4230-a2e5-4a5e-85f3-04bf8d41fce0	website	claim.name
9a1c4230-a2e5-4a5e-85f3-04bf8d41fce0	String	jsonType.label
9c9142b8-7b1d-4f12-9143-0732cd598733	true	introspection.token.claim
9c9142b8-7b1d-4f12-9143-0732cd598733	true	userinfo.token.claim
9c9142b8-7b1d-4f12-9143-0732cd598733	nickname	user.attribute
9c9142b8-7b1d-4f12-9143-0732cd598733	true	id.token.claim
9c9142b8-7b1d-4f12-9143-0732cd598733	true	access.token.claim
9c9142b8-7b1d-4f12-9143-0732cd598733	nickname	claim.name
9c9142b8-7b1d-4f12-9143-0732cd598733	String	jsonType.label
a9355b10-c7fc-4a3d-98d3-bea1b0feeafd	true	introspection.token.claim
a9355b10-c7fc-4a3d-98d3-bea1b0feeafd	true	userinfo.token.claim
a9355b10-c7fc-4a3d-98d3-bea1b0feeafd	lastName	user.attribute
a9355b10-c7fc-4a3d-98d3-bea1b0feeafd	true	id.token.claim
a9355b10-c7fc-4a3d-98d3-bea1b0feeafd	true	access.token.claim
a9355b10-c7fc-4a3d-98d3-bea1b0feeafd	family_name	claim.name
a9355b10-c7fc-4a3d-98d3-bea1b0feeafd	String	jsonType.label
b1f4725e-bc15-4b0a-8672-3b53a75e353b	true	introspection.token.claim
b1f4725e-bc15-4b0a-8672-3b53a75e353b	true	userinfo.token.claim
b1f4725e-bc15-4b0a-8672-3b53a75e353b	middleName	user.attribute
b1f4725e-bc15-4b0a-8672-3b53a75e353b	true	id.token.claim
b1f4725e-bc15-4b0a-8672-3b53a75e353b	true	access.token.claim
b1f4725e-bc15-4b0a-8672-3b53a75e353b	middle_name	claim.name
b1f4725e-bc15-4b0a-8672-3b53a75e353b	String	jsonType.label
b27286ab-5a72-487b-a801-3d5efaca23a2	true	introspection.token.claim
b27286ab-5a72-487b-a801-3d5efaca23a2	true	userinfo.token.claim
b27286ab-5a72-487b-a801-3d5efaca23a2	true	id.token.claim
b27286ab-5a72-487b-a801-3d5efaca23a2	true	access.token.claim
c4256356-7492-45d8-bf44-15d7ba40b11b	true	introspection.token.claim
c4256356-7492-45d8-bf44-15d7ba40b11b	true	userinfo.token.claim
c4256356-7492-45d8-bf44-15d7ba40b11b	birthdate	user.attribute
c4256356-7492-45d8-bf44-15d7ba40b11b	true	id.token.claim
c4256356-7492-45d8-bf44-15d7ba40b11b	true	access.token.claim
c4256356-7492-45d8-bf44-15d7ba40b11b	birthdate	claim.name
c4256356-7492-45d8-bf44-15d7ba40b11b	String	jsonType.label
e9496210-64bb-488f-b9ca-4a2d221b77db	true	introspection.token.claim
e9496210-64bb-488f-b9ca-4a2d221b77db	true	userinfo.token.claim
e9496210-64bb-488f-b9ca-4a2d221b77db	updatedAt	user.attribute
e9496210-64bb-488f-b9ca-4a2d221b77db	true	id.token.claim
e9496210-64bb-488f-b9ca-4a2d221b77db	true	access.token.claim
e9496210-64bb-488f-b9ca-4a2d221b77db	updated_at	claim.name
e9496210-64bb-488f-b9ca-4a2d221b77db	long	jsonType.label
f1f511d0-d915-4f7b-b551-f61411c0f177	true	introspection.token.claim
f1f511d0-d915-4f7b-b551-f61411c0f177	true	userinfo.token.claim
f1f511d0-d915-4f7b-b551-f61411c0f177	zoneinfo	user.attribute
f1f511d0-d915-4f7b-b551-f61411c0f177	true	id.token.claim
f1f511d0-d915-4f7b-b551-f61411c0f177	true	access.token.claim
f1f511d0-d915-4f7b-b551-f61411c0f177	zoneinfo	claim.name
f1f511d0-d915-4f7b-b551-f61411c0f177	String	jsonType.label
50e48917-494f-414c-a3ff-5c5073b34fbf	true	introspection.token.claim
50e48917-494f-414c-a3ff-5c5073b34fbf	true	userinfo.token.claim
50e48917-494f-414c-a3ff-5c5073b34fbf	emailVerified	user.attribute
50e48917-494f-414c-a3ff-5c5073b34fbf	true	id.token.claim
50e48917-494f-414c-a3ff-5c5073b34fbf	true	access.token.claim
50e48917-494f-414c-a3ff-5c5073b34fbf	email_verified	claim.name
50e48917-494f-414c-a3ff-5c5073b34fbf	boolean	jsonType.label
bb959414-fc03-4d13-ba9d-88cd2825c34b	true	introspection.token.claim
bb959414-fc03-4d13-ba9d-88cd2825c34b	true	userinfo.token.claim
bb959414-fc03-4d13-ba9d-88cd2825c34b	email	user.attribute
bb959414-fc03-4d13-ba9d-88cd2825c34b	true	id.token.claim
bb959414-fc03-4d13-ba9d-88cd2825c34b	true	access.token.claim
bb959414-fc03-4d13-ba9d-88cd2825c34b	email	claim.name
bb959414-fc03-4d13-ba9d-88cd2825c34b	String	jsonType.label
c295b695-048b-4439-a6e8-6efbf8cfb179	formatted	user.attribute.formatted
c295b695-048b-4439-a6e8-6efbf8cfb179	country	user.attribute.country
c295b695-048b-4439-a6e8-6efbf8cfb179	true	introspection.token.claim
c295b695-048b-4439-a6e8-6efbf8cfb179	postal_code	user.attribute.postal_code
c295b695-048b-4439-a6e8-6efbf8cfb179	true	userinfo.token.claim
c295b695-048b-4439-a6e8-6efbf8cfb179	street	user.attribute.street
c295b695-048b-4439-a6e8-6efbf8cfb179	true	id.token.claim
c295b695-048b-4439-a6e8-6efbf8cfb179	region	user.attribute.region
c295b695-048b-4439-a6e8-6efbf8cfb179	true	access.token.claim
c295b695-048b-4439-a6e8-6efbf8cfb179	locality	user.attribute.locality
9c1c21ad-da5f-42ba-a502-2335eaf17ddd	true	introspection.token.claim
9c1c21ad-da5f-42ba-a502-2335eaf17ddd	true	userinfo.token.claim
9c1c21ad-da5f-42ba-a502-2335eaf17ddd	phoneNumberVerified	user.attribute
9c1c21ad-da5f-42ba-a502-2335eaf17ddd	true	id.token.claim
9c1c21ad-da5f-42ba-a502-2335eaf17ddd	true	access.token.claim
9c1c21ad-da5f-42ba-a502-2335eaf17ddd	phone_number_verified	claim.name
9c1c21ad-da5f-42ba-a502-2335eaf17ddd	boolean	jsonType.label
c5ab88f8-b582-44c8-83bf-a0dd0d56924e	true	introspection.token.claim
c5ab88f8-b582-44c8-83bf-a0dd0d56924e	true	userinfo.token.claim
c5ab88f8-b582-44c8-83bf-a0dd0d56924e	phoneNumber	user.attribute
c5ab88f8-b582-44c8-83bf-a0dd0d56924e	true	id.token.claim
c5ab88f8-b582-44c8-83bf-a0dd0d56924e	true	access.token.claim
c5ab88f8-b582-44c8-83bf-a0dd0d56924e	phone_number	claim.name
c5ab88f8-b582-44c8-83bf-a0dd0d56924e	String	jsonType.label
02e36241-4d69-4fd2-94fa-2b167694041b	true	introspection.token.claim
02e36241-4d69-4fd2-94fa-2b167694041b	true	access.token.claim
366c745c-a780-4e72-9f45-ff1b08900e87	true	introspection.token.claim
366c745c-a780-4e72-9f45-ff1b08900e87	true	multivalued
366c745c-a780-4e72-9f45-ff1b08900e87	foo	user.attribute
366c745c-a780-4e72-9f45-ff1b08900e87	true	access.token.claim
366c745c-a780-4e72-9f45-ff1b08900e87	realm_access.roles	claim.name
366c745c-a780-4e72-9f45-ff1b08900e87	String	jsonType.label
8977adb4-c253-4baa-a906-b3bac35caa6b	true	introspection.token.claim
8977adb4-c253-4baa-a906-b3bac35caa6b	true	multivalued
8977adb4-c253-4baa-a906-b3bac35caa6b	foo	user.attribute
8977adb4-c253-4baa-a906-b3bac35caa6b	true	access.token.claim
8977adb4-c253-4baa-a906-b3bac35caa6b	resource_access.${client_id}.roles	claim.name
8977adb4-c253-4baa-a906-b3bac35caa6b	String	jsonType.label
25d4db89-e2e2-4564-8695-b7286c621501	true	introspection.token.claim
25d4db89-e2e2-4564-8695-b7286c621501	true	access.token.claim
026458da-c162-4193-8e5c-ebbdbaf04fb1	true	introspection.token.claim
026458da-c162-4193-8e5c-ebbdbaf04fb1	true	multivalued
026458da-c162-4193-8e5c-ebbdbaf04fb1	foo	user.attribute
026458da-c162-4193-8e5c-ebbdbaf04fb1	true	id.token.claim
026458da-c162-4193-8e5c-ebbdbaf04fb1	true	access.token.claim
026458da-c162-4193-8e5c-ebbdbaf04fb1	groups	claim.name
026458da-c162-4193-8e5c-ebbdbaf04fb1	String	jsonType.label
780d9991-71e6-4327-b315-de6328b15416	true	introspection.token.claim
780d9991-71e6-4327-b315-de6328b15416	true	userinfo.token.claim
780d9991-71e6-4327-b315-de6328b15416	username	user.attribute
780d9991-71e6-4327-b315-de6328b15416	true	id.token.claim
780d9991-71e6-4327-b315-de6328b15416	true	access.token.claim
780d9991-71e6-4327-b315-de6328b15416	upn	claim.name
780d9991-71e6-4327-b315-de6328b15416	String	jsonType.label
4aa8363c-ce8b-40ca-9c9a-c1782583475f	true	introspection.token.claim
4aa8363c-ce8b-40ca-9c9a-c1782583475f	true	id.token.claim
4aa8363c-ce8b-40ca-9c9a-c1782583475f	true	access.token.claim
26a2f556-4268-4982-9dac-63c7a850f469	AUTH_TIME	user.session.note
26a2f556-4268-4982-9dac-63c7a850f469	true	introspection.token.claim
26a2f556-4268-4982-9dac-63c7a850f469	true	id.token.claim
26a2f556-4268-4982-9dac-63c7a850f469	true	access.token.claim
26a2f556-4268-4982-9dac-63c7a850f469	auth_time	claim.name
26a2f556-4268-4982-9dac-63c7a850f469	long	jsonType.label
b2a4a419-c721-4e3a-ad52-3b7d38a33c74	true	introspection.token.claim
b2a4a419-c721-4e3a-ad52-3b7d38a33c74	true	access.token.claim
26c8804c-c690-4cc6-ab14-6107d5be2c53	client_id	user.session.note
26c8804c-c690-4cc6-ab14-6107d5be2c53	true	introspection.token.claim
26c8804c-c690-4cc6-ab14-6107d5be2c53	true	id.token.claim
26c8804c-c690-4cc6-ab14-6107d5be2c53	true	access.token.claim
26c8804c-c690-4cc6-ab14-6107d5be2c53	client_id	claim.name
26c8804c-c690-4cc6-ab14-6107d5be2c53	String	jsonType.label
42f3760c-f8b4-4213-a619-29326c654b21	clientAddress	user.session.note
42f3760c-f8b4-4213-a619-29326c654b21	true	introspection.token.claim
42f3760c-f8b4-4213-a619-29326c654b21	true	id.token.claim
42f3760c-f8b4-4213-a619-29326c654b21	true	access.token.claim
42f3760c-f8b4-4213-a619-29326c654b21	clientAddress	claim.name
42f3760c-f8b4-4213-a619-29326c654b21	String	jsonType.label
c0a03313-788a-4e8e-916e-3f3c3ba0c491	clientHost	user.session.note
c0a03313-788a-4e8e-916e-3f3c3ba0c491	true	introspection.token.claim
c0a03313-788a-4e8e-916e-3f3c3ba0c491	true	id.token.claim
c0a03313-788a-4e8e-916e-3f3c3ba0c491	true	access.token.claim
c0a03313-788a-4e8e-916e-3f3c3ba0c491	clientHost	claim.name
c0a03313-788a-4e8e-916e-3f3c3ba0c491	String	jsonType.label
52aea26e-d214-49c5-a960-98839507fa60	true	introspection.token.claim
52aea26e-d214-49c5-a960-98839507fa60	true	multivalued
52aea26e-d214-49c5-a960-98839507fa60	true	id.token.claim
52aea26e-d214-49c5-a960-98839507fa60	true	access.token.claim
52aea26e-d214-49c5-a960-98839507fa60	organization	claim.name
52aea26e-d214-49c5-a960-98839507fa60	String	jsonType.label
91c2cfc0-0f21-41d0-9c2e-f610a01df15d	true	introspection.token.claim
91c2cfc0-0f21-41d0-9c2e-f610a01df15d	true	userinfo.token.claim
91c2cfc0-0f21-41d0-9c2e-f610a01df15d	locale	user.attribute
91c2cfc0-0f21-41d0-9c2e-f610a01df15d	true	id.token.claim
91c2cfc0-0f21-41d0-9c2e-f610a01df15d	true	access.token.claim
91c2cfc0-0f21-41d0-9c2e-f610a01df15d	locale	claim.name
91c2cfc0-0f21-41d0-9c2e-f610a01df15d	String	jsonType.label
4706c8f8-6942-4a44-b6dc-e33a76a3ebd3	true	introspection.token.claim
4706c8f8-6942-4a44-b6dc-e33a76a3ebd3	true	userinfo.token.claim
4706c8f8-6942-4a44-b6dc-e33a76a3ebd3	tenantId	user.attribute
4706c8f8-6942-4a44-b6dc-e33a76a3ebd3	true	id.token.claim
4706c8f8-6942-4a44-b6dc-e33a76a3ebd3	false	lightweight.claim
4706c8f8-6942-4a44-b6dc-e33a76a3ebd3	true	access.token.claim
4706c8f8-6942-4a44-b6dc-e33a76a3ebd3	tenant_id	claim.name
4706c8f8-6942-4a44-b6dc-e33a76a3ebd3	String	jsonType.label
\.


--
-- TOC entry 4160 (class 0 OID 16440)
-- Dependencies: 224
-- Data for Name: realm; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.realm (id, access_code_lifespan, user_action_lifespan, access_token_lifespan, account_theme, admin_theme, email_theme, enabled, events_enabled, events_expiration, login_theme, name, not_before, password_policy, registration_allowed, remember_me, reset_password_allowed, social, ssl_required, sso_idle_timeout, sso_max_lifespan, update_profile_on_soc_login, verify_email, master_admin_client, login_lifespan, internationalization_enabled, default_locale, reg_email_as_username, admin_events_enabled, admin_events_details_enabled, edit_username_allowed, otp_policy_counter, otp_policy_window, otp_policy_period, otp_policy_digits, otp_policy_alg, otp_policy_type, browser_flow, registration_flow, direct_grant_flow, reset_credentials_flow, client_auth_flow, offline_session_idle_timeout, revoke_refresh_token, access_token_life_implicit, login_with_email_allowed, duplicate_emails_allowed, docker_auth_flow, refresh_token_max_reuse, allow_user_managed_access, sso_max_lifespan_remember_me, sso_idle_timeout_remember_me, default_role) FROM stdin;
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	60	300	60	\N	\N	\N	t	f	0	\N	master	0	\N	f	f	f	f	EXTERNAL	1800	36000	f	f	e2949c03-f4a8-41d4-b6aa-2a9a13f1f8cb	1800	f	\N	f	f	f	f	0	1	30	6	HmacSHA1	totp	dda11955-b6a0-49b6-b6f9-23f996dedcb3	2c452b8d-00d1-4938-afc5-26322ecdd8af	56510d42-ae29-4292-a6cd-885d225b1d6b	575ec34d-c697-4594-ac2c-74141f7c1c17	7a10855f-ee13-4278-9b63-02382a570b8c	2592000	f	900	t	f	56295342-9379-42c8-bd0f-d8b1733918c2	0	f	0	0	24925405-e2df-4f44-bf4d-d5fa744940eb
1bcb83bb-6f51-410d-972e-8077c4359429	60	300	300	\N	\N	\N	t	f	0	\N	relma	0	\N	f	f	f	f	EXTERNAL	1800	36000	f	f	cccb0d38-c1a6-4baa-86f3-cb8f5f97c6b3	1800	f	\N	f	f	f	f	0	1	30	6	HmacSHA1	totp	d5e7e1fa-6bc5-41d5-8c2c-15d4e50a4104	bbe75f57-eee1-413e-9f97-e57f2f9eaea8	77386210-c959-4196-bab4-04f2fc58e97b	64cd3b6e-e1d3-48b8-8411-22d6a072dee0	60674ea8-99c3-4ecb-9bf3-3c02d4ebc6b2	2592000	f	900	t	f	6854c68c-d750-4169-9f81-ed80da1dcc92	0	f	0	0	76f8cb98-3d7b-47e6-abed-d3ebcd7203bc
\.


--
-- TOC entry 4161 (class 0 OID 16457)
-- Dependencies: 225
-- Data for Name: realm_attribute; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.realm_attribute (name, realm_id, value) FROM stdin;
_browser_header.contentSecurityPolicyReportOnly	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	
_browser_header.xContentTypeOptions	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	nosniff
_browser_header.referrerPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	no-referrer
_browser_header.xRobotsTag	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	none
_browser_header.xFrameOptions	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	SAMEORIGIN
_browser_header.contentSecurityPolicy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	frame-src 'self'; frame-ancestors 'self'; object-src 'none';
_browser_header.strictTransportSecurity	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	max-age=31536000; includeSubDomains
bruteForceProtected	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	false
permanentLockout	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	false
maxTemporaryLockouts	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	0
bruteForceStrategy	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	MULTIPLE
maxFailureWaitSeconds	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	900
minimumQuickLoginWaitSeconds	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	60
waitIncrementSeconds	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	60
quickLoginCheckMilliSeconds	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	1000
maxDeltaTimeSeconds	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	43200
failureFactor	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	30
realmReusableOtpCode	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	false
firstBrokerLoginFlowId	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f3a74744-0746-4945-8a7b-7205957d8bf0
displayName	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	Keycloak
displayNameHtml	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	<div class="kc-logo-text"><span>Keycloak</span></div>
defaultSignatureAlgorithm	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	RS256
offlineSessionMaxLifespanEnabled	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	false
offlineSessionMaxLifespan	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	5184000
_browser_header.contentSecurityPolicyReportOnly	1bcb83bb-6f51-410d-972e-8077c4359429	
_browser_header.xContentTypeOptions	1bcb83bb-6f51-410d-972e-8077c4359429	nosniff
_browser_header.referrerPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	no-referrer
_browser_header.xRobotsTag	1bcb83bb-6f51-410d-972e-8077c4359429	none
_browser_header.xFrameOptions	1bcb83bb-6f51-410d-972e-8077c4359429	SAMEORIGIN
_browser_header.contentSecurityPolicy	1bcb83bb-6f51-410d-972e-8077c4359429	frame-src 'self'; frame-ancestors 'self'; object-src 'none';
_browser_header.strictTransportSecurity	1bcb83bb-6f51-410d-972e-8077c4359429	max-age=31536000; includeSubDomains
bruteForceProtected	1bcb83bb-6f51-410d-972e-8077c4359429	false
permanentLockout	1bcb83bb-6f51-410d-972e-8077c4359429	false
maxTemporaryLockouts	1bcb83bb-6f51-410d-972e-8077c4359429	0
bruteForceStrategy	1bcb83bb-6f51-410d-972e-8077c4359429	MULTIPLE
maxFailureWaitSeconds	1bcb83bb-6f51-410d-972e-8077c4359429	900
minimumQuickLoginWaitSeconds	1bcb83bb-6f51-410d-972e-8077c4359429	60
waitIncrementSeconds	1bcb83bb-6f51-410d-972e-8077c4359429	60
quickLoginCheckMilliSeconds	1bcb83bb-6f51-410d-972e-8077c4359429	1000
maxDeltaTimeSeconds	1bcb83bb-6f51-410d-972e-8077c4359429	43200
failureFactor	1bcb83bb-6f51-410d-972e-8077c4359429	30
realmReusableOtpCode	1bcb83bb-6f51-410d-972e-8077c4359429	false
defaultSignatureAlgorithm	1bcb83bb-6f51-410d-972e-8077c4359429	RS256
offlineSessionMaxLifespanEnabled	1bcb83bb-6f51-410d-972e-8077c4359429	false
offlineSessionMaxLifespan	1bcb83bb-6f51-410d-972e-8077c4359429	5184000
actionTokenGeneratedByAdminLifespan	1bcb83bb-6f51-410d-972e-8077c4359429	43200
actionTokenGeneratedByUserLifespan	1bcb83bb-6f51-410d-972e-8077c4359429	300
oauth2DeviceCodeLifespan	1bcb83bb-6f51-410d-972e-8077c4359429	600
oauth2DevicePollingInterval	1bcb83bb-6f51-410d-972e-8077c4359429	5
webAuthnPolicyRpEntityName	1bcb83bb-6f51-410d-972e-8077c4359429	keycloak
webAuthnPolicySignatureAlgorithms	1bcb83bb-6f51-410d-972e-8077c4359429	ES256,RS256
webAuthnPolicyRpId	1bcb83bb-6f51-410d-972e-8077c4359429	
webAuthnPolicyAttestationConveyancePreference	1bcb83bb-6f51-410d-972e-8077c4359429	not specified
webAuthnPolicyAuthenticatorAttachment	1bcb83bb-6f51-410d-972e-8077c4359429	not specified
webAuthnPolicyRequireResidentKey	1bcb83bb-6f51-410d-972e-8077c4359429	not specified
webAuthnPolicyUserVerificationRequirement	1bcb83bb-6f51-410d-972e-8077c4359429	not specified
webAuthnPolicyCreateTimeout	1bcb83bb-6f51-410d-972e-8077c4359429	0
webAuthnPolicyAvoidSameAuthenticatorRegister	1bcb83bb-6f51-410d-972e-8077c4359429	false
webAuthnPolicyRpEntityNamePasswordless	1bcb83bb-6f51-410d-972e-8077c4359429	keycloak
webAuthnPolicySignatureAlgorithmsPasswordless	1bcb83bb-6f51-410d-972e-8077c4359429	ES256,RS256
webAuthnPolicyRpIdPasswordless	1bcb83bb-6f51-410d-972e-8077c4359429	
webAuthnPolicyAttestationConveyancePreferencePasswordless	1bcb83bb-6f51-410d-972e-8077c4359429	not specified
webAuthnPolicyAuthenticatorAttachmentPasswordless	1bcb83bb-6f51-410d-972e-8077c4359429	not specified
webAuthnPolicyRequireResidentKeyPasswordless	1bcb83bb-6f51-410d-972e-8077c4359429	not specified
webAuthnPolicyUserVerificationRequirementPasswordless	1bcb83bb-6f51-410d-972e-8077c4359429	not specified
webAuthnPolicyCreateTimeoutPasswordless	1bcb83bb-6f51-410d-972e-8077c4359429	0
webAuthnPolicyAvoidSameAuthenticatorRegisterPasswordless	1bcb83bb-6f51-410d-972e-8077c4359429	false
cibaBackchannelTokenDeliveryMode	1bcb83bb-6f51-410d-972e-8077c4359429	poll
cibaExpiresIn	1bcb83bb-6f51-410d-972e-8077c4359429	120
cibaInterval	1bcb83bb-6f51-410d-972e-8077c4359429	5
cibaAuthRequestedUserHint	1bcb83bb-6f51-410d-972e-8077c4359429	login_hint
parRequestUriLifespan	1bcb83bb-6f51-410d-972e-8077c4359429	60
firstBrokerLoginFlowId	1bcb83bb-6f51-410d-972e-8077c4359429	d79b5d89-c58c-4fbe-bd2b-a81b5ae758a9
\.


--
-- TOC entry 4202 (class 0 OID 17214)
-- Dependencies: 266
-- Data for Name: realm_default_groups; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.realm_default_groups (realm_id, group_id) FROM stdin;
\.


--
-- TOC entry 4182 (class 0 OID 16910)
-- Dependencies: 246
-- Data for Name: realm_enabled_event_types; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.realm_enabled_event_types (realm_id, value) FROM stdin;
\.


--
-- TOC entry 4162 (class 0 OID 16465)
-- Dependencies: 226
-- Data for Name: realm_events_listeners; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.realm_events_listeners (realm_id, value) FROM stdin;
9c60f1e0-3006-44bf-be9a-e9110bd2df7a	jboss-logging
1bcb83bb-6f51-410d-972e-8077c4359429	jboss-logging
\.


--
-- TOC entry 4235 (class 0 OID 17916)
-- Dependencies: 299
-- Data for Name: realm_localizations; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.realm_localizations (realm_id, locale, texts) FROM stdin;
\.


--
-- TOC entry 4163 (class 0 OID 16468)
-- Dependencies: 227
-- Data for Name: realm_required_credential; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.realm_required_credential (type, form_label, input, secret, realm_id) FROM stdin;
password	password	t	t	9c60f1e0-3006-44bf-be9a-e9110bd2df7a
password	password	t	t	1bcb83bb-6f51-410d-972e-8077c4359429
\.


--
-- TOC entry 4164 (class 0 OID 16475)
-- Dependencies: 228
-- Data for Name: realm_smtp_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.realm_smtp_config (realm_id, value, name) FROM stdin;
\.


--
-- TOC entry 4181 (class 0 OID 16826)
-- Dependencies: 245
-- Data for Name: realm_supported_locales; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.realm_supported_locales (realm_id, value) FROM stdin;
\.


--
-- TOC entry 4165 (class 0 OID 16485)
-- Dependencies: 229
-- Data for Name: redirect_uris; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.redirect_uris (client_id, value) FROM stdin;
955b4b55-16f3-472b-a919-6e4969b3feb0	/realms/master/account/*
31102431-0756-40bd-bffb-0757ab96be0d	/realms/master/account/*
07df0e9c-0213-48a6-b89f-658593bde218	/admin/master/console/*
a647b04c-25f4-4103-b180-ab357a7e456a	/realms/relma/account/*
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	/realms/relma/account/*
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	/admin/relma/console/*
3f25824c-6d29-4079-a63b-f39091380fb6	*
4a3129e2-ab8f-4d02-b98d-aa9f82145149	*
\.


--
-- TOC entry 4195 (class 0 OID 17149)
-- Dependencies: 259
-- Data for Name: required_action_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.required_action_config (required_action_id, value, name) FROM stdin;
\.


--
-- TOC entry 4194 (class 0 OID 17142)
-- Dependencies: 258
-- Data for Name: required_action_provider; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.required_action_provider (id, alias, name, realm_id, enabled, default_action, provider_id, priority) FROM stdin;
07c3857d-6ea2-43d1-b190-1a6302b1c04b	VERIFY_EMAIL	Verify Email	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	VERIFY_EMAIL	50
284ce6a8-b277-42e5-81e2-e5ce8224dff8	UPDATE_PROFILE	Update Profile	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	UPDATE_PROFILE	40
8e2bd7b9-2339-4eff-9eb7-20741197a320	CONFIGURE_TOTP	Configure OTP	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	CONFIGURE_TOTP	10
a7459e68-44dc-46a4-88ca-e9eabc243896	UPDATE_PASSWORD	Update Password	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	UPDATE_PASSWORD	30
7b3f6d77-fe2d-44cf-bc5e-69933c99e7a7	TERMS_AND_CONDITIONS	Terms and Conditions	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f	f	TERMS_AND_CONDITIONS	20
339dd339-a2b2-416a-8fd8-b0a09fee547f	delete_account	Delete Account	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	f	f	delete_account	60
391b3ee5-8cf6-4661-be1f-f7dc6808e17f	delete_credential	Delete Credential	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	delete_credential	100
98ccc209-8953-49db-8b85-fa290f3b12db	update_user_locale	Update User Locale	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	update_user_locale	1000
1520beed-79a4-4377-a7b6-ddadf9efe8ab	CONFIGURE_RECOVERY_AUTHN_CODES	Recovery Authentication Codes	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	CONFIGURE_RECOVERY_AUTHN_CODES	120
88c66192-2584-40b6-8332-a6b28cf1a9dc	webauthn-register	Webauthn Register	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	webauthn-register	70
9ea011da-a460-4339-95d1-d94d9d7847c3	webauthn-register-passwordless	Webauthn Register Passwordless	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	webauthn-register-passwordless	80
e4f512fd-ebd4-419e-8afd-a5b75bfa8a89	VERIFY_PROFILE	Verify Profile	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	VERIFY_PROFILE	90
c5fbb9d8-cac9-4ecd-a9e2-e1f827616e08	idp_link	Linking Identity Provider	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	t	f	idp_link	110
fbb3465d-d246-4576-b85c-09cbd121c717	VERIFY_EMAIL	Verify Email	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	VERIFY_EMAIL	50
bb95ef7d-d36a-4305-b836-a751a41764ad	UPDATE_PROFILE	Update Profile	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	UPDATE_PROFILE	40
76e76ff3-4171-4e81-8bf5-ed25be560150	CONFIGURE_TOTP	Configure OTP	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	CONFIGURE_TOTP	10
6b97bcf2-415f-42c3-87ec-5e6ca1c1cfda	UPDATE_PASSWORD	Update Password	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	UPDATE_PASSWORD	30
cc003d9f-0658-4b90-8588-1a62758727f9	TERMS_AND_CONDITIONS	Terms and Conditions	1bcb83bb-6f51-410d-972e-8077c4359429	f	f	TERMS_AND_CONDITIONS	20
fb194672-5465-4917-937d-1ead442f2bb0	delete_account	Delete Account	1bcb83bb-6f51-410d-972e-8077c4359429	f	f	delete_account	60
6f12d8a2-1920-4501-8eb8-252258ef14e0	delete_credential	Delete Credential	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	delete_credential	100
19c3a968-976c-4145-a552-252793ca4fc1	update_user_locale	Update User Locale	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	update_user_locale	1000
36293488-9953-4c5a-ab32-4876722b2d55	CONFIGURE_RECOVERY_AUTHN_CODES	Recovery Authentication Codes	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	CONFIGURE_RECOVERY_AUTHN_CODES	120
5d40c801-f3d4-4b25-ad0a-71f842d0d232	webauthn-register	Webauthn Register	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	webauthn-register	70
948963fb-ccbf-4fbe-a386-b7423d033893	webauthn-register-passwordless	Webauthn Register Passwordless	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	webauthn-register-passwordless	80
bdd2da5b-c2a3-494e-a4c9-430933d38292	VERIFY_PROFILE	Verify Profile	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	VERIFY_PROFILE	90
9acd01fe-ef40-4248-a4b7-9455625c8f6c	idp_link	Linking Identity Provider	1bcb83bb-6f51-410d-972e-8077c4359429	t	f	idp_link	110
\.


--
-- TOC entry 4232 (class 0 OID 17847)
-- Dependencies: 296
-- Data for Name: resource_attribute; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.resource_attribute (id, name, value, resource_id) FROM stdin;
\.


--
-- TOC entry 4212 (class 0 OID 17431)
-- Dependencies: 276
-- Data for Name: resource_policy; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.resource_policy (resource_id, policy_id) FROM stdin;
\.


--
-- TOC entry 4211 (class 0 OID 17416)
-- Dependencies: 275
-- Data for Name: resource_scope; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.resource_scope (resource_id, scope_id) FROM stdin;
\.


--
-- TOC entry 4206 (class 0 OID 17354)
-- Dependencies: 270
-- Data for Name: resource_server; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.resource_server (id, allow_rs_remote_mgmt, policy_enforce_mode, decision_strategy) FROM stdin;
\.


--
-- TOC entry 4231 (class 0 OID 17823)
-- Dependencies: 295
-- Data for Name: resource_server_perm_ticket; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.resource_server_perm_ticket (id, owner, requester, created_timestamp, granted_timestamp, resource_id, scope_id, resource_server_id, policy_id) FROM stdin;
\.


--
-- TOC entry 4209 (class 0 OID 17390)
-- Dependencies: 273
-- Data for Name: resource_server_policy; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.resource_server_policy (id, name, description, type, decision_strategy, logic, resource_server_id, owner) FROM stdin;
\.


--
-- TOC entry 4207 (class 0 OID 17362)
-- Dependencies: 271
-- Data for Name: resource_server_resource; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.resource_server_resource (id, name, type, icon_uri, owner, resource_server_id, owner_managed_access, display_name) FROM stdin;
\.


--
-- TOC entry 4208 (class 0 OID 17376)
-- Dependencies: 272
-- Data for Name: resource_server_scope; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.resource_server_scope (id, name, icon_uri, resource_server_id, display_name) FROM stdin;
\.


--
-- TOC entry 4233 (class 0 OID 17865)
-- Dependencies: 297
-- Data for Name: resource_uris; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.resource_uris (resource_id, value) FROM stdin;
\.


--
-- TOC entry 4238 (class 0 OID 17998)
-- Dependencies: 302
-- Data for Name: revoked_token; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.revoked_token (id, expire) FROM stdin;
\.


--
-- TOC entry 4234 (class 0 OID 17875)
-- Dependencies: 298
-- Data for Name: role_attribute; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.role_attribute (id, role_id, name, value) FROM stdin;
\.


--
-- TOC entry 4166 (class 0 OID 16488)
-- Dependencies: 230
-- Data for Name: scope_mapping; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.scope_mapping (client_id, role_id) FROM stdin;
31102431-0756-40bd-bffb-0757ab96be0d	af51e7ce-a9f4-4496-b26d-7484186e701a
31102431-0756-40bd-bffb-0757ab96be0d	f0e24b7d-6967-46f1-b83e-8f6abc1963ff
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	fcfbb7b5-abf7-4cb0-a873-2bb4998f9514
a13d31bb-ec3e-4483-bca7-1b20cd40cfd2	390ee334-47d6-4db3-bb73-812758808e33
\.


--
-- TOC entry 4213 (class 0 OID 17446)
-- Dependencies: 277
-- Data for Name: scope_policy; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.scope_policy (scope_id, policy_id) FROM stdin;
\.


--
-- TOC entry 4240 (class 0 OID 18014)
-- Dependencies: 304
-- Data for Name: server_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.server_config (server_config_key, value, version) FROM stdin;
crt_jgroups	{"prvKey":"MIIEowIBAAKCAQEAm/WiXv2ZI2goIAI4K7jUD/SnLypE2Sua0fcko23U8yyYxSZJXvozse9vmj5V+AVTFu5ORYuw6cbktk+MNczBQ/MRn1oJjjnZjdNeFbClq6J95fHM7lycGPX3pogeEAH+/oqO0ozXWVqaDOswb4bpYXKSxBnVI/IZ4yQCb9Kj05jEs31qxK0rMAIkl3I67NU4iajFUtaKfEJkcKEEun4250mD/kfWOnsK34fFObM9KDKGWazRGMPG8BveC8s/3m+QjkaMa1xvs+BhzSw3pQNwsuZnPzTukW3oNuRvACQINuFznPBadTdiDklDdeLqmot/3ETIUteTXPSEeMuL2mvciQIDAQABAoIBABA+wVeIretovYk8AfLX8UrqLoH0srA2YQ2O40OcvKpJ0m3yw4C/qiPWL3dPF/BaSaHhV8jZuKdZPtiYp5+xAM6doo/JNqaYcwdZZXr36AHdTqTlj3WisuXMtSulERCUzKVh9GeNIR9fFx3SX7y2PlgWsZ/4muGAJd8pkJFqTQNMxCbNSpqe2bamPtPNv5EtiKQCUj18DFrmp5yqNEMP/nEs0c8d6x179aa5jSgBtcK+6FChhC36JoiNxAivi5gME3Wc5PTNuRhRGgWGySGa4AajIsRVc4SdUQsOnzIu76ZD9HPCk9gZmjbmP3Uxfo63/J0oloAMZRQRuFoUTCp70sECgYEA2TeXF/qrb9ABfN3XvGmS4lHJKFQi3peoGLpdfS96q74iL312tKH5cX/dlOY3mA0J2FK8nWNQy5/Mn/CEP8WJPFq3pOSf2Q+JQ+92RF/m7/+u7Of7fRiNeww8iN+Z8dShl5LB+/JKa1I2WFMdcA6CInms/qWOzGyLcXRZAO8DaBECgYEAt84fYnLzOa2X70st4KyU060L6UdxW2r8pDO/rdJLIfbftDU5rNJPpI+QiuhKqKIBgf/k+kHz5ASaTCI7FVaLe5H9ssFObu1PSPfzKiNN1I0440T41bDfKgty612kZo2dh1X89TPDYdsgRjC0bNUa/xo1ZfAnALXqpJWu0PElZPkCgYEAtVeEi59ZKpKz+0PVjNVyrADtd/6t0bk4u9QyooVV2zVzosQSqwCimA/QtDNybD1OX8vK7XZxiF2TXl9UeUz3omx17nJzxKoExbOPv8t0qB6EMgIF4czyqKxxiS9rfQowEbOSwTzoxiovdpOE+v0RxahhNYKP8bgYvhINEAt6g5ECgYABC85H7CmQOXm05fOj5LeQINsBf+OYRpqbgKPlpF3DGrmZN/hVEHldkWecMshB/3IMwF0YQ4nU0WXjawp1C6INmP4dp1X/8Z/S5X5fzgWiYQ9i08Cqzq/meFw2FLlb0BCGdanT53CEPRAI9jwAppHZw9dr9hryuRZVy3hMPiMdWQKBgH9GkCMd2UoZgab8HFGRt0q6i0XUnr8pXXDdWtlGqD7NbJvSzHjUODD+m7qyTIMkJKsBqfS6tojdnOwn9iQRzoSuBey5bbLJM1OG4qRj4IOvg0ryyCjtBjWEUrvKs6x/r1Qm5gXAItl0qDQQHyV+E/eFSVeexmak+E1Cn+QDHp6M","pubKey":"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAm/WiXv2ZI2goIAI4K7jUD/SnLypE2Sua0fcko23U8yyYxSZJXvozse9vmj5V+AVTFu5ORYuw6cbktk+MNczBQ/MRn1oJjjnZjdNeFbClq6J95fHM7lycGPX3pogeEAH+/oqO0ozXWVqaDOswb4bpYXKSxBnVI/IZ4yQCb9Kj05jEs31qxK0rMAIkl3I67NU4iajFUtaKfEJkcKEEun4250mD/kfWOnsK34fFObM9KDKGWazRGMPG8BveC8s/3m+QjkaMa1xvs+BhzSw3pQNwsuZnPzTukW3oNuRvACQINuFznPBadTdiDklDdeLqmot/3ETIUteTXPSEeMuL2mvciQIDAQAB","crt":"MIICnTCCAYUCBgGYZPuerTANBgkqhkiG9w0BAQsFADASMRAwDgYDVQQDDAdqZ3JvdXBzMB4XDTI1MDgwMTA5MzI0OFoXDTI1MDkzMDA5MzQyOFowEjEQMA4GA1UEAwwHamdyb3VwczCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAJv1ol79mSNoKCACOCu41A/0py8qRNkrmtH3JKNt1PMsmMUmSV76M7Hvb5o+VfgFUxbuTkWLsOnG5LZPjDXMwUPzEZ9aCY452Y3TXhWwpauifeXxzO5cnBj196aIHhAB/v6KjtKM11lamgzrMG+G6WFyksQZ1SPyGeMkAm/So9OYxLN9asStKzACJJdyOuzVOImoxVLWinxCZHChBLp+NudJg/5H1jp7Ct+HxTmzPSgyhlms0RjDxvAb3gvLP95vkI5GjGtcb7PgYc0sN6UDcLLmZz807pFt6DbkbwAkCDbhc5zwWnU3Yg5JQ3Xi6pqLf9xEyFLXk1z0hHjLi9pr3IkCAwEAATANBgkqhkiG9w0BAQsFAAOCAQEAE5V6SZvV0TnDTHzBvHaOmWKYaCu5N6bk3cSa6W2c7h0QUt6zX657G+6cavhFzI88j0sBit7esVDUnhhG9BCx0ZVDlN5VzYHHxa1L3HQC2RonP3GYLv61wmX2K3vNZdt3US0VqQ09fJrXGmHB+CnSqP0tLLC19R3e+q6AGDRMXeFEqTk33KPFtlzoNtzii1YPtAYJJIYsQ7Rz3hqvAJL1IuF4gL/2XdDxth0gfLYEJSB+NgPjoQIEk0VZM0NZe1CxaZztDN3gN5ec0ec8GQR4kfbLkvoAH9Xro24chBYVDnB5jJALwMyt+3YxYZXldG7pgAeYTiT+NYopP2kCBQK9Bg==","alias":"6d31c029-aac3-4051-9e94-1f623de6e66d","generatedMillis":1754040868569}	0
\.


--
-- TOC entry 4167 (class 0 OID 16494)
-- Dependencies: 231
-- Data for Name: user_attribute; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_attribute (name, value, user_id, id, long_value_hash, long_value_hash_lower_case, long_value) FROM stdin;
tenant_id	2f59eb4b-d043-4e2c-9256-1e070b3aa191	5212ba59-34d9-477f-b395-6cbf89826932	217987ff-46c2-4e22-a7b8-5965eec3f109	\N	\N	\N
tenantId	xxx	5212ba59-34d9-477f-b395-6cbf89826932	b6e86039-f800-45b7-b118-e00e978946c3	\N	\N	\N
\.


--
-- TOC entry 4186 (class 0 OID 16931)
-- Dependencies: 250
-- Data for Name: user_consent; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_consent (id, client_id, user_id, created_date, last_updated_date, client_storage_provider, external_client_id) FROM stdin;
\.


--
-- TOC entry 4229 (class 0 OID 17798)
-- Dependencies: 293
-- Data for Name: user_consent_client_scope; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_consent_client_scope (user_consent_id, scope_id) FROM stdin;
\.


--
-- TOC entry 4168 (class 0 OID 16499)
-- Dependencies: 232
-- Data for Name: user_entity; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_entity (id, email, email_constraint, email_verified, enabled, federation_link, first_name, last_name, realm_id, username, created_timestamp, service_account_client_link, not_before) FROM stdin;
ebf0197c-6d8a-4074-9cef-a1b7e6f34277	admin@gmail.com	admin@gmail.com	t	t	\N	Nguyễn	Mạnh	9c60f1e0-3006-44bf-be9a-e9110bd2df7a	admin	1754041347707	\N	0
5212ba59-34d9-477f-b395-6cbf89826932	mes@hi-tech.com	mes@hi-tech.com	t	t	\N	Nguyễn	Mạnh	1bcb83bb-6f51-410d-972e-8077c4359429	mes	1754042137242	\N	0
\.


--
-- TOC entry 4169 (class 0 OID 16507)
-- Dependencies: 233
-- Data for Name: user_federation_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_federation_config (user_federation_provider_id, value, name) FROM stdin;
\.


--
-- TOC entry 4192 (class 0 OID 17043)
-- Dependencies: 256
-- Data for Name: user_federation_mapper; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_federation_mapper (id, name, federation_provider_id, federation_mapper_type, realm_id) FROM stdin;
\.


--
-- TOC entry 4193 (class 0 OID 17048)
-- Dependencies: 257
-- Data for Name: user_federation_mapper_config; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_federation_mapper_config (user_federation_mapper_id, value, name) FROM stdin;
\.


--
-- TOC entry 4170 (class 0 OID 16512)
-- Dependencies: 234
-- Data for Name: user_federation_provider; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_federation_provider (id, changed_sync_period, display_name, full_sync_period, last_sync, priority, provider_name, realm_id) FROM stdin;
\.


--
-- TOC entry 4201 (class 0 OID 17211)
-- Dependencies: 265
-- Data for Name: user_group_membership; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_group_membership (group_id, user_id, membership_type) FROM stdin;
bce44866-71b9-4f39-a015-eb9e0e0484b0	5212ba59-34d9-477f-b395-6cbf89826932	UNMANAGED
\.


--
-- TOC entry 4171 (class 0 OID 16517)
-- Dependencies: 235
-- Data for Name: user_required_action; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_required_action (user_id, required_action) FROM stdin;
\.


--
-- TOC entry 4172 (class 0 OID 16520)
-- Dependencies: 236
-- Data for Name: user_role_mapping; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_role_mapping (role_id, user_id) FROM stdin;
24925405-e2df-4f44-bf4d-d5fa744940eb	ebf0197c-6d8a-4074-9cef-a1b7e6f34277
4afc773e-1b6d-43f3-8b05-003cf2f29f59	ebf0197c-6d8a-4074-9cef-a1b7e6f34277
76f8cb98-3d7b-47e6-abed-d3ebcd7203bc	5212ba59-34d9-477f-b395-6cbf89826932
\.


--
-- TOC entry 4173 (class 0 OID 16534)
-- Dependencies: 237
-- Data for Name: web_origins; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.web_origins (client_id, value) FROM stdin;
07df0e9c-0213-48a6-b89f-658593bde218	+
d1827d3b-91a1-4de9-b940-e3ac92e6ee63	+
3f25824c-6d29-4079-a63b-f39091380fb6	*
4a3129e2-ab8f-4d02-b98d-aa9f82145149	*
\.


--
-- TOC entry 3932 (class 2606 OID 17987)
-- Name: org_domain ORG_DOMAIN_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.org_domain
    ADD CONSTRAINT "ORG_DOMAIN_pkey" PRIMARY KEY (id, name);


--
-- TOC entry 3924 (class 2606 OID 17976)
-- Name: org ORG_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.org
    ADD CONSTRAINT "ORG_pkey" PRIMARY KEY (id);


--
-- TOC entry 3940 (class 2606 OID 18021)
-- Name: server_config SERVER_CONFIG_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.server_config
    ADD CONSTRAINT "SERVER_CONFIG_pkey" PRIMARY KEY (server_config_key);


--
-- TOC entry 3654 (class 2606 OID 17899)
-- Name: keycloak_role UK_J3RWUVD56ONTGSUHOGM184WW2-2; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.keycloak_role
    ADD CONSTRAINT "UK_J3RWUVD56ONTGSUHOGM184WW2-2" UNIQUE (name, client_realm_constraint);


--
-- TOC entry 3893 (class 2606 OID 17729)
-- Name: client_auth_flow_bindings c_cli_flow_bind; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_auth_flow_bindings
    ADD CONSTRAINT c_cli_flow_bind PRIMARY KEY (client_id, binding_name);


--
-- TOC entry 3895 (class 2606 OID 17928)
-- Name: client_scope_client c_cli_scope_bind; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_scope_client
    ADD CONSTRAINT c_cli_scope_bind PRIMARY KEY (client_id, scope_id);


--
-- TOC entry 3890 (class 2606 OID 17604)
-- Name: client_initial_access cnstr_client_init_acc_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_initial_access
    ADD CONSTRAINT cnstr_client_init_acc_pk PRIMARY KEY (id);


--
-- TOC entry 3805 (class 2606 OID 17252)
-- Name: realm_default_groups con_group_id_def_groups; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_default_groups
    ADD CONSTRAINT con_group_id_def_groups UNIQUE (group_id);


--
-- TOC entry 3853 (class 2606 OID 17527)
-- Name: broker_link constr_broker_link_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.broker_link
    ADD CONSTRAINT constr_broker_link_pk PRIMARY KEY (identity_provider, user_id);


--
-- TOC entry 3881 (class 2606 OID 17547)
-- Name: component_config constr_component_config_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.component_config
    ADD CONSTRAINT constr_component_config_pk PRIMARY KEY (id);


--
-- TOC entry 3884 (class 2606 OID 17545)
-- Name: component constr_component_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.component
    ADD CONSTRAINT constr_component_pk PRIMARY KEY (id);


--
-- TOC entry 3873 (class 2606 OID 17543)
-- Name: fed_user_required_action constr_fed_required_action; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.fed_user_required_action
    ADD CONSTRAINT constr_fed_required_action PRIMARY KEY (required_action, user_id);


--
-- TOC entry 3855 (class 2606 OID 17529)
-- Name: fed_user_attribute constr_fed_user_attr_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.fed_user_attribute
    ADD CONSTRAINT constr_fed_user_attr_pk PRIMARY KEY (id);


--
-- TOC entry 3860 (class 2606 OID 17531)
-- Name: fed_user_consent constr_fed_user_consent_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.fed_user_consent
    ADD CONSTRAINT constr_fed_user_consent_pk PRIMARY KEY (id);


--
-- TOC entry 3865 (class 2606 OID 17537)
-- Name: fed_user_credential constr_fed_user_cred_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.fed_user_credential
    ADD CONSTRAINT constr_fed_user_cred_pk PRIMARY KEY (id);


--
-- TOC entry 3869 (class 2606 OID 17539)
-- Name: fed_user_group_membership constr_fed_user_group; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.fed_user_group_membership
    ADD CONSTRAINT constr_fed_user_group PRIMARY KEY (group_id, user_id);


--
-- TOC entry 3877 (class 2606 OID 17541)
-- Name: fed_user_role_mapping constr_fed_user_role; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.fed_user_role_mapping
    ADD CONSTRAINT constr_fed_user_role PRIMARY KEY (role_id, user_id);


--
-- TOC entry 3888 (class 2606 OID 17584)
-- Name: federated_user constr_federated_user; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.federated_user
    ADD CONSTRAINT constr_federated_user PRIMARY KEY (id);


--
-- TOC entry 3807 (class 2606 OID 17688)
-- Name: realm_default_groups constr_realm_default_groups; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_default_groups
    ADD CONSTRAINT constr_realm_default_groups PRIMARY KEY (realm_id, group_id);


--
-- TOC entry 3736 (class 2606 OID 17705)
-- Name: realm_enabled_event_types constr_realm_enabl_event_types; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_enabled_event_types
    ADD CONSTRAINT constr_realm_enabl_event_types PRIMARY KEY (realm_id, value);


--
-- TOC entry 3668 (class 2606 OID 17707)
-- Name: realm_events_listeners constr_realm_events_listeners; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_events_listeners
    ADD CONSTRAINT constr_realm_events_listeners PRIMARY KEY (realm_id, value);


--
-- TOC entry 3733 (class 2606 OID 17709)
-- Name: realm_supported_locales constr_realm_supported_locales; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_supported_locales
    ADD CONSTRAINT constr_realm_supported_locales PRIMARY KEY (realm_id, value);


--
-- TOC entry 3724 (class 2606 OID 16838)
-- Name: identity_provider constraint_2b; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.identity_provider
    ADD CONSTRAINT constraint_2b PRIMARY KEY (internal_id);


--
-- TOC entry 3709 (class 2606 OID 16772)
-- Name: client_attributes constraint_3c; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_attributes
    ADD CONSTRAINT constraint_3c PRIMARY KEY (client_id, name);


--
-- TOC entry 3651 (class 2606 OID 16546)
-- Name: event_entity constraint_4; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.event_entity
    ADD CONSTRAINT constraint_4 PRIMARY KEY (id);


--
-- TOC entry 3720 (class 2606 OID 16840)
-- Name: federated_identity constraint_40; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.federated_identity
    ADD CONSTRAINT constraint_40 PRIMARY KEY (identity_provider, user_id);


--
-- TOC entry 3660 (class 2606 OID 16548)
-- Name: realm constraint_4a; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm
    ADD CONSTRAINT constraint_4a PRIMARY KEY (id);


--
-- TOC entry 3697 (class 2606 OID 16554)
-- Name: user_federation_provider constraint_5c; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_federation_provider
    ADD CONSTRAINT constraint_5c PRIMARY KEY (id);


--
-- TOC entry 3639 (class 2606 OID 16558)
-- Name: client constraint_7; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT constraint_7 PRIMARY KEY (id);


--
-- TOC entry 3678 (class 2606 OID 16562)
-- Name: scope_mapping constraint_81; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.scope_mapping
    ADD CONSTRAINT constraint_81 PRIMARY KEY (client_id, role_id);


--
-- TOC entry 3712 (class 2606 OID 16776)
-- Name: client_node_registrations constraint_84; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_node_registrations
    ADD CONSTRAINT constraint_84 PRIMARY KEY (client_id, name);


--
-- TOC entry 3665 (class 2606 OID 16564)
-- Name: realm_attribute constraint_9; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_attribute
    ADD CONSTRAINT constraint_9 PRIMARY KEY (name, realm_id);


--
-- TOC entry 3671 (class 2606 OID 16566)
-- Name: realm_required_credential constraint_92; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_required_credential
    ADD CONSTRAINT constraint_92 PRIMARY KEY (realm_id, type);


--
-- TOC entry 3656 (class 2606 OID 16568)
-- Name: keycloak_role constraint_a; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.keycloak_role
    ADD CONSTRAINT constraint_a PRIMARY KEY (id);


--
-- TOC entry 3758 (class 2606 OID 17692)
-- Name: admin_event_entity constraint_admin_event_entity; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.admin_event_entity
    ADD CONSTRAINT constraint_admin_event_entity PRIMARY KEY (id);


--
-- TOC entry 3771 (class 2606 OID 17069)
-- Name: authenticator_config_entry constraint_auth_cfg_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.authenticator_config_entry
    ADD CONSTRAINT constraint_auth_cfg_pk PRIMARY KEY (authenticator_id, name);


--
-- TOC entry 3767 (class 2606 OID 17067)
-- Name: authentication_execution constraint_auth_exec_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.authentication_execution
    ADD CONSTRAINT constraint_auth_exec_pk PRIMARY KEY (id);


--
-- TOC entry 3764 (class 2606 OID 17065)
-- Name: authentication_flow constraint_auth_flow_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.authentication_flow
    ADD CONSTRAINT constraint_auth_flow_pk PRIMARY KEY (id);


--
-- TOC entry 3761 (class 2606 OID 17063)
-- Name: authenticator_config constraint_auth_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.authenticator_config
    ADD CONSTRAINT constraint_auth_pk PRIMARY KEY (id);


--
-- TOC entry 3703 (class 2606 OID 16570)
-- Name: user_role_mapping constraint_c; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_role_mapping
    ADD CONSTRAINT constraint_c PRIMARY KEY (role_id, user_id);


--
-- TOC entry 3644 (class 2606 OID 17686)
-- Name: composite_role constraint_composite_role; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.composite_role
    ADD CONSTRAINT constraint_composite_role PRIMARY KEY (composite, child_role);


--
-- TOC entry 3731 (class 2606 OID 16842)
-- Name: identity_provider_config constraint_d; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.identity_provider_config
    ADD CONSTRAINT constraint_d PRIMARY KEY (identity_provider_id, name);


--
-- TOC entry 3839 (class 2606 OID 17410)
-- Name: policy_config constraint_dpc; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.policy_config
    ADD CONSTRAINT constraint_dpc PRIMARY KEY (policy_id, name);


--
-- TOC entry 3673 (class 2606 OID 16572)
-- Name: realm_smtp_config constraint_e; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_smtp_config
    ADD CONSTRAINT constraint_e PRIMARY KEY (realm_id, name);


--
-- TOC entry 3648 (class 2606 OID 16574)
-- Name: credential constraint_f; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.credential
    ADD CONSTRAINT constraint_f PRIMARY KEY (id);


--
-- TOC entry 3695 (class 2606 OID 16576)
-- Name: user_federation_config constraint_f9; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_federation_config
    ADD CONSTRAINT constraint_f9 PRIMARY KEY (user_federation_provider_id, name);


--
-- TOC entry 3909 (class 2606 OID 17827)
-- Name: resource_server_perm_ticket constraint_fapmt; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT constraint_fapmt PRIMARY KEY (id);


--
-- TOC entry 3824 (class 2606 OID 17368)
-- Name: resource_server_resource constraint_farsr; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_resource
    ADD CONSTRAINT constraint_farsr PRIMARY KEY (id);


--
-- TOC entry 3834 (class 2606 OID 17396)
-- Name: resource_server_policy constraint_farsrp; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_policy
    ADD CONSTRAINT constraint_farsrp PRIMARY KEY (id);


--
-- TOC entry 3850 (class 2606 OID 17465)
-- Name: associated_policy constraint_farsrpap; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.associated_policy
    ADD CONSTRAINT constraint_farsrpap PRIMARY KEY (policy_id, associated_policy_id);


--
-- TOC entry 3844 (class 2606 OID 17435)
-- Name: resource_policy constraint_farsrpp; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_policy
    ADD CONSTRAINT constraint_farsrpp PRIMARY KEY (resource_id, policy_id);


--
-- TOC entry 3829 (class 2606 OID 17382)
-- Name: resource_server_scope constraint_farsrs; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_scope
    ADD CONSTRAINT constraint_farsrs PRIMARY KEY (id);


--
-- TOC entry 3841 (class 2606 OID 17420)
-- Name: resource_scope constraint_farsrsp; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_scope
    ADD CONSTRAINT constraint_farsrsp PRIMARY KEY (resource_id, scope_id);


--
-- TOC entry 3847 (class 2606 OID 17450)
-- Name: scope_policy constraint_farsrsps; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.scope_policy
    ADD CONSTRAINT constraint_farsrsps PRIMARY KEY (scope_id, policy_id);


--
-- TOC entry 3687 (class 2606 OID 16578)
-- Name: user_entity constraint_fb; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_entity
    ADD CONSTRAINT constraint_fb PRIMARY KEY (id);


--
-- TOC entry 3777 (class 2606 OID 17077)
-- Name: user_federation_mapper_config constraint_fedmapper_cfg_pm; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_federation_mapper_config
    ADD CONSTRAINT constraint_fedmapper_cfg_pm PRIMARY KEY (user_federation_mapper_id, name);


--
-- TOC entry 3773 (class 2606 OID 17075)
-- Name: user_federation_mapper constraint_fedmapperpm; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_federation_mapper
    ADD CONSTRAINT constraint_fedmapperpm PRIMARY KEY (id);


--
-- TOC entry 3907 (class 2606 OID 17812)
-- Name: fed_user_consent_cl_scope constraint_fgrntcsnt_clsc_pm; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.fed_user_consent_cl_scope
    ADD CONSTRAINT constraint_fgrntcsnt_clsc_pm PRIMARY KEY (user_consent_id, scope_id);


--
-- TOC entry 3903 (class 2606 OID 17802)
-- Name: user_consent_client_scope constraint_grntcsnt_clsc_pm; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_consent_client_scope
    ADD CONSTRAINT constraint_grntcsnt_clsc_pm PRIMARY KEY (user_consent_id, scope_id);


--
-- TOC entry 3751 (class 2606 OID 16950)
-- Name: user_consent constraint_grntcsnt_pm; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_consent
    ADD CONSTRAINT constraint_grntcsnt_pm PRIMARY KEY (id);


--
-- TOC entry 3791 (class 2606 OID 17219)
-- Name: keycloak_group constraint_group; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.keycloak_group
    ADD CONSTRAINT constraint_group PRIMARY KEY (id);


--
-- TOC entry 3798 (class 2606 OID 17226)
-- Name: group_attribute constraint_group_attribute_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.group_attribute
    ADD CONSTRAINT constraint_group_attribute_pk PRIMARY KEY (id);


--
-- TOC entry 3795 (class 2606 OID 17240)
-- Name: group_role_mapping constraint_group_role; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.group_role_mapping
    ADD CONSTRAINT constraint_group_role PRIMARY KEY (role_id, group_id);


--
-- TOC entry 3746 (class 2606 OID 16946)
-- Name: identity_provider_mapper constraint_idpm; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.identity_provider_mapper
    ADD CONSTRAINT constraint_idpm PRIMARY KEY (id);


--
-- TOC entry 3749 (class 2606 OID 17126)
-- Name: idp_mapper_config constraint_idpmconfig; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.idp_mapper_config
    ADD CONSTRAINT constraint_idpmconfig PRIMARY KEY (idp_mapper_id, name);


--
-- TOC entry 3938 (class 2606 OID 18013)
-- Name: jgroups_ping constraint_jgroups_ping; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.jgroups_ping
    ADD CONSTRAINT constraint_jgroups_ping PRIMARY KEY (address);


--
-- TOC entry 3739 (class 2606 OID 16944)
-- Name: migration_model constraint_migmod; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.migration_model
    ADD CONSTRAINT constraint_migmod PRIMARY KEY (id);


--
-- TOC entry 3789 (class 2606 OID 17905)
-- Name: offline_client_session constraint_offl_cl_ses_pk3; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.offline_client_session
    ADD CONSTRAINT constraint_offl_cl_ses_pk3 PRIMARY KEY (user_session_id, client_id, client_storage_provider, external_client_id, offline_flag);


--
-- TOC entry 3784 (class 2606 OID 17196)
-- Name: offline_user_session constraint_offl_us_ses_pk2; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.offline_user_session
    ADD CONSTRAINT constraint_offl_us_ses_pk2 PRIMARY KEY (user_session_id, offline_flag);


--
-- TOC entry 3714 (class 2606 OID 16836)
-- Name: protocol_mapper constraint_pcm; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.protocol_mapper
    ADD CONSTRAINT constraint_pcm PRIMARY KEY (id);


--
-- TOC entry 3718 (class 2606 OID 17119)
-- Name: protocol_mapper_config constraint_pmconfig; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.protocol_mapper_config
    ADD CONSTRAINT constraint_pmconfig PRIMARY KEY (protocol_mapper_id, name);


--
-- TOC entry 3675 (class 2606 OID 17711)
-- Name: redirect_uris constraint_redirect_uris; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.redirect_uris
    ADD CONSTRAINT constraint_redirect_uris PRIMARY KEY (client_id, value);


--
-- TOC entry 3782 (class 2606 OID 17159)
-- Name: required_action_config constraint_req_act_cfg_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.required_action_config
    ADD CONSTRAINT constraint_req_act_cfg_pk PRIMARY KEY (required_action_id, name);


--
-- TOC entry 3779 (class 2606 OID 17157)
-- Name: required_action_provider constraint_req_act_prv_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.required_action_provider
    ADD CONSTRAINT constraint_req_act_prv_pk PRIMARY KEY (id);


--
-- TOC entry 3700 (class 2606 OID 17071)
-- Name: user_required_action constraint_required_action; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_required_action
    ADD CONSTRAINT constraint_required_action PRIMARY KEY (required_action, user_id);


--
-- TOC entry 3917 (class 2606 OID 17874)
-- Name: resource_uris constraint_resour_uris_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_uris
    ADD CONSTRAINT constraint_resour_uris_pk PRIMARY KEY (resource_id, value);


--
-- TOC entry 3919 (class 2606 OID 17881)
-- Name: role_attribute constraint_role_attribute_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.role_attribute
    ADD CONSTRAINT constraint_role_attribute_pk PRIMARY KEY (id);


--
-- TOC entry 3935 (class 2606 OID 18002)
-- Name: revoked_token constraint_rt; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.revoked_token
    ADD CONSTRAINT constraint_rt PRIMARY KEY (id);


--
-- TOC entry 3681 (class 2606 OID 17155)
-- Name: user_attribute constraint_user_attribute_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_attribute
    ADD CONSTRAINT constraint_user_attribute_pk PRIMARY KEY (id);


--
-- TOC entry 3802 (class 2606 OID 17233)
-- Name: user_group_membership constraint_user_group; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_group_membership
    ADD CONSTRAINT constraint_user_group PRIMARY KEY (group_id, user_id);


--
-- TOC entry 3706 (class 2606 OID 17713)
-- Name: web_origins constraint_web_origins; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.web_origins
    ADD CONSTRAINT constraint_web_origins PRIMARY KEY (client_id, value);


--
-- TOC entry 3637 (class 2606 OID 16390)
-- Name: databasechangeloglock databasechangeloglock_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.databasechangeloglock
    ADD CONSTRAINT databasechangeloglock_pkey PRIMARY KEY (id);


--
-- TOC entry 3816 (class 2606 OID 17336)
-- Name: client_scope_attributes pk_cl_tmpl_attr; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_scope_attributes
    ADD CONSTRAINT pk_cl_tmpl_attr PRIMARY KEY (scope_id, name);


--
-- TOC entry 3811 (class 2606 OID 17295)
-- Name: client_scope pk_cli_template; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_scope
    ADD CONSTRAINT pk_cli_template PRIMARY KEY (id);


--
-- TOC entry 3822 (class 2606 OID 17666)
-- Name: resource_server pk_resource_server; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server
    ADD CONSTRAINT pk_resource_server PRIMARY KEY (id);


--
-- TOC entry 3820 (class 2606 OID 17324)
-- Name: client_scope_role_mapping pk_template_scope; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_scope_role_mapping
    ADD CONSTRAINT pk_template_scope PRIMARY KEY (scope_id, role_id);


--
-- TOC entry 3901 (class 2606 OID 17787)
-- Name: default_client_scope r_def_cli_scope_bind; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.default_client_scope
    ADD CONSTRAINT r_def_cli_scope_bind PRIMARY KEY (realm_id, scope_id);


--
-- TOC entry 3922 (class 2606 OID 17922)
-- Name: realm_localizations realm_localizations_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_localizations
    ADD CONSTRAINT realm_localizations_pkey PRIMARY KEY (realm_id, locale);


--
-- TOC entry 3915 (class 2606 OID 17854)
-- Name: resource_attribute res_attr_pk; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_attribute
    ADD CONSTRAINT res_attr_pk PRIMARY KEY (id);


--
-- TOC entry 3793 (class 2606 OID 17596)
-- Name: keycloak_group sibling_names; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.keycloak_group
    ADD CONSTRAINT sibling_names UNIQUE (realm_id, parent_group, name);


--
-- TOC entry 3729 (class 2606 OID 16893)
-- Name: identity_provider uk_2daelwnibji49avxsrtuf6xj33; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.identity_provider
    ADD CONSTRAINT uk_2daelwnibji49avxsrtuf6xj33 UNIQUE (provider_alias, realm_id);


--
-- TOC entry 3642 (class 2606 OID 16582)
-- Name: client uk_b71cjlbenv945rb6gcon438at; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT uk_b71cjlbenv945rb6gcon438at UNIQUE (realm_id, client_id);


--
-- TOC entry 3813 (class 2606 OID 17740)
-- Name: client_scope uk_cli_scope; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_scope
    ADD CONSTRAINT uk_cli_scope UNIQUE (realm_id, name);


--
-- TOC entry 3691 (class 2606 OID 16586)
-- Name: user_entity uk_dykn684sl8up1crfei6eckhd7; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_entity
    ADD CONSTRAINT uk_dykn684sl8up1crfei6eckhd7 UNIQUE (realm_id, email_constraint);


--
-- TOC entry 3754 (class 2606 OID 17991)
-- Name: user_consent uk_external_consent; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_consent
    ADD CONSTRAINT uk_external_consent UNIQUE (client_storage_provider, external_client_id, user_id);


--
-- TOC entry 3827 (class 2606 OID 17913)
-- Name: resource_server_resource uk_frsr6t700s9v50bu18ws5ha6; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_resource
    ADD CONSTRAINT uk_frsr6t700s9v50bu18ws5ha6 UNIQUE (name, owner, resource_server_id);


--
-- TOC entry 3913 (class 2606 OID 17909)
-- Name: resource_server_perm_ticket uk_frsr6t700s9v50bu18ws5pmt; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT uk_frsr6t700s9v50bu18ws5pmt UNIQUE (owner, requester, resource_server_id, resource_id, scope_id);


--
-- TOC entry 3837 (class 2606 OID 17657)
-- Name: resource_server_policy uk_frsrpt700s9v50bu18ws5ha6; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_policy
    ADD CONSTRAINT uk_frsrpt700s9v50bu18ws5ha6 UNIQUE (name, resource_server_id);


--
-- TOC entry 3832 (class 2606 OID 17661)
-- Name: resource_server_scope uk_frsrst700s9v50bu18ws5ha6; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_scope
    ADD CONSTRAINT uk_frsrst700s9v50bu18ws5ha6 UNIQUE (name, resource_server_id);


--
-- TOC entry 3756 (class 2606 OID 17989)
-- Name: user_consent uk_local_consent; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_consent
    ADD CONSTRAINT uk_local_consent UNIQUE (client_id, user_id);


--
-- TOC entry 3742 (class 2606 OID 18026)
-- Name: migration_model uk_migration_update_time; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.migration_model
    ADD CONSTRAINT uk_migration_update_time UNIQUE (update_time);


--
-- TOC entry 3744 (class 2606 OID 18024)
-- Name: migration_model uk_migration_version; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.migration_model
    ADD CONSTRAINT uk_migration_version UNIQUE (version);


--
-- TOC entry 3926 (class 2606 OID 17995)
-- Name: org uk_org_alias; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.org
    ADD CONSTRAINT uk_org_alias UNIQUE (realm_id, alias);


--
-- TOC entry 3928 (class 2606 OID 17980)
-- Name: org uk_org_group; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.org
    ADD CONSTRAINT uk_org_group UNIQUE (group_id);


--
-- TOC entry 3930 (class 2606 OID 17978)
-- Name: org uk_org_name; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.org
    ADD CONSTRAINT uk_org_name UNIQUE (realm_id, name);


--
-- TOC entry 3663 (class 2606 OID 16594)
-- Name: realm uk_orvsdmla56612eaefiq6wl5oi; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm
    ADD CONSTRAINT uk_orvsdmla56612eaefiq6wl5oi UNIQUE (name);


--
-- TOC entry 3693 (class 2606 OID 17586)
-- Name: user_entity uk_ru8tt6t700s9v50bu18ws5ha6; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_entity
    ADD CONSTRAINT uk_ru8tt6t700s9v50bu18ws5ha6 UNIQUE (realm_id, username);


--
-- TOC entry 3856 (class 1259 OID 17962)
-- Name: fed_user_attr_long_values; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX fed_user_attr_long_values ON public.fed_user_attribute USING btree (long_value_hash, name);


--
-- TOC entry 3857 (class 1259 OID 17964)
-- Name: fed_user_attr_long_values_lower_case; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX fed_user_attr_long_values_lower_case ON public.fed_user_attribute USING btree (long_value_hash_lower_case, name);


--
-- TOC entry 3759 (class 1259 OID 17938)
-- Name: idx_admin_event_time; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_admin_event_time ON public.admin_event_entity USING btree (realm_id, admin_event_time);


--
-- TOC entry 3851 (class 1259 OID 17610)
-- Name: idx_assoc_pol_assoc_pol_id; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_assoc_pol_assoc_pol_id ON public.associated_policy USING btree (associated_policy_id);


--
-- TOC entry 3762 (class 1259 OID 17614)
-- Name: idx_auth_config_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_auth_config_realm ON public.authenticator_config USING btree (realm_id);


--
-- TOC entry 3768 (class 1259 OID 17612)
-- Name: idx_auth_exec_flow; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_auth_exec_flow ON public.authentication_execution USING btree (flow_id);


--
-- TOC entry 3769 (class 1259 OID 17611)
-- Name: idx_auth_exec_realm_flow; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_auth_exec_realm_flow ON public.authentication_execution USING btree (realm_id, flow_id);


--
-- TOC entry 3765 (class 1259 OID 17613)
-- Name: idx_auth_flow_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_auth_flow_realm ON public.authentication_flow USING btree (realm_id);


--
-- TOC entry 3896 (class 1259 OID 17929)
-- Name: idx_cl_clscope; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_cl_clscope ON public.client_scope_client USING btree (scope_id);


--
-- TOC entry 3710 (class 1259 OID 17965)
-- Name: idx_client_att_by_name_value; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_client_att_by_name_value ON public.client_attributes USING btree (name, substr(value, 1, 255));


--
-- TOC entry 3640 (class 1259 OID 17914)
-- Name: idx_client_id; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_client_id ON public.client USING btree (client_id);


--
-- TOC entry 3891 (class 1259 OID 17654)
-- Name: idx_client_init_acc_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_client_init_acc_realm ON public.client_initial_access USING btree (realm_id);


--
-- TOC entry 3814 (class 1259 OID 17817)
-- Name: idx_clscope_attrs; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_clscope_attrs ON public.client_scope_attributes USING btree (scope_id);


--
-- TOC entry 3897 (class 1259 OID 17926)
-- Name: idx_clscope_cl; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_clscope_cl ON public.client_scope_client USING btree (client_id);


--
-- TOC entry 3715 (class 1259 OID 17814)
-- Name: idx_clscope_protmap; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_clscope_protmap ON public.protocol_mapper USING btree (client_scope_id);


--
-- TOC entry 3817 (class 1259 OID 17815)
-- Name: idx_clscope_role; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_clscope_role ON public.client_scope_role_mapping USING btree (scope_id);


--
-- TOC entry 3882 (class 1259 OID 17620)
-- Name: idx_compo_config_compo; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_compo_config_compo ON public.component_config USING btree (component_id);


--
-- TOC entry 3885 (class 1259 OID 17888)
-- Name: idx_component_provider_type; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_component_provider_type ON public.component USING btree (provider_type);


--
-- TOC entry 3886 (class 1259 OID 17619)
-- Name: idx_component_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_component_realm ON public.component USING btree (realm_id);


--
-- TOC entry 3645 (class 1259 OID 17621)
-- Name: idx_composite; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_composite ON public.composite_role USING btree (composite);


--
-- TOC entry 3646 (class 1259 OID 17622)
-- Name: idx_composite_child; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_composite_child ON public.composite_role USING btree (child_role);


--
-- TOC entry 3898 (class 1259 OID 17820)
-- Name: idx_defcls_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_defcls_realm ON public.default_client_scope USING btree (realm_id);


--
-- TOC entry 3899 (class 1259 OID 17821)
-- Name: idx_defcls_scope; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_defcls_scope ON public.default_client_scope USING btree (scope_id);


--
-- TOC entry 3652 (class 1259 OID 17915)
-- Name: idx_event_time; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_event_time ON public.event_entity USING btree (realm_id, event_time);


--
-- TOC entry 3721 (class 1259 OID 17353)
-- Name: idx_fedidentity_feduser; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fedidentity_feduser ON public.federated_identity USING btree (federated_user_id);


--
-- TOC entry 3722 (class 1259 OID 17352)
-- Name: idx_fedidentity_user; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fedidentity_user ON public.federated_identity USING btree (user_id);


--
-- TOC entry 3858 (class 1259 OID 17714)
-- Name: idx_fu_attribute; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_attribute ON public.fed_user_attribute USING btree (user_id, realm_id, name);


--
-- TOC entry 3861 (class 1259 OID 17734)
-- Name: idx_fu_cnsnt_ext; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_cnsnt_ext ON public.fed_user_consent USING btree (user_id, client_storage_provider, external_client_id);


--
-- TOC entry 3862 (class 1259 OID 17897)
-- Name: idx_fu_consent; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_consent ON public.fed_user_consent USING btree (user_id, client_id);


--
-- TOC entry 3863 (class 1259 OID 17716)
-- Name: idx_fu_consent_ru; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_consent_ru ON public.fed_user_consent USING btree (realm_id, user_id);


--
-- TOC entry 3866 (class 1259 OID 17717)
-- Name: idx_fu_credential; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_credential ON public.fed_user_credential USING btree (user_id, type);


--
-- TOC entry 3867 (class 1259 OID 17718)
-- Name: idx_fu_credential_ru; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_credential_ru ON public.fed_user_credential USING btree (realm_id, user_id);


--
-- TOC entry 3870 (class 1259 OID 17719)
-- Name: idx_fu_group_membership; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_group_membership ON public.fed_user_group_membership USING btree (user_id, group_id);


--
-- TOC entry 3871 (class 1259 OID 17720)
-- Name: idx_fu_group_membership_ru; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_group_membership_ru ON public.fed_user_group_membership USING btree (realm_id, user_id);


--
-- TOC entry 3874 (class 1259 OID 17721)
-- Name: idx_fu_required_action; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_required_action ON public.fed_user_required_action USING btree (user_id, required_action);


--
-- TOC entry 3875 (class 1259 OID 17722)
-- Name: idx_fu_required_action_ru; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_required_action_ru ON public.fed_user_required_action USING btree (realm_id, user_id);


--
-- TOC entry 3878 (class 1259 OID 17723)
-- Name: idx_fu_role_mapping; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_role_mapping ON public.fed_user_role_mapping USING btree (user_id, role_id);


--
-- TOC entry 3879 (class 1259 OID 17724)
-- Name: idx_fu_role_mapping_ru; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_fu_role_mapping_ru ON public.fed_user_role_mapping USING btree (realm_id, user_id);


--
-- TOC entry 3799 (class 1259 OID 17940)
-- Name: idx_group_att_by_name_value; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_group_att_by_name_value ON public.group_attribute USING btree (name, ((value)::character varying(250)));


--
-- TOC entry 3800 (class 1259 OID 17625)
-- Name: idx_group_attr_group; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_group_attr_group ON public.group_attribute USING btree (group_id);


--
-- TOC entry 3796 (class 1259 OID 17626)
-- Name: idx_group_role_mapp_group; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_group_role_mapp_group ON public.group_role_mapping USING btree (group_id);


--
-- TOC entry 3747 (class 1259 OID 17628)
-- Name: idx_id_prov_mapp_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_id_prov_mapp_realm ON public.identity_provider_mapper USING btree (realm_id);


--
-- TOC entry 3725 (class 1259 OID 17627)
-- Name: idx_ident_prov_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_ident_prov_realm ON public.identity_provider USING btree (realm_id);


--
-- TOC entry 3726 (class 1259 OID 18006)
-- Name: idx_idp_for_login; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_idp_for_login ON public.identity_provider USING btree (realm_id, enabled, link_only, hide_on_login, organization_id);


--
-- TOC entry 3727 (class 1259 OID 18005)
-- Name: idx_idp_realm_org; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_idp_realm_org ON public.identity_provider USING btree (realm_id, organization_id);


--
-- TOC entry 3657 (class 1259 OID 17629)
-- Name: idx_keycloak_role_client; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_keycloak_role_client ON public.keycloak_role USING btree (client);


--
-- TOC entry 3658 (class 1259 OID 17630)
-- Name: idx_keycloak_role_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_keycloak_role_realm ON public.keycloak_role USING btree (realm);


--
-- TOC entry 3785 (class 1259 OID 17969)
-- Name: idx_offline_uss_by_broker_session_id; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_offline_uss_by_broker_session_id ON public.offline_user_session USING btree (broker_session_id, realm_id);


--
-- TOC entry 3786 (class 1259 OID 17968)
-- Name: idx_offline_uss_by_last_session_refresh; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_offline_uss_by_last_session_refresh ON public.offline_user_session USING btree (realm_id, offline_flag, last_session_refresh);


--
-- TOC entry 3787 (class 1259 OID 17933)
-- Name: idx_offline_uss_by_user; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_offline_uss_by_user ON public.offline_user_session USING btree (user_id, realm_id, offline_flag);


--
-- TOC entry 3933 (class 1259 OID 17997)
-- Name: idx_org_domain_org_id; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_org_domain_org_id ON public.org_domain USING btree (org_id);


--
-- TOC entry 3910 (class 1259 OID 17993)
-- Name: idx_perm_ticket_owner; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_perm_ticket_owner ON public.resource_server_perm_ticket USING btree (owner);


--
-- TOC entry 3911 (class 1259 OID 17992)
-- Name: idx_perm_ticket_requester; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_perm_ticket_requester ON public.resource_server_perm_ticket USING btree (requester);


--
-- TOC entry 3716 (class 1259 OID 17631)
-- Name: idx_protocol_mapper_client; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_protocol_mapper_client ON public.protocol_mapper USING btree (client_id);


--
-- TOC entry 3666 (class 1259 OID 17634)
-- Name: idx_realm_attr_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_realm_attr_realm ON public.realm_attribute USING btree (realm_id);


--
-- TOC entry 3809 (class 1259 OID 17813)
-- Name: idx_realm_clscope; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_realm_clscope ON public.client_scope USING btree (realm_id);


--
-- TOC entry 3808 (class 1259 OID 17635)
-- Name: idx_realm_def_grp_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_realm_def_grp_realm ON public.realm_default_groups USING btree (realm_id);


--
-- TOC entry 3669 (class 1259 OID 17638)
-- Name: idx_realm_evt_list_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_realm_evt_list_realm ON public.realm_events_listeners USING btree (realm_id);


--
-- TOC entry 3737 (class 1259 OID 17637)
-- Name: idx_realm_evt_types_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_realm_evt_types_realm ON public.realm_enabled_event_types USING btree (realm_id);


--
-- TOC entry 3661 (class 1259 OID 17633)
-- Name: idx_realm_master_adm_cli; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_realm_master_adm_cli ON public.realm USING btree (master_admin_client);


--
-- TOC entry 3734 (class 1259 OID 17639)
-- Name: idx_realm_supp_local_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_realm_supp_local_realm ON public.realm_supported_locales USING btree (realm_id);


--
-- TOC entry 3676 (class 1259 OID 17640)
-- Name: idx_redir_uri_client; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_redir_uri_client ON public.redirect_uris USING btree (client_id);


--
-- TOC entry 3780 (class 1259 OID 17641)
-- Name: idx_req_act_prov_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_req_act_prov_realm ON public.required_action_provider USING btree (realm_id);


--
-- TOC entry 3845 (class 1259 OID 17642)
-- Name: idx_res_policy_policy; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_res_policy_policy ON public.resource_policy USING btree (policy_id);


--
-- TOC entry 3842 (class 1259 OID 17643)
-- Name: idx_res_scope_scope; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_res_scope_scope ON public.resource_scope USING btree (scope_id);


--
-- TOC entry 3835 (class 1259 OID 17662)
-- Name: idx_res_serv_pol_res_serv; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_res_serv_pol_res_serv ON public.resource_server_policy USING btree (resource_server_id);


--
-- TOC entry 3825 (class 1259 OID 17663)
-- Name: idx_res_srv_res_res_srv; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_res_srv_res_res_srv ON public.resource_server_resource USING btree (resource_server_id);


--
-- TOC entry 3830 (class 1259 OID 17664)
-- Name: idx_res_srv_scope_res_srv; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_res_srv_scope_res_srv ON public.resource_server_scope USING btree (resource_server_id);


--
-- TOC entry 3936 (class 1259 OID 18003)
-- Name: idx_rev_token_on_expire; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_rev_token_on_expire ON public.revoked_token USING btree (expire);


--
-- TOC entry 3920 (class 1259 OID 17887)
-- Name: idx_role_attribute; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_role_attribute ON public.role_attribute USING btree (role_id);


--
-- TOC entry 3818 (class 1259 OID 17816)
-- Name: idx_role_clscope; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_role_clscope ON public.client_scope_role_mapping USING btree (role_id);


--
-- TOC entry 3679 (class 1259 OID 17647)
-- Name: idx_scope_mapping_role; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_scope_mapping_role ON public.scope_mapping USING btree (role_id);


--
-- TOC entry 3848 (class 1259 OID 17648)
-- Name: idx_scope_policy_policy; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_scope_policy_policy ON public.scope_policy USING btree (policy_id);


--
-- TOC entry 3740 (class 1259 OID 17895)
-- Name: idx_update_time; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_update_time ON public.migration_model USING btree (update_time);


--
-- TOC entry 3904 (class 1259 OID 17822)
-- Name: idx_usconsent_clscope; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_usconsent_clscope ON public.user_consent_client_scope USING btree (user_consent_id);


--
-- TOC entry 3905 (class 1259 OID 17939)
-- Name: idx_usconsent_scope_id; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_usconsent_scope_id ON public.user_consent_client_scope USING btree (scope_id);


--
-- TOC entry 3682 (class 1259 OID 17349)
-- Name: idx_user_attribute; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_user_attribute ON public.user_attribute USING btree (user_id);


--
-- TOC entry 3683 (class 1259 OID 17936)
-- Name: idx_user_attribute_name; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_user_attribute_name ON public.user_attribute USING btree (name, value);


--
-- TOC entry 3752 (class 1259 OID 17346)
-- Name: idx_user_consent; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_user_consent ON public.user_consent USING btree (user_id);


--
-- TOC entry 3649 (class 1259 OID 17350)
-- Name: idx_user_credential; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_user_credential ON public.credential USING btree (user_id);


--
-- TOC entry 3688 (class 1259 OID 17343)
-- Name: idx_user_email; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_user_email ON public.user_entity USING btree (email);


--
-- TOC entry 3803 (class 1259 OID 17345)
-- Name: idx_user_group_mapping; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_user_group_mapping ON public.user_group_membership USING btree (user_id);


--
-- TOC entry 3701 (class 1259 OID 17351)
-- Name: idx_user_reqactions; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_user_reqactions ON public.user_required_action USING btree (user_id);


--
-- TOC entry 3704 (class 1259 OID 17344)
-- Name: idx_user_role_mapping; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_user_role_mapping ON public.user_role_mapping USING btree (user_id);


--
-- TOC entry 3689 (class 1259 OID 17937)
-- Name: idx_user_service_account; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_user_service_account ON public.user_entity USING btree (realm_id, service_account_client_link);


--
-- TOC entry 3774 (class 1259 OID 17650)
-- Name: idx_usr_fed_map_fed_prv; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_usr_fed_map_fed_prv ON public.user_federation_mapper USING btree (federation_provider_id);


--
-- TOC entry 3775 (class 1259 OID 17651)
-- Name: idx_usr_fed_map_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_usr_fed_map_realm ON public.user_federation_mapper USING btree (realm_id);


--
-- TOC entry 3698 (class 1259 OID 17652)
-- Name: idx_usr_fed_prv_realm; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_usr_fed_prv_realm ON public.user_federation_provider USING btree (realm_id);


--
-- TOC entry 3707 (class 1259 OID 17653)
-- Name: idx_web_orig_client; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX idx_web_orig_client ON public.web_origins USING btree (client_id);


--
-- TOC entry 3684 (class 1259 OID 17961)
-- Name: user_attr_long_values; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX user_attr_long_values ON public.user_attribute USING btree (long_value_hash, name);


--
-- TOC entry 3685 (class 1259 OID 17963)
-- Name: user_attr_long_values_lower_case; Type: INDEX; Schema: public; Owner: admin
--

CREATE INDEX user_attr_long_values_lower_case ON public.user_attribute USING btree (long_value_hash_lower_case, name);


--
-- TOC entry 3963 (class 2606 OID 16847)
-- Name: identity_provider fk2b4ebc52ae5c3b34; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.identity_provider
    ADD CONSTRAINT fk2b4ebc52ae5c3b34 FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3957 (class 2606 OID 16777)
-- Name: client_attributes fk3c47c64beacca966; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_attributes
    ADD CONSTRAINT fk3c47c64beacca966 FOREIGN KEY (client_id) REFERENCES public.client(id);


--
-- TOC entry 3962 (class 2606 OID 16857)
-- Name: federated_identity fk404288b92ef007a6; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.federated_identity
    ADD CONSTRAINT fk404288b92ef007a6 FOREIGN KEY (user_id) REFERENCES public.user_entity(id);


--
-- TOC entry 3958 (class 2606 OID 17004)
-- Name: client_node_registrations fk4129723ba992f594; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_node_registrations
    ADD CONSTRAINT fk4129723ba992f594 FOREIGN KEY (client_id) REFERENCES public.client(id);


--
-- TOC entry 3949 (class 2606 OID 16602)
-- Name: redirect_uris fk_1burs8pb4ouj97h5wuppahv9f; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.redirect_uris
    ADD CONSTRAINT fk_1burs8pb4ouj97h5wuppahv9f FOREIGN KEY (client_id) REFERENCES public.client(id);


--
-- TOC entry 3953 (class 2606 OID 16607)
-- Name: user_federation_provider fk_1fj32f6ptolw2qy60cd8n01e8; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_federation_provider
    ADD CONSTRAINT fk_1fj32f6ptolw2qy60cd8n01e8 FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3947 (class 2606 OID 16617)
-- Name: realm_required_credential fk_5hg65lybevavkqfki3kponh9v; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_required_credential
    ADD CONSTRAINT fk_5hg65lybevavkqfki3kponh9v FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 4005 (class 2606 OID 17855)
-- Name: resource_attribute fk_5hrm2vlf9ql5fu022kqepovbr; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_attribute
    ADD CONSTRAINT fk_5hrm2vlf9ql5fu022kqepovbr FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);


--
-- TOC entry 3951 (class 2606 OID 16622)
-- Name: user_attribute fk_5hrm2vlf9ql5fu043kqepovbr; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_attribute
    ADD CONSTRAINT fk_5hrm2vlf9ql5fu043kqepovbr FOREIGN KEY (user_id) REFERENCES public.user_entity(id);


--
-- TOC entry 3954 (class 2606 OID 16632)
-- Name: user_required_action fk_6qj3w1jw9cvafhe19bwsiuvmd; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_required_action
    ADD CONSTRAINT fk_6qj3w1jw9cvafhe19bwsiuvmd FOREIGN KEY (user_id) REFERENCES public.user_entity(id);


--
-- TOC entry 3944 (class 2606 OID 16637)
-- Name: keycloak_role fk_6vyqfe4cn4wlq8r6kt5vdsj5c; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.keycloak_role
    ADD CONSTRAINT fk_6vyqfe4cn4wlq8r6kt5vdsj5c FOREIGN KEY (realm) REFERENCES public.realm(id);


--
-- TOC entry 3948 (class 2606 OID 16642)
-- Name: realm_smtp_config fk_70ej8xdxgxd0b9hh6180irr0o; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_smtp_config
    ADD CONSTRAINT fk_70ej8xdxgxd0b9hh6180irr0o FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3945 (class 2606 OID 16657)
-- Name: realm_attribute fk_8shxd6l3e9atqukacxgpffptw; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_attribute
    ADD CONSTRAINT fk_8shxd6l3e9atqukacxgpffptw FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3941 (class 2606 OID 16662)
-- Name: composite_role fk_a63wvekftu8jo1pnj81e7mce2; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.composite_role
    ADD CONSTRAINT fk_a63wvekftu8jo1pnj81e7mce2 FOREIGN KEY (composite) REFERENCES public.keycloak_role(id);


--
-- TOC entry 3972 (class 2606 OID 17098)
-- Name: authentication_execution fk_auth_exec_flow; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.authentication_execution
    ADD CONSTRAINT fk_auth_exec_flow FOREIGN KEY (flow_id) REFERENCES public.authentication_flow(id);


--
-- TOC entry 3973 (class 2606 OID 17093)
-- Name: authentication_execution fk_auth_exec_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.authentication_execution
    ADD CONSTRAINT fk_auth_exec_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3971 (class 2606 OID 17088)
-- Name: authentication_flow fk_auth_flow_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.authentication_flow
    ADD CONSTRAINT fk_auth_flow_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3970 (class 2606 OID 17083)
-- Name: authenticator_config fk_auth_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.authenticator_config
    ADD CONSTRAINT fk_auth_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3955 (class 2606 OID 16672)
-- Name: user_role_mapping fk_c4fqv34p1mbylloxang7b1q3l; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_role_mapping
    ADD CONSTRAINT fk_c4fqv34p1mbylloxang7b1q3l FOREIGN KEY (user_id) REFERENCES public.user_entity(id);


--
-- TOC entry 3982 (class 2606 OID 17761)
-- Name: client_scope_attributes fk_cl_scope_attr_scope; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_scope_attributes
    ADD CONSTRAINT fk_cl_scope_attr_scope FOREIGN KEY (scope_id) REFERENCES public.client_scope(id);


--
-- TOC entry 3983 (class 2606 OID 17751)
-- Name: client_scope_role_mapping fk_cl_scope_rm_scope; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_scope_role_mapping
    ADD CONSTRAINT fk_cl_scope_rm_scope FOREIGN KEY (scope_id) REFERENCES public.client_scope(id);


--
-- TOC entry 3959 (class 2606 OID 17746)
-- Name: protocol_mapper fk_cli_scope_mapper; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.protocol_mapper
    ADD CONSTRAINT fk_cli_scope_mapper FOREIGN KEY (client_scope_id) REFERENCES public.client_scope(id);


--
-- TOC entry 3998 (class 2606 OID 17605)
-- Name: client_initial_access fk_client_init_acc_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.client_initial_access
    ADD CONSTRAINT fk_client_init_acc_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3996 (class 2606 OID 17553)
-- Name: component_config fk_component_config; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.component_config
    ADD CONSTRAINT fk_component_config FOREIGN KEY (component_id) REFERENCES public.component(id);


--
-- TOC entry 3997 (class 2606 OID 17548)
-- Name: component fk_component_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.component
    ADD CONSTRAINT fk_component_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3981 (class 2606 OID 17253)
-- Name: realm_default_groups fk_def_groups_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_default_groups
    ADD CONSTRAINT fk_def_groups_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3976 (class 2606 OID 17113)
-- Name: user_federation_mapper_config fk_fedmapper_cfg; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_federation_mapper_config
    ADD CONSTRAINT fk_fedmapper_cfg FOREIGN KEY (user_federation_mapper_id) REFERENCES public.user_federation_mapper(id);


--
-- TOC entry 3974 (class 2606 OID 17108)
-- Name: user_federation_mapper fk_fedmapperpm_fedprv; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_federation_mapper
    ADD CONSTRAINT fk_fedmapperpm_fedprv FOREIGN KEY (federation_provider_id) REFERENCES public.user_federation_provider(id);


--
-- TOC entry 3975 (class 2606 OID 17103)
-- Name: user_federation_mapper fk_fedmapperpm_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_federation_mapper
    ADD CONSTRAINT fk_fedmapperpm_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3994 (class 2606 OID 17471)
-- Name: associated_policy fk_frsr5s213xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.associated_policy
    ADD CONSTRAINT fk_frsr5s213xcx4wnkog82ssrfy FOREIGN KEY (associated_policy_id) REFERENCES public.resource_server_policy(id);


--
-- TOC entry 3992 (class 2606 OID 17456)
-- Name: scope_policy fk_frsrasp13xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.scope_policy
    ADD CONSTRAINT fk_frsrasp13xcx4wnkog82ssrfy FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);


--
-- TOC entry 4001 (class 2606 OID 17828)
-- Name: resource_server_perm_ticket fk_frsrho213xcx4wnkog82sspmt; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT fk_frsrho213xcx4wnkog82sspmt FOREIGN KEY (resource_server_id) REFERENCES public.resource_server(id);


--
-- TOC entry 3984 (class 2606 OID 17672)
-- Name: resource_server_resource fk_frsrho213xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_resource
    ADD CONSTRAINT fk_frsrho213xcx4wnkog82ssrfy FOREIGN KEY (resource_server_id) REFERENCES public.resource_server(id);


--
-- TOC entry 4002 (class 2606 OID 17833)
-- Name: resource_server_perm_ticket fk_frsrho213xcx4wnkog83sspmt; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT fk_frsrho213xcx4wnkog83sspmt FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);


--
-- TOC entry 4003 (class 2606 OID 17838)
-- Name: resource_server_perm_ticket fk_frsrho213xcx4wnkog84sspmt; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT fk_frsrho213xcx4wnkog84sspmt FOREIGN KEY (scope_id) REFERENCES public.resource_server_scope(id);


--
-- TOC entry 3995 (class 2606 OID 17466)
-- Name: associated_policy fk_frsrpas14xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.associated_policy
    ADD CONSTRAINT fk_frsrpas14xcx4wnkog82ssrfy FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);


--
-- TOC entry 3993 (class 2606 OID 17451)
-- Name: scope_policy fk_frsrpass3xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.scope_policy
    ADD CONSTRAINT fk_frsrpass3xcx4wnkog82ssrfy FOREIGN KEY (scope_id) REFERENCES public.resource_server_scope(id);


--
-- TOC entry 4004 (class 2606 OID 17860)
-- Name: resource_server_perm_ticket fk_frsrpo2128cx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_perm_ticket
    ADD CONSTRAINT fk_frsrpo2128cx4wnkog82ssrfy FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);


--
-- TOC entry 3986 (class 2606 OID 17667)
-- Name: resource_server_policy fk_frsrpo213xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_policy
    ADD CONSTRAINT fk_frsrpo213xcx4wnkog82ssrfy FOREIGN KEY (resource_server_id) REFERENCES public.resource_server(id);


--
-- TOC entry 3988 (class 2606 OID 17421)
-- Name: resource_scope fk_frsrpos13xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_scope
    ADD CONSTRAINT fk_frsrpos13xcx4wnkog82ssrfy FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);


--
-- TOC entry 3990 (class 2606 OID 17436)
-- Name: resource_policy fk_frsrpos53xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_policy
    ADD CONSTRAINT fk_frsrpos53xcx4wnkog82ssrfy FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);


--
-- TOC entry 3991 (class 2606 OID 17441)
-- Name: resource_policy fk_frsrpp213xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_policy
    ADD CONSTRAINT fk_frsrpp213xcx4wnkog82ssrfy FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);


--
-- TOC entry 3989 (class 2606 OID 17426)
-- Name: resource_scope fk_frsrps213xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_scope
    ADD CONSTRAINT fk_frsrps213xcx4wnkog82ssrfy FOREIGN KEY (scope_id) REFERENCES public.resource_server_scope(id);


--
-- TOC entry 3985 (class 2606 OID 17677)
-- Name: resource_server_scope fk_frsrso213xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_server_scope
    ADD CONSTRAINT fk_frsrso213xcx4wnkog82ssrfy FOREIGN KEY (resource_server_id) REFERENCES public.resource_server(id);


--
-- TOC entry 3942 (class 2606 OID 16687)
-- Name: composite_role fk_gr7thllb9lu8q4vqa4524jjy8; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.composite_role
    ADD CONSTRAINT fk_gr7thllb9lu8q4vqa4524jjy8 FOREIGN KEY (child_role) REFERENCES public.keycloak_role(id);


--
-- TOC entry 4000 (class 2606 OID 17803)
-- Name: user_consent_client_scope fk_grntcsnt_clsc_usc; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_consent_client_scope
    ADD CONSTRAINT fk_grntcsnt_clsc_usc FOREIGN KEY (user_consent_id) REFERENCES public.user_consent(id);


--
-- TOC entry 3969 (class 2606 OID 16967)
-- Name: user_consent fk_grntcsnt_user; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_consent
    ADD CONSTRAINT fk_grntcsnt_user FOREIGN KEY (user_id) REFERENCES public.user_entity(id);


--
-- TOC entry 3979 (class 2606 OID 17227)
-- Name: group_attribute fk_group_attribute_group; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.group_attribute
    ADD CONSTRAINT fk_group_attribute_group FOREIGN KEY (group_id) REFERENCES public.keycloak_group(id);


--
-- TOC entry 3978 (class 2606 OID 17241)
-- Name: group_role_mapping fk_group_role_group; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.group_role_mapping
    ADD CONSTRAINT fk_group_role_group FOREIGN KEY (group_id) REFERENCES public.keycloak_group(id);


--
-- TOC entry 3966 (class 2606 OID 16913)
-- Name: realm_enabled_event_types fk_h846o4h0w8epx5nwedrf5y69j; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_enabled_event_types
    ADD CONSTRAINT fk_h846o4h0w8epx5nwedrf5y69j FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3946 (class 2606 OID 16697)
-- Name: realm_events_listeners fk_h846o4h0w8epx5nxev9f5y69j; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_events_listeners
    ADD CONSTRAINT fk_h846o4h0w8epx5nxev9f5y69j FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3967 (class 2606 OID 16957)
-- Name: identity_provider_mapper fk_idpm_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.identity_provider_mapper
    ADD CONSTRAINT fk_idpm_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3968 (class 2606 OID 17127)
-- Name: idp_mapper_config fk_idpmconfig; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.idp_mapper_config
    ADD CONSTRAINT fk_idpmconfig FOREIGN KEY (idp_mapper_id) REFERENCES public.identity_provider_mapper(id);


--
-- TOC entry 3956 (class 2606 OID 16707)
-- Name: web_origins fk_lojpho213xcx4wnkog82ssrfy; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.web_origins
    ADD CONSTRAINT fk_lojpho213xcx4wnkog82ssrfy FOREIGN KEY (client_id) REFERENCES public.client(id);


--
-- TOC entry 3950 (class 2606 OID 16717)
-- Name: scope_mapping fk_ouse064plmlr732lxjcn1q5f1; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.scope_mapping
    ADD CONSTRAINT fk_ouse064plmlr732lxjcn1q5f1 FOREIGN KEY (client_id) REFERENCES public.client(id);


--
-- TOC entry 3960 (class 2606 OID 16852)
-- Name: protocol_mapper fk_pcm_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.protocol_mapper
    ADD CONSTRAINT fk_pcm_realm FOREIGN KEY (client_id) REFERENCES public.client(id);


--
-- TOC entry 3943 (class 2606 OID 16732)
-- Name: credential fk_pfyr0glasqyl0dei3kl69r6v0; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.credential
    ADD CONSTRAINT fk_pfyr0glasqyl0dei3kl69r6v0 FOREIGN KEY (user_id) REFERENCES public.user_entity(id);


--
-- TOC entry 3961 (class 2606 OID 17120)
-- Name: protocol_mapper_config fk_pmconfig; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.protocol_mapper_config
    ADD CONSTRAINT fk_pmconfig FOREIGN KEY (protocol_mapper_id) REFERENCES public.protocol_mapper(id);


--
-- TOC entry 3999 (class 2606 OID 17788)
-- Name: default_client_scope fk_r_def_cli_scope_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.default_client_scope
    ADD CONSTRAINT fk_r_def_cli_scope_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3977 (class 2606 OID 17162)
-- Name: required_action_provider fk_req_act_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.required_action_provider
    ADD CONSTRAINT fk_req_act_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 4006 (class 2606 OID 17868)
-- Name: resource_uris fk_resource_server_uris; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.resource_uris
    ADD CONSTRAINT fk_resource_server_uris FOREIGN KEY (resource_id) REFERENCES public.resource_server_resource(id);


--
-- TOC entry 4007 (class 2606 OID 17882)
-- Name: role_attribute fk_role_attribute_id; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.role_attribute
    ADD CONSTRAINT fk_role_attribute_id FOREIGN KEY (role_id) REFERENCES public.keycloak_role(id);


--
-- TOC entry 3965 (class 2606 OID 16882)
-- Name: realm_supported_locales fk_supported_locales_realm; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.realm_supported_locales
    ADD CONSTRAINT fk_supported_locales_realm FOREIGN KEY (realm_id) REFERENCES public.realm(id);


--
-- TOC entry 3952 (class 2606 OID 16752)
-- Name: user_federation_config fk_t13hpu1j94r2ebpekr39x5eu5; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_federation_config
    ADD CONSTRAINT fk_t13hpu1j94r2ebpekr39x5eu5 FOREIGN KEY (user_federation_provider_id) REFERENCES public.user_federation_provider(id);


--
-- TOC entry 3980 (class 2606 OID 17234)
-- Name: user_group_membership fk_user_group_user; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_group_membership
    ADD CONSTRAINT fk_user_group_user FOREIGN KEY (user_id) REFERENCES public.user_entity(id);


--
-- TOC entry 3987 (class 2606 OID 17411)
-- Name: policy_config fkdc34197cf864c4e43; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.policy_config
    ADD CONSTRAINT fkdc34197cf864c4e43 FOREIGN KEY (policy_id) REFERENCES public.resource_server_policy(id);


--
-- TOC entry 3964 (class 2606 OID 16862)
-- Name: identity_provider_config fkdc4897cf864c4e43; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.identity_provider_config
    ADD CONSTRAINT fkdc4897cf864c4e43 FOREIGN KEY (identity_provider_id) REFERENCES public.identity_provider(internal_id);


-- Completed on 2025-08-04 14:28:41

--
-- PostgreSQL database dump complete
--

--
-- Database "postgres" dump
--

\connect postgres

--
-- PostgreSQL database dump
--

-- Dumped from database version 17.5
-- Dumped by pg_dump version 17.5

-- Started on 2025-08-04 14:28:41

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- Completed on 2025-08-04 14:28:41

--
-- PostgreSQL database dump complete
--

--
-- Database "relma" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 17.5
-- Dumped by pg_dump version 17.5

-- Started on 2025-08-04 14:28:41

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3362 (class 1262 OID 18037)
-- Name: relma; Type: DATABASE; Schema: -; Owner: admin
--

CREATE DATABASE relma WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.UTF-8';


ALTER DATABASE relma OWNER TO admin;

\connect relma

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 18038)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MIGRATION_ID" character varying(150) NOT NULL,
    "PRODUCT_VERSION" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO admin;

--
-- TOC entry 3356 (class 0 OID 18038)
-- Dependencies: 217
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public."__EFMigrationsHistory" ("MIGRATION_ID", "PRODUCT_VERSION") FROM stdin;
\.


--
-- TOC entry 3210 (class 2606 OID 18042)
-- Name: __EFMigrationsHistory PK___EF_MIGRATIONS_HISTORY; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EF_MIGRATIONS_HISTORY" PRIMARY KEY ("MIGRATION_ID");


-- Completed on 2025-08-04 14:28:42

--
-- PostgreSQL database dump complete
--

-- Completed on 2025-08-04 14:28:42

--
-- PostgreSQL database cluster dump complete
--

