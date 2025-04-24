CREATE TABLE projects
(
    id                SERIAL PRIMARY KEY,
    title             VARCHAR(100)   NOT NULL,
    project_code      CHAR(6)        NOT NULL,
    description       VARCHAR(500),
    start_date        DATE           NOT NULL,
    expected_end_date DATE,
    priority          INTEGER        NOT NULL,
    budget            DECIMAL(12, 2) NOT NULL,

    CONSTRAINT chk_title_length CHECK (LENGTH(title) >= 3 AND LENGTH(title) <= 100),
    CONSTRAINT chk_project_code_format CHECK (project_code ~ '^[A-Z0-9]{6,8}$'),
    CONSTRAINT chk_priority_range CHECK (priority >= 1 AND priority <= 5),
    CONSTRAINT chk_budget_range CHECK (budget >= 0 AND budget <= 1000000),
    CONSTRAINT uq_project_code UNIQUE (project_code)
);

CREATE INDEX idx_projects_project_code ON projects (project_code);

-- Clear existing project data if needed
-- TRUNCATE TABLE "Projects" RESTART IDENTITY;

-- Insert 100 sample projects
INSERT INTO projects (title, project_code, description, start_date, expected_end_date, priority, budget)
VALUES
-- Project 1
('Web Portal Redesign', 'WEB723', 'Complete overhaul of the company customer portal with modern UI/UX principles and improved accessibility features.', '2025-01-15', '2025-05-30', 4, 85000.00),
-- Project 2
('Mobile App Development', 'MOB194', 'Development of a cross-platform mobile application for both iOS and Android using React Native.', '2025-02-10', '2025-07-20', 5, 120000.00),
-- Project 3
('Database Migration', 'DBM452', 'Migration from legacy SQL Server to PostgreSQL with data integrity verification and performance optimization.', '2025-03-05', '2025-06-15', 4, 65000.00),
-- Project 4
('Cloud Infrastructure Setup', 'CLD835', 'Setting up scalable cloud infrastructure on AWS with proper security policies and monitoring.', '2025-01-20', '2025-04-10', 3, 45000.00),
-- Project 5
('Security Audit', 'SEC721', 'Comprehensive security audit of all systems and implementation of recommended improvements.', '2025-04-01', '2025-05-15', 5, 30000.00),
-- Project 6
('Employee Training Portal', 'TRN436', 'Development of an internal training portal with course tracking and certification management.', '2025-03-10', '2025-08-30', 2, 50000.00),
-- Project 7
('Customer Support System', 'SUP198', 'Implementation of a new ticketing system for customer support with AI-powered routing.', '2025-02-15', '2025-05-20', 3, 68000.00),
-- Project 8
('Payment Gateway Integration', 'PAY625', 'Integration of multiple payment gateways to support international transactions and cryptocurrencies.', '2025-05-01', '2025-07-15', 4, 35000.00),
-- Project 9
('Analytics Dashboard', 'ANL247', 'Creation of real-time analytics dashboard for business intelligence and decision making.', '2025-04-15', '2025-09-10', 3, 55000.00),
-- Project 10
('Social Media Campaign', 'SMC913', 'Planning and execution of cross-platform social media campaign for product launch.', '2025-06-01', '2025-08-15', 2, 25000.00),
-- Project 11
('Network Infrastructure Upgrade', 'NET578', 'Upgrade of core network infrastructure to support 10Gb connectivity and improved reliability.', '2025-05-10', '2025-09-20', 3, 95000.00),
-- Project 12
('Content Management System', 'CMS362', 'Implementation of a new CMS for the corporate website with multilingual support.', '2025-03-20', '2025-07-10', 2, 43000.00),
-- Project 13
('Inventory Management System', 'INV741', 'Development of a barcode-based inventory tracking system with real-time updates.', '2025-04-05', '2025-08-25', 3, 62000.00),
-- Project 14
('Email Marketing Platform', 'EML529', 'Setup of an email marketing platform with automation and segmentation capabilities.', '2025-02-25', '2025-05-05', 2, 28000.00),
-- Project 15
('Virtual Reality Training', 'VRT863', 'Development of VR-based safety training modules for manufacturing staff.', '2025-06-15', '2025-11-30', 3, 110000.00),
-- Project 16
('API Gateway Implementation', 'API492', 'Implementation of an API gateway for secure and controlled access to backend services.', '2025-05-20', '2025-08-10', 4, 47000.00),
-- Project 17
('HR System Upgrade', 'HRS318', 'Upgrade of the HR management system with improved reporting and compliance features.', '2025-04-10', '2025-07-25', 2, 38000.00),
-- Project 18
('Financial Reporting Tool', 'FIN627', 'Development of automated financial reporting tool with regulatory compliance features.', '2025-03-15', '2025-06-30', 4, 72000.00),
-- Project 19
('Remote Work Infrastructure', 'REM194', 'Enhancement of remote work capabilities with secure access and collaboration tools.', '2025-01-05', '2025-04-15', 5, 65000.00),
-- Project 20
('Customer Loyalty Program', 'LOY283', 'Implementation of a points-based customer loyalty program with mobile app integration.', '2025-05-05', '2025-09-15', 3, 58000.00),
-- Project 21
('DevOps Pipeline Setup', 'DEV751', 'Setting up automated CI/CD pipelines for faster and more reliable software delivery.', '2025-02-20', '2025-05-10', 4, 40000.00),
-- Project 22
('Data Warehouse Implementation', 'DWH394', 'Implementation of a data warehouse for business analytics and reporting.', '2025-04-20', '2025-09-30', 3, 88000.00),
-- Project 23
('Multi-factor Authentication', 'MFA623', 'Implementation of MFA across all company systems to enhance security.', '2025-03-25', '2025-06-05', 5, 22000.00),
-- Project 24
('Document Management System', 'DOC187', 'Implementation of a document management system with version control and approval workflows.', '2025-05-15', '2025-08-20', 2, 36000.00),
-- Project 25
('Product Recommendation Engine', 'REC952', 'Development of an AI-powered product recommendation engine for the e-commerce platform.', '2025-06-10', '2025-10-15', 3, 78000.00),
-- Project 26
('Supply Chain Optimization', 'SCO416', 'Optimization of the supply chain processes with predictive analytics for inventory management.', '2025-04-25', '2025-09-05', 4, 92000.00),
-- Project 27
('Customer Feedback System', 'CFB274', 'Implementation of an automated customer feedback collection and analysis system.', '2025-03-05', '2025-06-25', 2, 33000.00),
-- Project 28
('Business Process Automation', 'BPA618', 'Automation of key business processes to reduce manual work and improve efficiency.', '2025-05-25', '2025-10-10', 3, 67000.00),
-- Project 29
('Disaster Recovery Planning', 'DRP843', 'Development and testing of comprehensive disaster recovery procedures for critical systems.', '2025-04-15', '2025-07-05', 5, 29000.00),
-- Project 30
('Corporate Intranet Redesign', 'INT591', 'Redesign of the corporate intranet with improved search and personalization features.', '2025-06-05', '2025-09-25', 2, 42000.00),
-- Project 31
('E-commerce Platform Upgrade', 'ECM372', 'Major upgrade of the e-commerce platform with improved mobile experience and payment options.', '2025-03-10', '2025-08-15', 4, 115000.00),
-- Project 32
('Knowledge Base Development', 'KNB216', 'Development of a searchable knowledge base for customer self-service.', '2025-05-05', '2025-08-05', 2, 27000.00),
-- Project 33
('AI Chatbot Implementation', 'CHT943', 'Implementation of AI-powered chatbots for customer service and internal support.', '2025-04-20', '2025-08-10', 3, 52000.00),
-- Project 34
('Sales Automation System', 'SLS625', 'Implementation of a sales automation system with CRM integration and reporting.', '2025-02-15', '2025-06-10', 4, 63000.00),
-- Project 35
('Video Conferencing Solution', 'VCS318', 'Implementation of an enterprise-grade video conferencing solution for remote meetings.', '2025-01-25', '2025-04-05', 3, 31000.00),
-- Project 36
('Compliance Management System', 'CMP742', 'Implementation of a system to track and manage regulatory compliance requirements.', '2025-05-20', '2025-09-15', 4, 48000.00),
-- Project 37
('Digital Signage Network', 'DSN195', 'Implementation of a digital signage network across all office locations.', '2025-03-15', '2025-06-20', 1, 35000.00),
-- Project 38
('Quality Assurance Automation', 'QAA624', 'Automation of QA processes with continuous testing integration in the development pipeline.', '2025-04-10', '2025-07-15', 3, 44000.00),
-- Project 39
('Performance Monitoring System', 'PMS873', 'Implementation of end-to-end performance monitoring for all critical systems.', '2025-06-01', '2025-09-10', 4, 37000.00),
-- Project 40
('Unified Communications', 'UCM429', 'Implementation of a unified communications platform integrating voice, video, and messaging.', '2025-05-10', '2025-08-30', 3, 71000.00),
-- Project 41
('IoT Sensor Network', 'IOT516', 'Implementation of an IoT sensor network for building management and energy optimization.', '2025-04-05', '2025-09-20', 2, 83000.00),
-- Project 42
('Fleet Management System', 'FMS287', 'Implementation of a GPS-based fleet management and routing optimization system.', '2025-03-20', '2025-07-10', 3, 59000.00),
-- Project 43
('Contract Management System', 'CTM631', 'Implementation of a contract lifecycle management system with e-signature integration.', '2025-05-15', '2025-08-25', 2, 32000.00),
-- Project 44
('Virtual Desktop Infrastructure', 'VDI893', 'Implementation of VDI for secure remote access to corporate resources.', '2025-02-10', '2025-06-15', 4, 78000.00),
-- Project 45
('Project Management Tool', 'PMT421', 'Implementation of an enterprise project management tool with resource allocation features.', '2025-04-15', '2025-07-30', 3, 45000.00),
-- Project 46
('Customer Data Platform', 'CDP764', 'Implementation of a customer data platform for unified customer profiles and insights.', '2025-06-10', '2025-10-20', 4, 93000.00),
-- Project 47
('Augmented Reality App', 'ARA158', 'Development of an AR app for product visualization and interactive catalogs.', '2025-05-25', '2025-11-15', 3, 105000.00),
-- Project 48
('Identity Management System', 'IDM592', 'Implementation of a centralized identity management system with SSO capabilities.', '2025-03-05', '2025-07-20', 4, 67000.00),
-- Project 49
('Gamification Platform', 'GMP317', 'Development of a gamification platform for employee engagement and training.', '2025-04-25', '2025-08-15', 2, 41000.00),
-- Project 50
('Voice Assistant Integration', 'VAI824', 'Integration of voice assistant technology into existing customer service channels.', '2025-05-05', '2025-09-05', 3, 49000.00),
-- Project 51
('Physical Security Upgrade', 'PSU615', 'Upgrade of physical security systems including access control and surveillance.', '2025-02-20', '2025-05-30', 4, 56000.00),
-- Project 52
('Automated Testing Framework', 'ATF293', 'Development of an automated testing framework for web and mobile applications.', '2025-04-10', '2025-07-25', 3, 38000.00),
-- Project 53
('Blockchain Prototype', 'BCH719', 'Development of a blockchain prototype for secure transaction verification.', '2025-06-15', '2025-10-30', 2, 72000.00),
-- Project 54
('Sustainability Reporting', 'SUS481', 'Implementation of a sustainability metrics tracking and reporting system.', '2025-03-25', '2025-07-15', 1, 26000.00),
-- Project 55
('Code Quality System', 'CQS342', 'Implementation of a code quality monitoring system with automated reviews.', '2025-05-20', '2025-08-10', 3, 33000.00),
-- Project 56
('Digital Asset Management', 'DAM698', 'Implementation of a digital asset management system for brand consistency.', '2025-04-05', '2025-07-20', 2, 47000.00),
-- Project 57
('Internal Podcast Platform', 'IPP125', 'Setup of an internal podcast platform for corporate communications.', '2025-02-15', '2025-05-15', 1, 19000.00),
-- Project 58
('Real-time Collaboration Tools', 'RCT573', 'Implementation of real-time collaboration tools for distributed teams.', '2025-05-10', '2025-08-20', 3, 41000.00),
-- Project 59
('Customer Experience Mapping', 'CEM326', 'Mapping and optimization of customer experience touchpoints across all channels.', '2025-04-20', '2025-08-15', 4, 58000.00),
-- Project 60
('Energy Management System', 'EMS871', 'Implementation of an energy management system for cost optimization and sustainability.', '2025-03-15', '2025-07-10', 2, 51000.00),
-- Project 61
('Website Localization', 'WLO429', 'Localization of the corporate website for ten additional languages.', '2025-05-15', '2025-09-10', 3, 62000.00),
-- Project 62
('Vendor Management System', 'VMS683', 'Implementation of a vendor management system with performance tracking.', '2025-04-15', '2025-08-05', 2, 37000.00),
-- Project 63
('Predictive Maintenance', 'PDM219', 'Implementation of predictive maintenance for manufacturing equipment using IoT data.', '2025-06-05', '2025-10-15', 4, 89000.00),
-- Project 64
('Software License Management', 'SLM567', 'Implementation of a software license management and compliance tracking system.', '2025-03-20', '2025-06-30', 3, 32000.00),
-- Project 65
('Online Training Platform', 'OTP384', 'Development of an online training platform with progress tracking and certifications.', '2025-05-25', '2025-09-25', 2, 53000.00),
-- Project 66
('3D Printing Lab', 'TPL918', 'Setting up a 3D printing lab for rapid prototyping and small-scale production.', '2025-04-10', '2025-08-10', 1, 46000.00),
-- Project 67
('Warehouse Automation', 'WAU752', 'Partial automation of warehouse operations with robotic process automation.', '2025-03-05', '2025-07-25', 3, 118000.00),
-- Project 68
('Smart Office Initiative', 'SOI413', 'Implementation of smart office technology for improved space utilization and comfort.', '2025-05-05', '2025-09-15', 2, 67000.00),
-- Project 69
('Corporate App Store', 'CAS296', 'Development of an internal app store for approved business applications.', '2025-04-25', '2025-08-20', 2, 29000.00),
-- Project 70
('Data Privacy Compliance', 'DPC817', 'Implementation of systems and processes to ensure GDPR and other data privacy compliance.', '2025-02-25', '2025-06-15', 5, 51000.00),
-- Project 71
('Print Management System', 'PMS534', 'Implementation of a secure print management system to reduce waste and improve security.', '2025-05-20', '2025-08-15', 1, 24000.00),
-- Project 72
('Digital Onboarding', 'DON679', 'Development of a digital onboarding system for new employees with workflow automation.', '2025-04-15', '2025-08-30', 3, 47000.00),
-- Project 73
('Biometric Authentication', 'BIO342', 'Implementation of biometric authentication for high-security areas and systems.', '2025-06-10', '2025-09-20', 4, 38000.00),
-- Project 74
('Workflow Automation', 'WFA587', 'Automation of key business workflows to improve efficiency and reduce errors.', '2025-03-15', '2025-07-15', 3, 56000.00),
-- Project 75
('Meeting Room Booking System', 'MRB129', 'Implementation of a meeting room booking system with mobile app and display integration.', '2025-05-10', '2025-08-10', 1, 21000.00),
-- Project 76
('Ticketing System Upgrade', 'TSU783', 'Major upgrade of the customer service ticketing system with AI-powered categorization.', '2025-04-05', '2025-08-25', 3, 43000.00),
-- Project 77
('Research Data Management', 'RDM426', 'Implementation of a research data management platform with compliance features.', '2025-03-25', '2025-07-30', 4, 59000.00),
-- Project 78
('Field Service Automation', 'FSA691', 'Implementation of mobile tools for field service teams with real-time data access.', '2025-05-15', '2025-09-30', 3, 72000.00),
-- Project 79
('Call Center AI Assistant', 'CCA258', 'Implementation of an AI assistant for call center agents with real-time recommendations.', '2025-06-01', '2025-10-10', 3, 68000.00),
-- Project 80
('Price Optimization System', 'POS837', 'Implementation of a dynamic pricing system with market analysis capabilities.', '2025-04-20', '2025-09-15', 4, 83000.00),
-- Project 81
('Recruitment Marketing', 'RCM495', 'Development of a recruitment marketing platform to attract top talent.', '2025-03-10', '2025-06-20', 2, 35000.00),
-- Project 82
('Customer Segmentation Tool', 'CST628', 'Development of an advanced customer segmentation tool using machine learning.', '2025-05-25', '2025-09-25', 3, 64000.00),
-- Project 83
('Content Delivery Network', 'CDN374', 'Implementation of a global CDN for improved website and application performance.', '2025-04-10', '2025-07-20', 3, 39000.00),
-- Project 84
('Digital Workplace Portal', 'DWP519', 'Development of a unified digital workplace portal for employee tools and information.', '2025-06-15', '2025-10-15', 2, 52000.00),
-- Project 85
('Real-time Reporting Dashboard', 'RRD762', 'Development of real-time reporting dashboards for executive decisions.', '2025-03-20', '2025-07-05', 4, 48000.00),
-- Project 86
('Revenue Forecasting Tool', 'RFT391', 'Development of an advanced revenue forecasting tool with scenario analysis.', '2025-05-05', '2025-09-10', 4, 57000.00),
-- Project 87
('Employee Recognition Platform', 'ERP648', 'Implementation of a peer-to-peer employee recognition and rewards platform.', '2025-04-25', '2025-08-15', 1, 23000.00),
-- Project 88
('Paperless Office Initiative', 'POI215', 'Implementation of digital workflows to reduce paper usage across the organization.', '2025-02-10', '2025-06-10', 2, 31000.00),
-- Project 89
('Text Analytics Platform', 'TAP573', 'Implementation of a text analytics platform for customer feedback analysis.', '2025-05-20', '2025-09-20', 3, 76000.00),
-- Project 90
('Product Information Management', 'PIM824', 'Implementation of a centralized product information management system.', '2025-04-15', '2025-08-30', 3, 63000.00),
-- Project 91
('Equipment Monitoring System', 'EMS369', 'Implementation of IoT-based equipment monitoring for proactive maintenance.', '2025-03-05', '2025-07-15', 4, 71000.00),
-- Project 92
('Search Engine Optimization', 'SEO712', 'Comprehensive SEO initiative to improve organic search rankings and traffic.', '2025-05-15', '2025-09-15', 2, 34000.00),
-- Project 93
('Donation Management System', 'DMS485', 'Implementation of a donation management system for the corporate foundation.', '2025-04-05', '2025-08-05', 1, 27000.00),
-- Project 94
('Multi-channel Marketing', 'MCM927', 'Implementation of a multi-channel marketing automation platform.', '2025-06-10', '2025-10-20', 3, 79000.00),
-- Project 95
('Agile Transformation', 'AGT563', 'Organizational transformation to Agile methodologies with training and tools.', '2025-03-15', '2025-08-15', 4, 95000.00),
-- Project 96
('Health and Safety Platform', 'HSP318', 'Implementation of a health and safety tracking and reporting platform.', '2025-05-25', '2025-09-05', 3, 42000.00),
-- Project 97
('Expense Management System', 'EMS791', 'Implementation of an automated expense management system with mobile receipt capture.', '2025-04-20', '2025-08-10', 2, 36000.00),
-- Project 98
('Data Visualization Tools', 'DVT426', 'Implementation of advanced data visualization tools for business analytics.', '2025-03-25', '2025-07-25', 3, 49000.00),
-- Project 99
('Robotic Process Automation', 'RPA683', 'Implementation of RPA for high-volume, repetitive back-office tasks.', '2025-05-10', '2025-09-30', 4, 87000.00),
-- Project 100
('Digital Transformation Strategy', 'DTS954', 'Development of a comprehensive digital transformation strategy and roadmap.', '2025-04-15', '2025-10-15', 5, 125000.00);