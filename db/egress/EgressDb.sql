CREATE DATABASE EgressDb;
USE EgressDb;

CREATE TABLE Person (
    id UNIQUEIDENTIFIER NOT NULL,
	cpf VARCHAR(11) NOT NULL,
	name VARCHAR(200) NOT NULL,
	birth_date DATETIME2,
	sex TINYINT,
	email VARCHAR(80),
	phone_number VARCHAR(45),
	perfil_image VARCHAR(200),
	expose_data BIT NOT NULL,
	person_type TINYINT NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT person_pk PRIMARY KEY (id)
);

CREATE TABLE Specialization (
	id UNIQUEIDENTIFIER NOT NULL,
	course_name VARCHAR(100) NOT NULL,
	institution_name VARCHAR(100) NOT NULL,
	status BIT NOT NULL,
	modality TINYINT NOT NULL,
	start_date DATETIME2 NOT NULL,
	end_date DATETIME2 NOT NULL,
	person_id UNIQUEIDENTIFIER NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT specialization_pk PRIMARY KEY (id)
);

CREATE TABLE Address (
	id UNIQUEIDENTIFIER NOT NULL,
	street VARCHAR(45),
	district VARCHAR(45),
	city VARCHAR(45) NOT NULL,
	state VARCHAR(45) NOT NULL,
	country VARCHAR(45) NOT NULL,
	person_id UNIQUEIDENTIFIER NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT address_pk PRIMARY KEY (id)
);

CREATE TABLE Course (
	id UNIQUEIDENTIFIER NOT NULL,
	course_name VARCHAR(80) NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT course_pk PRIMARY KEY (id)
);

CREATE TABLE PersonCourse (
	id UNIQUEIDENTIFIER NOT NULL,
	start_date DATETIME2 NOT NULL,
	end_date DATETIME2,
	mat VARCHAR(20) NOT NULL,
	level TINYINT NOT NULL,
	modality TINYINT NOT NULL,
	person_id UNIQUEIDENTIFIER NOT NULL,
	course_id UNIQUEIDENTIFIER NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT personcourse_pk PRIMARY KEY (id)
);

CREATE TABLE Highlights (
	id UNIQUEIDENTIFIER NOT NULL,
	title VARCHAR(200) NOT NULL,
	description TEXT NOT NULL,
	link VARCHAR(200),
	person_id UNIQUEIDENTIFIER NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT highlights_pk PRIMARY KEY (id)
);

CREATE TABLE Testimony (
	id UNIQUEIDENTIFIER NOT NULL,
	content TEXT NOT NULL,
	was_accepted BIT NOT NULL,
	person_id UNIQUEIDENTIFIER NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT testimony_pk PRIMARY KEY (id)
);

CREATE TABLE Employment (
	id UNIQUEIDENTIFIER NOT NULL,
	role VARCHAR(150) NOT NULL,
	enterprise VARCHAR(80) NOT NULL,
	section VARCHAR(45) NOT NULL,
	salary_range DECIMAL,
	initiative TINYINT NOT NULL,
	status BIT NOT NULL,
	start_date DATETIME2,
	end_date DATETIME2,
	person_id UNIQUEIDENTIFIER NOT NULL,
	created_at DATETIME2 NOT NULL,
	updated_at DATETIME2 NOT NULL,
	CONSTRAINT employment_pk PRIMARY KEY (id)
);

ALTER TABLE Specialization
ADD CONSTRAINT fk_specialization_person
FOREIGN KEY (person_id) REFERENCES Person(id);

ALTER TABLE Address
ADD CONSTRAINT fk_address_person
FOREIGN KEY (person_id) REFERENCES Person(id);

ALTER TABLE Address
ADD CONSTRAINT uc_address_person_id
UNIQUE (person_id);

ALTER TABLE Highlights
ADD CONSTRAINT fk_highlights_person
FOREIGN KEY (person_id) REFERENCES Person(id);

ALTER TABLE Testimony
ADD CONSTRAINT fk_testimony_person
FOREIGN KEY (person_id) REFERENCES Person(id);

ALTER TABLE Employment
ADD CONSTRAINT fk_employment_person
FOREIGN KEY (person_id) REFERENCES Person(id);

ALTER TABLE PersonCourse
ADD CONSTRAINT fk_personcourse_person
FOREIGN KEY (person_id) REFERENCES Person(id);

ALTER TABLE PersonCourse
ADD CONSTRAINT fk_personcourse_course
FOREIGN KEY (course_id) REFERENCES Course(id);


INSERT INTO Person (id, cpf, name, birth_date, sex, email, phone_number, expose_data, person_type, created_at, updated_at) VALUES
('4df17a6b-3097-47b0-b934-f9c5d906c581', '12365498798', 'Ellison', convert(datetime2,'18-06-12 10:34:09 PM',5), 0, 'ellison.guimaraes@gmail.com', '73988991122', 1, 1, convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('dcbe59a8-d650-4bee-8bf1-497b902b749f', '11122233343', 'Rebeca', convert(datetime2,'18-06-12 10:34:09 PM',5), 1, 'rebeca@gmail.com', '11988991122', 1, 0, convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('c174ad75-602a-4034-92ea-474b736069a9', '77788899909', 'Matheus', convert(datetime2,'18-06-12 10:34:09 PM',5), 0, 'matheus@gmail.com', '32988991122', 0, 1, convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('50c01544-9f73-49ca-9b0c-e218a0e6517a', '1028510632', 'Tybie', '7/4/2022', 1, 'triden0@state.tx.us', '9495569809', 1, 0, '9/30/2022', '7/31/2022'),
('f48e71af-0a39-4bfd-882f-0f39bc27972e', '4471968424', 'Kellie', '4/16/2022', 1, 'kiacobucci1@sun.com', '4526741058', 1, 1, '9/21/2022', '7/19/2022'),
('95e13a6a-fab6-4e29-8b42-abcb6da797a5', '9763844851', 'Josh', '5/10/2022', 1, 'jchattoe2@shareasale.com', '3271456859', 1, 1, '7/30/2022', '10/6/2022'),
('446187f6-e006-4145-bdc6-e6c8bff42c50', '4134038189', 'Gnni', '2/25/2022', 1, 'ggraveston3@google.de', '6289606954', 0, 2, '9/14/2022', '2/27/2022'),
('fc31541c-1560-46e4-8a21-433b42d08a6b', '9228344296', 'Emilio', '10/28/2022', 0, 'etodarini4@vinaora.com', '1892576360', 1, 0, '9/23/2022', '5/13/2022'),
('152d0fea-5b2a-4255-b66b-f31053327124', '9710496085', 'Boone', '5/8/2022', 1, 'bmatteucci5@so-net.ne.jp', '7882035233', 0, 0, '11/26/2022', '11/1/2022'),
('b2b793e6-6381-488a-97e5-3a8667ea512b', '8908798935', 'Quint', '5/3/2022', 1, 'qyellowlee6@phpbb.com', '9551575606', 0, 1, '5/11/2022', '5/29/2022'),
('4bd26624-1142-4dc7-8dfe-86b92083c1c5', '6340311806', 'Trula', '2/10/2023', 1, 'tchidzoy7@weebly.com', '3815906070', 1, 2, '5/1/2022', '7/17/2022'),
('d500b13c-4c64-4f55-bf6a-421ca8cd60e9', '1335871063', 'Geri', '2/3/2023', 1, 'glangeley8@wikispaces.com', '9825708786', 1, 2, '11/6/2022', '1/3/2023'),
('f8b9d49a-4531-490d-8e7f-065c8ded1daf', '2933248557', 'Lindon', '5/14/2022', 0, 'lstoodale9@wiley.com', '8002745378', 0, 2, '12/18/2022', '6/5/2022'),
('92773c95-cde0-4b49-8fc1-47a811b5ee6f', '8293627387', 'Bryan', '2/13/2023', 1, 'bwigginsa@cocolog-nifty.com', '9883827032', 0, 1, '2/15/2023', '9/4/2022'),
('ca44d035-025d-4398-848b-3344eb76a881', '2292940345', 'Deonne', '8/31/2022', 0, 'dsikorab@omniture.com', '6286668913', 1, 0, '11/21/2022', '3/24/2022'),
('fc5a6fc1-ea2a-4878-a62d-da2f9592e585', '3331154989', 'Adi', '9/6/2022', 0, 'atrevesc@behance.net', '3657034597', 1, 1, '6/26/2022', '4/4/2022'),
('fbe81873-d27c-4115-a5be-5630398dc627', '4077758645', 'Rourke', '1/8/2023', 1, 'rcohaned@japanpost.jp', '1264262870', 1, 1, '5/11/2022', '9/19/2022'),
('88c82241-c008-45d1-b078-8989919b4c95', '3367315826', 'Daron', '11/6/2022', 0, 'dpollinse@squarespace.com', '7443730153', 0, 0, '10/15/2022', '12/1/2022'),
('ec7d07d2-cf07-4755-911c-93657f9f0190', '5853708449', 'Margalit', '9/12/2022', 0, 'mjozwiakf@ocn.ne.jp', '4147445010', 0, 2, '7/2/2022', '1/4/2023'),
('8b9a8716-df67-4123-99a2-5df88273560b', '1988305500', 'Griff', '2/10/2023', 1, 'gquinceeg@istockphoto.com', '9812177801', 0, 2, '3/16/2022', '3/4/2022'),
('e0cbbce0-bb97-4e06-9f90-7087edaff458', '9812122931', 'Garald', '10/29/2022', 0, 'gwinscumh@slideshare.net', '5529627535', 0, 0, '3/15/2022', '2/17/2023'),
('0423f87e-8f70-4f6e-9cec-69a95b655da1', '7437087812', 'Mil', '10/3/2022', 0, 'mmcgregori@csmonitor.com', '6613987847', 1, 1, '6/24/2022', '12/8/2022'),
('d28dc6d9-7767-4b8b-bbfb-ebd0017fc32d', '2317597568', 'Filbert', '10/17/2022', 0, 'flemasneyj@irs.gov', '7931745407', 0, 2, '6/18/2022', '1/31/2023'),
('a8775d6e-4a36-4e90-b4d0-1b4c5c5cf097', '4219769366', 'Rozanna', '12/15/2022', 0, 'rcorringhamk@mit.edu', '8198736746', 1, 2, '5/4/2022', '11/28/2022'),
('cd412b04-06b4-4a13-83cf-f941a91e01d2', '0783950209', 'Donica', '10/24/2022', 0, 'dmcartl@1688.com', '8313916804', 1, 0, '3/11/2022', '5/4/2022'),
('95e3b361-cdc3-4a2e-b58c-1b1cafd1a139', '9984579492', 'Shelby', '4/27/2022', 1, 'srobertuccim@amazon.co.jp', '2088252684', 0, 1, '7/19/2022', '3/8/2022'),
('216e73b6-fa4e-41c4-8025-8bc0ddeddd88', '8853031379', 'Barde', '4/1/2022', 0, 'bleonn@ft.com', '2436567826', 0, 1, '9/1/2022', '2/9/2023'),
('9c1d60b8-aa51-4693-b055-a194fed01c7a', '0773793046', 'Magdalen', '12/4/2022', 0, 'mheiblo@phoca.cz', '3559459046', 0, 0, '7/10/2022', '3/16/2022'),
('def399df-6eb3-466b-bb0b-831baee733e1', '9253089245', 'Donovan', '11/21/2022', 1, 'drichardetp@ca.gov', '6763521903', 1, 1, '8/2/2022', '4/10/2022'),
('0b496a54-bdbf-4042-baf4-95a8161f1a86', '2898066737', 'Gordie', '11/16/2022', 1, 'glucienq@alexa.com', '2523749069', 1, 1, '11/14/2022', '12/17/2022'),
('62c10eaa-9afc-48c2-99a0-bd9709e6705e', '1209363860', 'Steven', '11/16/2022', 1, 'sgreener@1688.com', '9367864940', 1, 2, '3/27/2022', '6/8/2022'),
('95fa81e7-12dd-4da4-ba26-d3289339c06f', '4606372141', 'Sky', '12/14/2022', 1, 'sstacks@economist.com', '5439607627', 0, 1, '12/24/2022', '11/1/2022'),
('098bceba-6195-4905-b177-8a9910661383', '7213546090', 'Ranique', '1/20/2023', 1, 'rmcguinesst@ox.ac.uk', '6446880727', 0, 1, '1/12/2023', '7/13/2022'),
('eeaeef95-52de-4e5c-a028-71a2a50f02c5', '2771488639', 'Boony', '3/12/2022', 0, 'bschollingu@drupal.org', '7498342081', 0, 0, '10/13/2022', '10/11/2022'),
('a2ecaa9d-162e-49f7-9da5-1b232acd7015', '5599993140', 'Newton', '11/24/2022', 1, 'nassenderv@answers.com', '9952844098', 0, 1, '12/25/2022', '8/27/2022'),
('739a9c91-8898-485d-aa6e-f25308284190', '6573599403', 'Hermann', '2/5/2023', 1, 'hricciardiw@google.com.hk', '2271904782', 0, 0, '5/1/2022', '11/26/2022'),
('e1718013-7bfd-4916-8d4d-2e757185642d', '2188604261', 'Milli', '6/11/2022', 0, 'mbateupx@rediff.com', '6185043482', 0, 1, '10/30/2022', '8/24/2022'),
('b9d3bcef-32b5-4747-b018-02c56e686cb3', '7867525718', 'Augustine', '10/9/2022', 0, 'anoddley@delicious.com', '7784830857', 0, 2, '1/23/2023', '1/21/2023'),
('37d641e0-bec4-49bb-9c00-8c796a06534d', '6499733393', 'Tito', '7/24/2022', 1, 'tsnasdellz@washingtonpost.com', '8133082188', 1, 1, '4/4/2022', '8/30/2022'),
('68e3d1a2-0142-4dd1-a68b-ee6c6757e787', '2354427816', 'Jaine', '8/14/2022', 1, 'jkaesmans10@msn.com', '7753427209', 0, 0, '10/27/2022', '2/27/2022'),
('7b4b8a6e-ab3e-423d-8038-b37b3e083041', '7033825722', 'Marina', '11/6/2022', 1, 'mranscombe11@paginegialle.it', '4767322697', 1, 1, '9/23/2022', '12/28/2022'),
('fa43e057-9fbd-49cf-9917-4f7658c67f6f', '5249713564', 'Marj', '4/7/2022', 0, 'mbrookton12@amazon.co.jp', '9586662817', 1, 2, '6/19/2022', '10/3/2022'),
('8dec7182-41d0-4041-8e99-23eb90be9887', '5353947886', 'Sol', '9/23/2022', 1, 'shandasyde13@tuttocitta.it', '5718240534', 0, 2, '1/16/2023', '1/9/2023'),
('889fc1c6-98f8-4a84-bb29-f156c2b40433', '7731862259', 'Raynor', '10/16/2022', 0, 'rkerfoot14@geocities.com', '4039042469', 1, 2, '7/30/2022', '7/18/2022'),
('99edc838-2a0d-4891-88f6-16f86f6cb268', '6354227128', 'Penni', '2/22/2023', 1, 'prennebach15@vinaora.com', '3145114196', 0, 0, '9/23/2022', '6/26/2022'),
('cf6e3569-32fd-4d97-93a8-caa8ac2a25e7', '6789890068', 'Rey', '11/8/2022', 1, 'rcassius16@google.it', '4218865830', 0, 0, '6/18/2022', '12/3/2022'),
('412e3504-bc47-4494-9832-822332552648', '3322503437', 'Cindie', '10/20/2022', 1, 'cpatten17@infoseek.co.jp', '4639881469', 1, 2, '3/11/2022', '11/12/2022'),
('6817e26e-ecd7-4130-9939-9596d5f4d276', '3470708118', 'Gifford', '1/2/2023', 0, 'grubel18@arizona.edu', '9904073784', 1, 1, '7/2/2022', '12/24/2022'),
('ca092dda-b03d-4df3-a42b-3f58c4183512', '3591648841', 'Shela', '3/15/2022', 1, 'sveart19@lycos.com', '1096520583', 0, 2, '11/19/2022', '4/16/2022'),
('0e22786d-6a9f-4467-8d62-71627f6be550', '8230898332', 'Aldon', '6/16/2022', 0, 'alafee1a@tripadvisor.com', '5712353799', 1, 2, '12/19/2022', '6/3/2022'),
('e4ca701f-29a6-46ec-8f91-fc8cdaeeb97b', '6061294468', 'Zabrina', '12/5/2022', 1, 'zhaldenby1b@google.cn', '6307138254', 0, 0, '11/14/2022', '5/18/2022'),
('bf848313-71d2-44d1-b74e-52962522be07', '9819988144', 'Lyndsay', '1/13/2023', 0, 'llawland1c@webmd.com', '1125561199', 1, 1, '1/22/2023', '10/6/2022'),
('e05ce478-a79d-4218-a72a-ffaa360b9c54', '8258083805', 'Glad', '9/15/2022', 1, 'gnoni1d@plala.or.jp', '2281734562', 0, 2, '6/30/2022', '9/5/2022'),
('9acd71b5-e6b4-412e-a55a-e5d36b0a579b', '6172322567', 'Michail', '4/25/2022', 0, 'mrushton1e@bing.com', '8348395162', 0, 2, '11/21/2022', '7/3/2022'),
('5346812b-92f6-45d1-b6cd-c0133ebdb521', '3310828093', 'Nestor', '6/8/2022', 1, 'nhusthwaite1f@google.de', '4302388409', 1, 2, '5/28/2022', '5/24/2022'),
('2ec25f79-c46d-4581-91bb-c9fd72a49cdf', '3633904484', 'Aland', '4/28/2022', 1, 'amchaffy1g@amazon.com', '2185699024', 0, 0, '6/1/2022', '6/11/2022'),
('6f825eb0-1d82-41dd-bc4a-60c0216dfb50', '9934849151', 'Katerina', '7/30/2022', 1, 'kscipsey1h@yale.edu', '9595325519', 1, 0, '10/11/2022', '9/9/2022'),
('a351b304-25b4-4acf-abab-7fd9f4c266b8', '1810841798', 'Dasi', '8/3/2022', 0, 'ddelort1i@studiopress.com', '4587162393', 0, 1, '5/16/2022', '1/16/2023'),
('aee05993-46cb-4ad1-bc0a-c17b0a8e594c', '1310668973', 'Lester', '7/20/2022', 1, 'lbhatia1j@domainmarket.com', '2214617664', 0, 2, '3/31/2022', '1/28/2023'),
('84c08bc4-a972-4a44-ac9c-facd071adb84', '1974587398', 'Zsazsa', '1/27/2023', 0, 'zmckelvey1k@ycombinator.com', '7673745279', 1, 1, '6/25/2022', '7/2/2022'),
('e133423c-e9ce-4e31-b6c5-26401d2f2e92', '6056972216', 'Clo', '7/30/2022', 1, 'cerrol1l@ibm.com', '8414537794', 0, 2, '12/4/2022', '1/19/2023'),
('96b38f8c-7a4f-47b1-960f-043252ea7e2b', '7015637162', 'Dimitri', '1/29/2023', 0, 'dtomblett1m@indiatimes.com', '7238528748', 1, 1, '4/15/2022', '5/17/2022'),
('00f03293-9cf7-4e66-9945-2c84eea41c7e', '8450325927', 'Ramona', '8/23/2022', 0, 'rkmietsch1n@army.mil', '1894323340', 0, 0, '4/13/2022', '3/18/2022'),
('387787e4-7dea-4f86-8bbe-eef1a6b00598', '9304921252', 'Edmund', '12/17/2022', 0, 'elyal1o@naver.com', '6204281234', 1, 2, '9/22/2022', '8/15/2022'),
('51b46a15-05c8-4eb8-87f3-4dc8fd043f31', '6739058695', 'Fonz', '7/11/2022', 0, 'fvanyatin1p@google.com', '3265860147', 0, 1, '10/9/2022', '3/17/2022'),
('89d4f4aa-54af-41c3-98ea-25e5f380815b', '1066961964', 'Edgar', '5/8/2022', 1, 'ebeldum1q@fastcompany.com', '2849343913', 1, 0, '12/12/2022', '8/8/2022'),
('730c90ab-f083-4854-824b-dccd6daf6452', '0584261217', 'Mariann', '2/15/2023', 0, 'mbrauns1r@about.me', '2642779575', 1, 1, '3/18/2022', '9/4/2022'),
('3c8c7019-45ab-471c-893f-effd65c87686', '8099199723', 'Gil', '8/21/2022', 0, 'gmcmackin1s@europa.eu', '1837154798', 0, 2, '3/17/2022', '5/31/2022'),
('c272f338-e673-4a90-bab0-2f282966228a', '4591401537', 'Ralina', '12/13/2022', 0, 'rlippitt1t@tamu.edu', '7624073595', 1, 2, '4/11/2022', '10/4/2022'),
('5a76ce41-1283-47c7-9cdd-a688ca89dce6', '2419286960', 'Trixie', '6/30/2022', 0, 'tbehagg1u@miibeian.gov.cn', '7575395082', 0, 0, '10/13/2022', '5/10/2022'),
('f8c816c6-f456-41a8-b7f0-eae63ddb2313', '9098958869', 'Quincey', '12/8/2022', 0, 'qcritten1v@tiny.cc', '9313770458', 1, 1, '12/31/2022', '10/2/2022'),
('52d618b6-4689-44c3-bfeb-6c32d4fe67dd', '8980204280', 'Crin', '1/9/2023', 1, 'charwood1w@telegraph.co.uk', '7647074336', 1, 0, '1/22/2023', '10/28/2022'),
('8877301c-3f70-4ad9-ac36-92ba0a47e806', '8588322021', 'Maurise', '5/14/2022', 1, 'mhayley1x@yellowbook.com', '9385660280', 1, 2, '6/30/2022', '10/26/2022'),
('a960e8b8-a0fe-4b5f-9fb2-0256043c1071', '5274616542', 'Heddie', '7/12/2022', 1, 'hsommerfeld1y@prweb.com', '5118434617', 0, 2, '10/10/2022', '2/3/2023'),
('8794d0bf-195f-4d08-a89e-9cab22c795f0', '2976839743', 'Donnie', '11/27/2022', 0, 'dmonketon1z@wp.com', '1176606318', 1, 0, '4/26/2022', '1/18/2023'),
('59dd701f-94f1-43c3-991f-abda8563ff0f', '8596067442', 'Winfred', '5/28/2022', 0, 'whenricsson20@twitpic.com', '4383317389', 0, 0, '8/10/2022', '3/7/2022'),
('c6e829c4-4921-4a19-84a9-b95714f9f6d7', '7134494942', 'Tommie', '5/14/2022', 0, 'tferrie21@toplist.cz', '2257671035', 1, 2, '7/30/2022', '7/16/2022'),
('fb844251-171d-423a-af4a-4cf80e6941e0', '7848028070', 'Giraldo', '10/4/2022', 1, 'gmiskelly22@thetimes.co.uk', '6377363339', 1, 1, '12/14/2022', '6/25/2022'),
('357c52fc-9ab0-4c28-bf42-258985d52b2e', '4059471879', 'Valerye', '4/22/2022', 0, 'vsolland23@jimdo.com', '1769554951', 0, 0, '11/13/2022', '3/31/2022'),
('0344f0d7-3445-4906-865d-5a0654183539', '2463381205', 'Haydon', '3/31/2022', 0, 'hfenkel24@hatena.ne.jp', '1993295659', 0, 0, '9/10/2022', '10/27/2022'),
('934f4205-d197-4d45-959f-c013b8f55479', '0673508978', 'Jacklin', '11/26/2022', 0, 'jfields25@vinaora.com', '2182057390', 1, 2, '1/24/2023', '7/6/2022'),
('982c2f91-6a06-4e8a-9e54-a0c46b53864a', '5209820157', 'Cissy', '6/11/2022', 1, 'chastelow26@de.vu', '7969319333', 1, 2, '4/28/2022', '9/12/2022'),
('7111fa6f-7ffb-4ee9-a487-26b7c5458894', '6275193808', 'Weston', '8/11/2022', 0, 'wsmoth27@amazon.com', '7613898101', 0, 2, '3/2/2022', '6/7/2022'),
('3079256b-5e2b-4518-b72b-e66137f8687e', '3544538334', 'Taylor', '7/5/2022', 0, 'tgyer28@imdb.com', '8998590193', 0, 0, '4/29/2022', '6/11/2022'),
('6d66853b-2779-4126-9b83-11ca80f8dd4b', '9344054320', 'Ebenezer', '10/24/2022', 1, 'epifford29@liveinternet.ru', '3648302053', 0, 1, '4/9/2022', '6/9/2022'),
('d2d05858-1340-4aa3-8942-74b13f131944', '1158396503', 'Kylie', '8/31/2022', 1, 'kbetteriss2a@statcounter.com', '5744937923', 0, 1, '6/7/2022', '2/26/2022'),
('300d0dc8-3ae0-4e0e-8ad0-6793886b5a56', '0106010298', 'Gaston', '7/8/2022', 0, 'gsedgeworth2b@cmu.edu', '6833217415', 0, 1, '10/3/2022', '9/14/2022'),
('b8ea45ca-5606-492c-8dea-6b8c815ddf89', '4363058188', 'Cale', '10/21/2022', 0, 'cmeese2c@furl.net', '1225729498', 1, 0, '3/3/2022', '10/31/2022'),
('6e02c818-943a-4b5b-a829-cbb8e94ef009', '5597844261', 'Winnie', '12/27/2022', 0, 'whabbal2d@deviantart.com', '1886660795', 0, 0, '9/28/2022', '3/15/2022'),
('9f32f817-d117-4515-95b1-ceb96ee407ab', '3227260484', 'Tamas', '8/1/2022', 1, 'tstelljes2e@xrea.com', '8319084276', 0, 2, '6/5/2022', '6/2/2022'),
('6670bce4-c0ea-412a-ac35-86b1a08c1968', '5690795771', 'Bartholemy', '9/17/2022', 1, 'blindermann2f@google.es', '5722394055', 1, 1, '9/18/2022', '12/17/2022'),
('7d6a0679-04be-454c-b4ba-d7391c5b9db5', '2515065767', 'Joel', '6/2/2022', 0, 'jblofeld2g@usgs.gov', '9045501373', 0, 2, '2/3/2023', '3/25/2022'),
('98a5c710-755b-4f53-b644-59ff79c784f0', '3113781027', 'Rhoda', '9/16/2022', 0, 'rdavioud2h@baidu.com', '9149753581', 1, 0, '11/28/2022', '11/28/2022'),
('c7d6519a-b3db-420c-be32-0ad82d8fbb04', '3167458445', 'Janek', '3/1/2022', 0, 'jsange2i@posterous.com', '5549484281', 1, 0, '8/3/2022', '4/4/2022'),
('db7d04fa-16f2-4987-aed2-7fcbd87a979c', '6342228353', 'Morrie', '12/22/2022', 1, 'mbagshaw2j@networkadvertising.org', '7252085538', 0, 1, '5/27/2022', '2/25/2022'),
('e6a7ec72-9934-4b59-907c-8924997c824f', '3243693810', 'Lucy', '9/16/2022', 1, 'lhainsworth2k@hc360.com', '8884970845', 1, 2, '5/21/2022', '10/6/2022'),
('b9e3c235-ced7-40fc-af56-e024e7df4b66', '8234604406', 'Boy', '6/25/2022', 0, 'bjellyman2l@washington.edu', '5969002997', 0, 2, '7/24/2022', '9/8/2022'),
('b88d216c-4bf4-4ea4-b1c7-072cccbe6019', '0884142051', 'Rutger', '2/11/2023', 1, 'rboldry2m@mlb.com', '4937575749', 0, 0, '8/26/2022', '11/20/2022'),
('b7b95380-9543-4d1e-b6f9-15108edd95c9', '4372612176', 'Devin', '3/9/2022', 1, 'dpostians2n@go.com', '8199523333', 1, 0, '3/21/2022', '1/20/2023'),
('8b448d64-0eee-4890-8fd4-bee81dc7e6b0', '7143958549', 'Tamar', '8/20/2022', 1, 'toffell2o@wikispaces.com', '9791657883', 0, 2, '5/18/2022', '9/23/2022'),
('d68a4098-9527-4c10-923a-7ff9643a53c0', '6833818339', 'Dotty', '4/11/2022', 0, 'dpavic2p@marriott.com', '6079196618', 0, 0, '10/16/2022', '6/14/2022'),
('b9ee5bcd-a473-4a8d-b7ea-da4a855bc869', '2010590147', 'Frank', '7/20/2022', 0, 'fspiller2q@buzzfeed.com', '5157675077', 0, 2, '6/14/2022', '9/4/2022'),
('5ab762c5-c57f-4e67-b08f-a7a86e6a24c5', '2005298840', 'Jennica', '7/16/2022', 0, 'jfrankland2r@hibu.com', '6318278823', 1, 2, '2/2/2023', '3/12/2022'),
('052fe84a-eef0-4e06-a396-87b3d0023038', '2868416616', 'Clerkclaude', '5/22/2022', 1, 'castie2s@cnbc.com', '9447391687', 0, 0, '1/6/2023', '7/5/2022'),
('f5ec4381-b31e-4a99-b24f-8d9aa5f688d6', '7200114162', 'Caitrin', '5/20/2022', 0, 'cgatrill2t@odnoklassniki.ru', '8263500338', 0, 1, '12/25/2022', '8/14/2022'),
('c69e21a3-6a05-4f75-b0aa-932a94e98700', '5318666248', 'Florri', '5/12/2022', 1, 'fseabright2u@mac.com', '6886308137', 0, 1, '5/18/2022', '5/9/2022'),
('75df07c9-2e9b-42e1-a4bf-2bc2d3942f89', '7282844526', 'Fabe', '1/28/2023', 0, 'fschuelcke2v@stanford.edu', '4738126486', 0, 1, '11/3/2022', '6/19/2022'),
('d705ce21-cb42-4bce-8f8a-a7324c9d005f', '6705823410', 'Holly', '2/26/2022', 1, 'hfauguel2w@tmall.com', '3943464923', 0, 1, '7/3/2022', '10/24/2022'),
('2ad8f777-b85f-461b-abf0-58c161a27bcb', '0241121965', 'Kevina', '9/26/2022', 1, 'kascrofte2x@walmart.com', '7484703515', 1, 0, '3/24/2022', '5/23/2022'),
('4420a6c3-9113-4580-bf27-b7f61b88a876', '4642561536', 'Fredra', '5/2/2022', 1, 'fkinloch2y@jiathis.com', '5435537548', 0, 1, '6/10/2022', '7/12/2022'),
('62b25d49-dece-4e1c-972a-738b7c7000d4', '0326978909', 'Joelynn', '9/8/2022', 1, 'jtoe2z@sakura.ne.jp', '3268364253', 1, 1, '12/11/2022', '2/10/2023'),
('9cf616a5-fb36-42bd-bb7d-61f2865e8ef4', '5829866250', 'Consalve', '11/14/2022', 1, 'cmaccook30@craigslist.org', '4968885322', 1, 1, '12/16/2022', '8/30/2022'),
('4728fff2-0398-48ad-9e4b-b4997dcd4888', '4747373314', 'Christiana', '7/21/2022', 1, 'cdelaharpe31@epa.gov', '5002765005', 0, 2, '8/5/2022', '6/24/2022'),
('db87a64d-1183-4ac4-809c-cb9f9140394f', '1031326804', 'Kelly', '8/31/2022', 1, 'kcolborn32@technorati.com', '6458884061', 1, 1, '5/25/2022', '1/9/2023'),
('7cf88a5a-926c-4dfb-9da4-a011edf8841e', '1484990129', 'Jorrie', '8/1/2022', 0, 'jdymick33@senate.gov', '7323465449', 0, 1, '9/27/2022', '10/21/2022'),
('7741c75a-4bd1-4125-a257-86a9256516bf', '5496911443', 'Pierce', '1/16/2023', 0, 'pmilmith34@ca.gov', '9691035094', 0, 1, '12/23/2022', '11/18/2022'),
('23546a50-980d-4315-a06a-a4b529d6f990', '5627050578', 'Brinn', '7/27/2022', 0, 'btirte35@wikispaces.com', '8192684728', 1, 1, '9/12/2022', '7/3/2022'),
('0028e2c6-d2d4-416c-878d-55d4608aa4c8', '3533210081', 'Lew', '2/11/2023', 0, 'liglesia36@nps.gov', '6262922678', 1, 1, '7/27/2022', '7/9/2022'),
('e59ba45b-d0e4-434a-9a47-d1b679e02019', '6259803451', 'Tandy', '3/19/2022', 0, 'tnell37@elegantthemes.com', '1638374334', 1, 0, '3/15/2022', '3/26/2022'),
('98db21df-307d-4b0c-8f20-d7e1fa0fab1d', '5416778857', 'Grethel', '5/31/2022', 1, 'gseathwright38@soup.io', '3117141012', 1, 0, '11/19/2022', '8/31/2022'),
('db334b93-f9e1-468a-898d-a01cdff69e7a', '9706056645', 'Elga', '8/10/2022', 0, 'emartelet39@blog.com', '5856561792', 1, 0, '9/26/2022', '2/19/2023'),
('efcab5a8-c245-4916-9345-e9d6b130609c', '5697288581', 'Hans', '5/19/2022', 1, 'hmacilurick3a@columbia.edu', '7312123340', 0, 2, '5/24/2022', '4/16/2022'),
('fe616ae9-6028-47c5-a4f9-5cb0bfea7310', '5658906938', 'Aile', '5/18/2022', 1, 'amcowan3b@feedburner.com', '6278617580', 1, 2, '11/15/2022', '7/4/2022'),
('1a1aaffa-df26-45a6-8db2-b9c7617d8ae0', '8418569352', 'Gwendolyn', '5/28/2022', 0, 'gclemendet3c@youku.com', '2029029520', 0, 0, '4/17/2022', '2/22/2023'),
('2ad9d639-260b-4719-845d-734f43b6d281', '8236658066', 'Royce', '1/26/2023', 0, 'rverma3d@upenn.edu', '8247138478', 1, 1, '6/17/2022', '7/6/2022'),
('95942504-b05d-409e-b4d3-5a5144907f4f', '1155329813', 'Rasla', '10/30/2022', 0, 'rburgett3e@nationalgeographic.com', '6023264060', 0, 2, '12/14/2022', '7/30/2022'),
('2e8d3e31-9a52-4c0f-9e52-50a29ff9ab25', '9231385690', 'Karla', '3/23/2022', 0, 'klaurence3f@soundcloud.com', '1245704662', 0, 2, '10/9/2022', '11/2/2022'),
('fbc94270-985e-40cf-94ed-01772fd07827', '2412588287', 'Maggee', '7/25/2022', 1, 'mnorthcott3g@globo.com', '1415636559', 0, 2, '8/9/2022', '10/8/2022'),
('b5934da5-2681-4fa8-84f5-52191c2c3f7f', '4491350787', 'Gracia', '10/7/2022', 1, 'ggatsby3h@bing.com', '1007191991', 0, 2, '4/16/2022', '4/15/2022'),
('f17f1ed3-0260-488c-b05a-0f7c91060d38', '8150939369', 'Martyn', '12/9/2022', 1, 'mcorke3i@qq.com', '4873178991', 1, 1, '9/27/2022', '12/26/2022'),
('d6d48952-74a0-464b-ac3f-ec6982a83a55', '0348911300', 'Wesley', '1/21/2023', 1, 'wdanher3j@cnbc.com', '5257986675', 0, 1, '2/1/2023', '6/17/2022'),
('f9f29dd1-48a3-4b15-9b8d-b253c587a66f', '5477843837', 'Doyle', '11/5/2022', 1, 'dstandring3k@zimbio.com', '7845587042', 0, 1, '10/9/2022', '5/14/2022'),
('6ece6364-f383-4f85-a886-310d1762ea1b', '7394943054', 'Dianemarie', '10/10/2022', 0, 'dburgot3l@techcrunch.com', '9592763725', 1, 1, '9/8/2022', '6/25/2022'),
('80f4b668-2a0b-42aa-9d27-c9a2f335640d', '6282400082', 'Judi', '9/30/2022', 1, 'jcurnock3m@apple.com', '6019514905', 1, 0, '5/27/2022', '11/19/2022'),
('eca76993-0b6a-44d6-8076-331e2e05feb6', '0132582252', 'Annadiane', '12/2/2022', 1, 'aapperley3n@mapquest.com', '8822757781', 1, 2, '4/19/2022', '11/30/2022'),
('c2507f07-b0fc-476b-8dd5-c4cf10de2f6f', '4670418092', 'Rosemaria', '10/20/2022', 1, 'rgreenland3o@sbwire.com', '8963582565', 1, 1, '4/3/2022', '3/14/2022'),
('f268374a-6d32-4b98-9354-7a78aaf6c964', '9013200400', 'Don', '6/16/2022', 1, 'dkidsley3p@imageshack.us', '9335157965', 0, 1, '7/9/2022', '9/14/2022'),
('dc3ab8d8-72c9-45a6-942e-445b7afab77b', '8926375897', 'Sayre', '6/9/2022', 1, 'smilnthorpe3q@reference.com', '9618919370', 1, 2, '10/10/2022', '1/21/2023'),
('9bb484d2-cdd3-4798-9861-cec4954a1e6e', '5011658929', 'Tiffanie', '3/12/2022', 0, 'tfullick3r@qq.com', '3544005300', 1, 1, '1/12/2023', '7/2/2022'),
('ad0fc065-15a4-4497-aa96-32a67db92bbc', '2839403056', 'Archer', '3/15/2022', 0, 'aramsbotham3s@cafepress.com', '9927710431', 0, 0, '3/27/2022', '4/24/2022'),
('146bbbe3-e23d-44a4-82b0-50ed0a63cc0e', '0560385471', 'Theressa', '7/13/2022', 0, 'tneising3t@bbc.co.uk', '1323221079', 1, 1, '3/13/2022', '10/18/2022'),
('356ab8c7-0610-4a05-bdfd-925214af090c', '3980483614', 'Stefan', '12/18/2022', 0, 'scramer3u@weather.com', '2049440093', 1, 1, '7/8/2022', '11/23/2022'),
('990b237b-b0de-4d92-85ce-3bd05d160017', '6978187577', 'Jaquith', '2/5/2023', 0, 'jsouza3v@alexa.com', '7875304192', 1, 2, '5/13/2022', '1/18/2023'),
('691afb25-b950-4978-bd21-d2f5d40fe14d', '6776575894', 'Raddy', '12/19/2022', 1, 'rcurtiss3w@123-reg.co.uk', '2315741889', 1, 2, '5/11/2022', '8/13/2022'),
('cbc02dcf-81b0-4ee2-b13b-27b8d94d8493', '7063631772', 'Jarad', '9/21/2022', 0, 'jkneebone3x@comsenz.com', '3174927967', 0, 2, '9/21/2022', '6/23/2022'),
('e77f9515-0441-4ed1-bfd5-1de27c86d0be', '5727791839', 'Adham', '9/1/2022', 1, 'amorhall3y@alexa.com', '5491994816', 0, 2, '3/5/2022', '1/11/2023'),
('231482dc-f3e3-4021-805f-a03e93bf57eb', '0424001705', 'Zack', '12/23/2022', 1, 'zpurdey3z@accuweather.com', '3802254836', 1, 2, '8/7/2022', '10/4/2022'),
('b62d06c7-ec0b-4a8f-a079-d7f49a20f12c', '1618294881', 'Valma', '1/18/2023', 0, 'vkiloh40@edublogs.org', '2819293523', 1, 0, '9/25/2022', '7/1/2022'),
('c68fb0e0-5ab0-4308-9645-53c5fc76961a', '4412020274', 'Renate', '1/19/2023', 0, 'rfessby41@cisco.com', '9816400953', 1, 0, '2/11/2023', '4/5/2022'),
('7a4a1351-e40b-40dc-a29c-5ee134df5c30', '0181998483', 'Padraic', '4/23/2022', 1, 'pblaymires42@blogspot.com', '7033556550', 0, 1, '4/21/2022', '10/3/2022'),
('7dc75417-2898-48e8-b612-7689603eafce', '9177778014', 'Hynda', '6/7/2022', 1, 'hbucksey43@paginegialle.it', '3202636086', 0, 1, '11/18/2022', '3/13/2022'),
('3e9669ba-8232-423e-9a94-2c20317a1672', '1877306622', 'Chauncey', '12/28/2022', 0, 'cmityukov44@hao123.com', '5665204315', 0, 1, '3/23/2022', '7/28/2022'),
('1882c59f-674e-4245-81f2-4581681d087b', '0411148877', 'Pen', '5/21/2022', 0, 'pgeorgel45@networksolutions.com', '7325680110', 1, 0, '11/16/2022', '12/21/2022'),
('0a5b1c63-c7f4-4bdb-bc36-1618950e89f1', '1992927782', 'Alonso', '10/18/2022', 1, 'agringley46@ameblo.jp', '7982827948', 1, 2, '6/1/2022', '8/27/2022'),
('d507bf56-b79e-4c97-8e35-24846b30f092', '2860950281', 'Sonia', '11/17/2022', 1, 'sizacenko47@studiopress.com', '7591489458', 0, 1, '10/16/2022', '3/6/2022'),
('45842cee-ac7c-44d3-96ea-20da4885c748', '6802649926', 'Germayne', '11/26/2022', 0, 'gdaines48@dion.ne.jp', '5723809289', 1, 2, '1/31/2023', '9/18/2022'),
('5ad11ae8-987d-4a58-82dc-c5115003797d', '9816516284', 'Jeannie', '4/26/2022', 1, 'jveryan49@squarespace.com', '6239120900', 1, 1, '3/28/2022', '1/3/2023'),
('065e19cf-ef3b-4b32-8d32-b19210209156', '6369892998', 'Cletus', '10/8/2022', 0, 'cvauls4a@csmonitor.com', '4382990044', 0, 1, '1/21/2023', '7/22/2022'),
('d9c55a32-6ba4-401e-b9b8-1128bf810d93', '5448940196', 'Dore', '6/15/2022', 0, 'dmartinec4b@washington.edu', '2153175904', 1, 2, '3/21/2022', '8/1/2022'),
('31f45b5a-f8bf-4968-8885-6f3742eb06ac', '9954922318', 'Katee', '1/30/2023', 0, 'kcalvey4c@studiopress.com', '3024792717', 1, 1, '4/17/2022', '10/26/2022'),
('92c3300c-bda6-4156-a641-7cba9a380c04', '0776579312', 'Marlin', '12/9/2022', 1, 'mlavens4d@goo.ne.jp', '9793181889', 1, 0, '1/7/2023', '9/3/2022'),
('40ec35ec-221b-44aa-8e5b-7b658f57e88a', '4368750942', 'Lissy', '2/21/2023', 1, 'llemarquand4e@time.com', '4036938085', 1, 1, '11/29/2022', '7/14/2022'),
('86e1971e-5d5c-4c1a-b6fc-e20752720cd2', '8390792923', 'Rhonda', '3/3/2022', 1, 'rcheyenne4f@dyndns.org', '3825584621', 1, 1, '3/28/2022', '8/12/2022'),
('cd86e35b-3ab1-4976-ba29-f615924a0cd7', '1568747713', 'Hughie', '7/13/2022', 1, 'hsmardon4g@cisco.com', '7588215637', 0, 1, '11/21/2022', '8/28/2022'),
('52ee7e1f-d345-4d44-a4af-14ecf0fe1c31', '3533879801', 'Berny', '1/6/2023', 0, 'bkorting4h@tripod.com', '9416896131', 0, 0, '2/14/2023', '12/29/2022'),
('ff530413-9e4d-48b2-816e-f54c980d3721', '2047742110', 'Avie', '6/15/2022', 0, 'abramhill4i@earthlink.net', '7924926808', 1, 2, '12/20/2022', '11/3/2022'),
('2280cf1e-8887-4d82-a341-316944ec6751', '0446142042', 'Nellie', '10/11/2022', 1, 'nfuller4j@hubpages.com', '8749122027', 0, 0, '1/22/2023', '3/28/2022'),
('041cbbf9-7141-4ad2-bfe5-b2b065c72075', '4096964549', 'Mella', '9/8/2022', 0, 'mdegoey4k@washington.edu', '1993784167', 0, 0, '7/5/2022', '4/27/2022'),
('12a4f99a-29e0-4352-b684-b1073ae09b25', '5911449978', 'Cynthia', '4/23/2022', 0, 'cberens4l@hp.com', '4588300216', 0, 1, '10/4/2022', '8/17/2022'),
('bccda068-ace9-44e5-89b8-885ec15f92bc', '2232863220', 'Jacobo', '11/29/2022', 0, 'jdurrett4m@who.int', '4592911950', 0, 1, '5/13/2022', '1/5/2023'),
('76416b57-dca9-430a-920b-27811e6049cc', '8546766886', 'Emlyn', '12/7/2022', 0, 'ebingham4n@unesco.org', '8441871887', 0, 2, '1/19/2023', '5/19/2022'),
('6842a28e-02ed-448e-b877-04543c140f05', '3228972356', 'Rois', '1/16/2023', 0, 'rharnott4o@dmoz.org', '6128860535', 0, 0, '11/23/2022', '10/5/2022'),
('b1a71d05-218f-468b-bb5b-867665d139d4', '8603912173', 'Laurens', '8/11/2022', 0, 'lchristopher4p@weebly.com', '1172463934', 1, 0, '2/20/2023', '9/24/2022'),
('0d5b82c2-412f-42b0-84ed-cc91fac8ac63', '8329134415', 'Ania', '6/29/2022', 1, 'aankers4q@quantcast.com', '7747594791', 1, 1, '8/30/2022', '7/30/2022'),
('0beaa9a4-075c-418c-9d52-8495098a770d', '3090862239', 'Merrick', '5/6/2022', 0, 'mpreene4r@dailymotion.com', '3985867312', 1, 0, '6/26/2022', '4/6/2022'),
('99165a74-338b-4ae8-8f8b-d048e92dea05', '9022415163', 'Isabel', '2/9/2023', 1, 'ieaglestone4s@accuweather.com', '2778792365', 1, 1, '9/18/2022', '11/28/2022'),
('a2fc26d5-5a61-4fe3-b00f-b6820e986998', '6261929777', 'Harriet', '12/6/2022', 1, 'hchorlton4t@mapy.cz', '9497773696', 0, 2, '6/4/2022', '7/20/2022'),
('be3e63f6-9e9d-4dc5-b25a-51e6e1d99e4b', '3293295223', 'Lorant', '11/29/2022', 1, 'lgribbell4u@biglobe.ne.jp', '6392488912', 1, 2, '6/16/2022', '2/15/2023'),
('47c7c0e1-660c-460f-a913-7afbf82b2468', '6068129144', 'Kristofer', '11/15/2022', 1, 'khugnet4v@europa.eu', '2195234062', 1, 1, '1/11/2023', '2/4/2023'),
('9b2d0ad7-8272-47ed-9bb3-9efed6174aef', '9942092684', 'Lucilia', '1/30/2023', 0, 'lleeder4w@utexas.edu', '7958959409', 1, 1, '11/22/2022', '3/26/2022'),
('374f14ba-839b-4e32-b8e9-80f7762f24a9', '3322586316', 'Brewer', '4/7/2022', 1, 'bbrightie4x@tuttocitta.it', '8264688998', 1, 0, '1/10/2023', '3/10/2022'),
('f791744e-4e17-4c18-b90c-b2284322afc1', '9133661464', 'Nettie', '9/28/2022', 0, 'nhayzer4y@engadget.com', '4706432382', 1, 1, '4/19/2022', '5/19/2022'),
('02680074-a5b7-4c07-9417-d41408c4f418', '4897674956', 'Bette-ann', '2/28/2022', 1, 'bbrittain4z@sciencedirect.com', '2285277766', 1, 0, '5/3/2022', '7/12/2022'),
('c6581dca-9d87-4c60-8fc0-a2b95cd41291', '4973839418', 'Alene', '10/30/2022', 1, 'aheinzler50@etsy.com', '4455517875', 0, 2, '2/11/2023', '12/15/2022'),
('7188202f-e7e7-4ccd-a89d-5496411b4445', '6284890389', 'Borden', '2/26/2022', 1, 'bstainton51@chronoengine.com', '3247902443', 1, 0, '7/5/2022', '3/4/2022'),
('315fc768-e376-478a-b6c0-5c66b1d00a59', '0453949088', 'Brander', '12/23/2022', 1, 'bpetru52@newyorker.com', '3233438893', 1, 0, '12/15/2022', '5/17/2022'),
('3319bfc1-1bbf-4706-bb32-6249e2977d43', '0031344836', 'Nicolette', '12/11/2022', 0, 'nlambrick53@washington.edu', '6931296717', 0, 1, '9/6/2022', '9/1/2022'),
('88b56825-56cf-4c37-803c-0289f6570477', '9151749939', 'Sondra', '3/11/2022', 1, 'sclunie54@cloudflare.com', '9195491271', 0, 2, '2/9/2023', '10/28/2022'),
('5511a309-4921-4449-9680-275bb292b857', '7114166974', 'Aldis', '4/17/2022', 0, 'ayablsley55@state.tx.us', '1926231529', 1, 1, '10/2/2022', '9/11/2022'),
('1299998e-8b86-4b36-a9f8-10265c68409d', '8838179948', 'Eugenio', '5/2/2022', 1, 'enouch56@acquirethisname.com', '9345750434', 1, 2, '10/11/2022', '5/30/2022'),
('48a54753-73ac-4b1a-8996-2ccd59e07d65', '1606419153', 'Della', '9/30/2022', 0, 'dbence57@google.co.jp', '2677543710', 0, 0, '11/11/2022', '1/2/2023'),
('9d59adbb-bc9d-469c-9a4b-d019a6047010', '8566515560', 'Rafaela', '12/2/2022', 1, 'rdimanche58@ox.ac.uk', '9974355519', 0, 2, '8/4/2022', '7/11/2022'),
('765f1246-716b-4ebd-8e5c-8174c3148a8a', '9974878160', 'Dasie', '4/6/2022', 1, 'ddebrett59@go.com', '3557238874', 0, 1, '9/28/2022', '5/21/2022'),
('4a777734-aa64-46db-8909-95318e5bd599', '9392741057', 'Isahella', '3/15/2022', 0, 'imillery5a@disqus.com', '4336328671', 1, 2, '9/11/2022', '9/30/2022'),
('1919a7cc-b103-4513-92f3-84a010b2a959', '5288900337', 'Allissa', '2/27/2022', 1, 'asparham5b@fc2.com', '8707911341', 1, 1, '12/30/2022', '5/27/2022'),
('7fdace40-7b9b-4be2-b871-5d1eb20bc4c5', '9368264864', 'Hobart', '11/3/2022', 1, 'harrundale5c@weather.com', '7689148433', 0, 0, '8/4/2022', '11/2/2022'),
('37d7cda9-42d3-4988-b230-55d16a3b8246', '4906003044', 'Gil', '2/21/2023', 0, 'gputtan5d@hp.com', '1207026681', 1, 1, '12/13/2022', '9/11/2022'),
('2ef430e9-d4fc-4367-96b7-154ec61bf00c', '5322735704', 'Jenda', '7/7/2022', 1, 'jhubane5e@360.cn', '5762400843', 1, 1, '11/26/2022', '6/1/2022'),
('079105f3-f547-4b47-bddc-2ab5595df97b', '5819518764', 'Franky', '9/18/2022', 1, 'fkillich5f@canalblog.com', '8126349063', 1, 2, '4/16/2022', '1/2/2023'),
('019866aa-4906-44ec-bcd0-ca8eec248488', '4662241839', 'Jasmina', '8/23/2022', 1, 'jlewtey5g@tinyurl.com', '5299158664', 0, 0, '8/10/2022', '12/17/2022'),
('1ba44330-c065-41f4-b033-998168bf923b', '0434759384', 'Holly-anne', '9/13/2022', 1, 'hhorsburgh5h@pagesperso-orange.fr', '5806195787', 0, 0, '11/1/2022', '1/8/2023'),
('80bb40c3-9eb5-4939-acba-2fdc303aaf16', '5226021992', 'Giordano', '5/3/2022', 1, 'glober5i@desdev.cn', '4816022985', 1, 0, '9/25/2022', '12/10/2022'),
('2ff88a23-61c3-42db-bdaa-5c72c853f971', '6362003608', 'Camila', '5/11/2022', 0, 'cperring5j@gravatar.com', '1521565735', 1, 0, '7/1/2022', '4/25/2022');

INSERT INTO Employment (id, role, enterprise, section, salary_range, initiative, status, start_date, end_date, person_id, created_at, updated_at) VALUES
('2d25c6f6-5352-4634-bfeb-0a0d740e9554', 'Analista de sistemas', 'Take Blip', 'TI', 7000.00, 1, 1, convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5), '4df17a6b-3097-47b0-b934-f9c5d906c581', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('f230efab-b42c-442e-add5-46e913a49c48', 'Professora', 'Prefeitura Itabuna', 'Sec Edu', 3000.00, 0, 0, convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5), 'dcbe59a8-d650-4bee-8bf1-497b902b749f', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('e9a43f9f-36d4-4530-b40a-ecba4d3d2525', 'Software engineer', 'Valtech', 'TI', 35000.00, 1, 1, convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5), 'c174ad75-602a-4034-92ea-474b736069a9', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5));

INSERT INTO Testimony (id, content, was_accepted, person_id, created_at, updated_at) VALUES 
('12c56e42-6f8e-4430-b3cb-e9d6d6461cfc', 'content1', 1, '4df17a6b-3097-47b0-b934-f9c5d906c581', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('624156cc-b6ca-4a7d-8025-7fb0eef4366d', 'content2', 1, '4df17a6b-3097-47b0-b934-f9c5d906c581', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5)),
('c2535895-4b98-4f97-9367-0d9829b8989d', 'content3', 1, 'c174ad75-602a-4034-92ea-474b736069a9', convert(datetime2,'18-06-12 10:34:09 PM',5), convert(datetime2,'18-06-12 10:34:09 PM',5));

INSERT INTO Course (id, course_name, created_at, updated_at) VALUES
('2fac0950-65b2-4bba-ae0f-45928b1cc3c7', 'Ciencia da Computacao', '1/8/2023', '9/19/2022'),
('76b53ed9-2cb5-4677-b334-620169352e8e', 'Engenharia mecanica', '6/24/2022', '12/1/2022'),
('e8c347ec-a323-4b20-9622-9bdb554952d8', 'Pedagogia', '10/28/2022', '6/5/2022'),
('86f5e983-0388-4cd2-b747-94b9c0b4ac8a', 'Sistema da informacao', '10/14/2022', '12/22/2022'),
('18e8729e-6786-4a95-a65a-0a445bc77075', 'Geografia', '11/17/2022', '9/11/2022'),
('742912c3-801e-4601-bb17-0fa83e1ee2e0', 'Filosofia', '5/10/2022', '6/2/2022'),
('80b300d9-6b1f-4236-ba30-69e7592901c4', 'Medicina', '9/29/2022', '11/27/2022'),
('1c8515c3-be07-47ba-bc39-b122701ce981', 'Biomedicina', '9/6/2022', '10/5/2022'),
('9cdacfb6-7bd4-4409-9914-2455474930db', 'Veterinaria', '10/5/2022', '5/27/2022'),
('40156226-6c8f-4b0e-a5b6-1897ae9676bf', 'Matematica', '7/4/2022', '10/9/2022');

INSERT INTO PersonCourse (id, start_date, end_date, mat, level, modality, person_id, course_id, created_at, updated_at) VALUES
('d4756c29-4926-4b9e-a70e-a23da8fb5cd5', '5/16/2022', '4/13/2022', 276097435, 2, 1, '4df17a6b-3097-47b0-b934-f9c5d906c581', '2fac0950-65b2-4bba-ae0f-45928b1cc3c7', '12/23/2022', '12/3/2022'),
('8277a4de-1332-4858-81a7-7726588e8407', '3/27/2022', '7/22/2022', 892549970, 1, 3, 'dcbe59a8-d650-4bee-8bf1-497b902b749f', 'e8c347ec-a323-4b20-9622-9bdb554952d8', '4/23/2022', '6/16/2022'),
('93bc95c8-b604-4e0a-aa28-d72a40fb4d4c', '10/23/2022', '9/24/2022', 390219632, 1, 3, 'c174ad75-602a-4034-92ea-474b736069a9', '86f5e983-0388-4cd2-b747-94b9c0b4ac8a', '11/27/2022', '8/28/2022'),
('0df28379-e0d3-4545-bc50-204af070b0c4', '6/26/2022', '3/27/2022', 841817024, 3, 3, 'dcbe59a8-d650-4bee-8bf1-497b902b749f', '18e8729e-6786-4a95-a65a-0a445bc77075', '7/14/2022', '1/17/2023'),
('f03d4615-faca-4a82-b77d-86df8f137897', '3/15/2022', '12/28/2022', 738059024, 1, 3, '374f14ba-839b-4e32-b8e9-80f7762f24a9', '1c8515c3-be07-47ba-bc39-b122701ce981', '5/25/2022', '2/12/2023'),
('26d90640-94a9-4428-9016-4b14c17cbe7e', '12/31/2022', '8/14/2022', 121770088, 0, 1, '2ff88a23-61c3-42db-bdaa-5c72c853f971', '40156226-6c8f-4b0e-a5b6-1897ae9676bf', '2/15/2023', '10/15/2022');

