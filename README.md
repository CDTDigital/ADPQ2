# California Department of Technology, Request for Interest #CDT-ADQP-0117 Pre-Qualified Vendor Pool for Digital Services – Agile Development






![Natoma Technologies, Inc. Logo][logo]



# Natoma’s ADPQ Submission – Prototype B
Natoma Technologies, Inc. (Natoma) is pleased to respond to the California Office of Systems Integration RFI # CDT-ADPQ-0117 (the RFI) and is thankful for this opportunity to present our prototype and our approach. Consistent with the RFI Attachment 1. PVQP DS-AD Working Prototype and Technical Approach Requirements, we are providing a Working Prototype consistent with Prototype B Requirements defined as 

“The working prototype will be an application that will allow California residents to establish and manage their profile and receive emergency and non-emergency notifications via email, Short Message Service (SMS), and/or push notification based on the location and contact information provided in their profile and/or the geo-location of their cellphone if they have opted in for this service. In addition, the working prototype will provide the authorized administrative users with the ability to publish notifications and track, and analyze and visualize related data. The working prototype does not need to implement any authentication or authorization against an external directory or authentication mechanism.”


# [Link to the prototype web-application][prototype-url]  

Note: Use Chrome or Safari to access the Prototype. Due to the compressed development timeframe and our prioritization for the use of open source, Natoma limited validation to these two web browsers.

The system has been seeded with an Administrative user with the following credentials:

    Username:  admin@example.net
    Password:   password1


## Other useful links:
[Link to our Pivotal Tracker][pt-url]

## Natoma’s Approach to Building the Working Prototype
Natoma convened a multidisciplinary team of highly experienced Information Technology (IT) professionals from within our internal staffing network to work on this Prototype. Everyone on our team is currently working on other Natoma projects and our team members have worked with one another. This allowed us to coalesce quickly as a team and assign work based on each team member’s strengths. Our Chief Operations Officer (COO), Veronica Westlund, assumed the role of Project Director. As a Natoma Executive, she has decision-making authority regarding the approach and scope to be taken for this Prototype. We assigned senior staff to the roles of Agile Coach and Technical Architect. Additional technical development team members were added. The team adopted a Scrum-based approach to development and identified the high level scope necessary to meet the Minimal Value Plan (MVP) for the Working Prototype in a compressed, two-week time frame. Sprint planning included translating the MVP into two epics, creating high level user stories and determining how the Prototype architecture and infrastructure would be built to support the Prototype plan. Our technical team chose modern tools and techniques with which they were already familiar reducing ramp up time. The team determined that the work could be completed during two sprints over 10 days (five days per sprint). This Planning effort was completed within two days of receipt of the RFI and development began immediately.

We assigned another Natoma Senior Business Analyst to assume the role of end-user. This person worked with our team to further refine our user stories and establish testable acceptance criteria for each story. Our developers stood up and configured our development and test environments and began developing the Prototype based upon the team's agreed upon design. Our developers implemented configuration management, automated unit testing and continuous integration using GitHub as a central repository and tool set. Testing began immediate to the creation of a viable working product. The team conducted daily scrum meetings and participated in role playing scenarios where team members tested the Prototype based on a specific role. Our end user participated in this effort. This provided immediate feedback regarding design and usability of the product and resulted in updated or additional user stories. The Project Director reviewed each new user story to determine if the story belonged in the Backlog or in the Icebox for future consideration. This level of effort was performed with both sprints.

In order to achieve our end goal, we relied on collaborative development and communication tools such as Pivotal Tracker, GitHub and Skype. The final result of our efforts is a collaboratively designed and built Working Prototype that meets the MVP of the RFI requirements using modern, open source technologies within a very compressed time frame.


## Natoma’s User-Centered Design
Natoma applied a user-centered approach to the design of the Working Prototype. We identified a senior Natoma team member who served as our simulated end user. The team worked with this person to create purpose driven, high level user stories to define the end user needs for a notification alert system. The team then conducted additional internet-based research on other alert notification systems used within the State of California and other states as well as the technology supporting these systems and refined and enhanced the user stories identifying testable acceptance criteria for each story. The stories were then entered into Pivotal Tracker and linked to the previously identified Prototype epics. The development team created a viable prototype that was then tested by each member of the Sprint team using the role playing technique described above. This approach provided immediate feedback from our “users” as to the look, feel and functionality of our Prototype resulting in a better product that is easy to understand.


# Natoma’s Narrative for the Technical Approach 
## High Level Overview
The Natoma ADPQ2 Prototype uses a modern approach based on .NET core, Angular2, and PostgreSQL to provide a secure, user-friendly website.

Following the principles of Agile and MVP, the ADPQ2 development team built this application focused on the simplest, most quickly developed implementation of the prioritized stories. In the initial kick-off meeting, the team selected the base technologies (.NET Core and PostgreSQL).  As needs became more specific, we identified and added supporting technologies that simplified the implementation of a solution to those problems and provided the stability of code libraries that have already been proven in production environments. Key among the supporting technologies are Angular 2 and TypeScript which combine to form a modern JavaScript framework that provides the user with a responsive interface while giving developers tools that make writing code easier. Each member of the development team was empowered to research, identify, select, and implement tools with an understanding of the abbreviated nature of this development exercise and the need to provide a stable application.

The code was developed as a JavaScript web application that is initiated from the web application and then accesses RESTful services from the API which, in turn, use a business layer to provide business logic and data access. These three main architectural features (web application, API for JavaScript, and business layer) are represented as the three Project files within the overall Solution, [Com.Natoma.Adpq.Prototype.Web](https://github.com/NatomaTechnologies/ADPQ2/tree/master/src/Com.Natoma.Adpq.Prototype.Web), [Com.Natoma.Adpq.Prototype.Api](https://github.com/NatomaTechnologies/ADPQ2/tree/master/src/Com.Natoma.Adpq.Prototype.Api), and [Com.Natoma.Adpq.Prototype.Business](https://github.com/NatomaTechnologies/ADPQ2/tree/master/src/Com.Natoma.Adpq.Prototype.Business) respectively. Each project follows a standard approach to project structure according to the nature of the project. Additionally, the application and business projects have corresponding test projects. The web project does not have a test project as most of the runnable code is generated from TypeScript and the rest (e.g. the [HomeController](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Web/Controllers/HomeController.cs)) do not contain sufficient complexity to warrent their own automated tests.

## Code Flow
Please note that in the following code flow walkthrough, several .ts files are identified as controlling certain behaviors. These files are not used directly by the deployed application. Instead, these files are used by TypeScript to generate .js files which are deployed to the browser and control client-side behavior with only minimal interaction with the server.  The generated .js files are not part of the code base in GitHub as they would be duplicative and confusing.  While the .ts files are specified in the walkthrough, they merely represent their corresponding .js files as these files are not available in GitHub.  When the application is built the js files are generated from the compilation of .ts files (TypeScript classes)

When a user first connects to the website, he or she is able to log in or create a new user. The initial web page is the static [login.component.html](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Web/wwwroot/html/login.component.html) from the Web project which has been skinned using the [standard css](https://github.com/NatomaTechnologies/ADPQ2/tree/master/src/Com.Natoma.Adpq.Prototype.Web/wwwroot/css), uses [standard images](https://github.com/NatomaTechnologies/ADPQ2/tree/master/src/Com.Natoma.Adpq.Prototype.Web/wwwroot/images) and is controlled by JavaScript that is the result of [compiled TypeScript](https://github.com/NatomaTechnologies/ADPQ2/tree/master/src/Com.Natoma.Adpq.Prototype.Web/App). This generated JavaScript uses Angular2, and other open source tools (MVC, bootstrap, PrimeNG, etc.) to present UI changes on a page, manage interactions with the API tier, and manage page-specific application data within the browser.

Some actions, such as making the signup section of the login page visible is controlled solely in the browser by methods like doMakeSignupVisible.  This method alters the isSignupVisible variable which, through the component lifecycle of Angular2, triggers a change in the [corresponding HTML page](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Web/wwwroot/html/login.component.html).

Other actions within the UI, such as a successful log in, will result in navigation within the website.  This example behavior can be seen in routeUser method of the [login.component.ts file](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Web/App/login.component.ts) where the user will be routed to the admin or user component (home page) based on the user type. Routes are specified in [app.routing.ts](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Web/App/app.routing.ts) and map to specific components.  For instance, if the user is an administrator (isAdmin is true), the router (an Angular 2 construct) is told to navigate to the route “./admin”.  Per app.routing.ts, this route maps to the AdminHomeComponent which, by Angular 2 convention refers to the [admin-home.component](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Web/App/admin/admin-home.component.ts). For security reasons, this component does not trust that the user is really an administrator and first verifies against the user service that the user truly is an administrator before presenting the [admin-home.component.html](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Web/wwwroot/html/admin-home.component.html).  If it is determined that the user is not truly an administrator, the system navigates to the “./user” route instead.

The verification of the user’s status in the previous step is performed through the userService which is as seen in the import section is [user.service.ts](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Web/App/user/user.service.ts). The getLoggedInUser method asynchronously retrieves the user ID from the Angular 2 cookie service and, assuming the user has not timed out or otherwise logged out, gets the user’s information by making a call to the API.  More specifically, getLoggedInUser calls getUserInfo which uses the [auth.service](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Web/App/shared/auth.service.ts) to perform its authGet behavior which makes a HTTP call to api/UserProfile. (Note: This is passed as a parameter in the updateUserInfo method of user.service.ts to authService. Once in auth.service, it is seen as the url parameter.)

The [UserProfileController](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Api/Controllers/UserProfileController.cs) in the API project handles the request to api/UserProfile.  Since the request is a RESTful HTTP get and provides an ID, the Get method handles the request.  This get method first verifies that the requestor is properly authorized using Microsoft’s authorization tools and, assuming the requestor is successfully verified, the controller fires a Get request to the user profile service.  The user profile service, which is provided to the controller through constructor-based dependency injection, is a business service described by the [user profile service interface](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Business/Services/Interfaces/IUserProfileService.cs) and implemented by the [user profile service](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Business/Services/UserProfileService.cs). 

This get method uses Microsoft’s entity framework to return the first record from the database that matches the user ID provided by the cookie service. As UserId is defined as the primary key of the user table as seen at the end of CREATE TABLE public.”User” in the [database create script](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Api/DB/adpq_db_create_postgres.sql) no more than one record will ever be returned from this query.  If no matching record is found, null is returned.  Otherwise, the EntityFramework user is translated into a [UserProfileViewModel](https://github.com/NatomaTechnologies/ADPQ2/blob/master/src/Com.Natoma.Adpq.Prototype.Business/Models/UserProfile/UserProfileViewModel.cs) and returned.

This final UserProfileViewModel is passed back from the business service through the controller to the TypeScript user service which, in turn, returns it to the admin home component.

The code flow described here exemplifies the majority of transactions performed by the prototype application. Other transactions perform actions such as sending email or text are similar but end with calls to SMTP or SMS services (respectively) instead of calls to a database.

--------------------end of the technical approach---------------------------------


## Natoma Response to the RFI Requirements
In addition to Natoma’s Working Prototype and the Technical Approach, we are providing the following information to demonstrate that we have met the requirements as documented in Attachment 1 Section 2 Technical Approach for conformance to the US Digital Services Playbook and inclusion of Requirements A through T in the RFI.

### Natoma Conformed to the US Digital Services Playbook
“The vendor must demonstrate that they followed the US Digital Services Playbook by providing evidence in the repository.”  See our response to the 13 Plays from the Digital Services Playbook below.


### PLAY 1 UNDERSTAND WHAT PEOPLE NEED
We must begin digital projects by exploring and pinpointing the needs of the people who will use the service, and the ways the service will fit into their lives. Whether the users are members of the public or government employees, policy makers must include real people in their design process from the beginning. The needs of people — not constraints of government structures or silos — should inform technical and design decisions. We need to continually test the products we build with real people to keep us honest about what is important

#### Checklist
1.	Early in the project, spend time with current and prospective users of the service
2.	Use a range of qualitative and quantitative research methods to determine people’s goals, needs, and behaviors; be thoughtful about the time spent
3.	Test prototypes of solutions with real people, in the field if possible
4.	Document the findings about user goals, needs, behaviors, and preferences
5.	Share findings with the team and agency leadership
6.	Create a prioritized list of tasks the user is trying to accomplish, also known as "user stories"
7.	As the digital service is being built, regularly test it with potential users to ensure it meets people’s needs

#### Natoma’s Response to Key Questions
1.	Who are your primary users?

    California residents from across the State. 

2.	What user needs will this service address?

    California residents can register to receive email and/or text notifications of emergency or non-emergency events based on their selected location within the state of California.

3.	Why does the user want or need this service?

    California users want to know of events or situations such as heavy rain, fog, flooding, fires, large traffic jams, police action, etc that may impact their homes, schools, work places, travel destinations, commute, etc.

4.	Which people will have the most difficulty with the service?

    People who are not used to or comfortable using internet-based services. Easy registration and sign up is necessary.

5.	Which research methods were used?

    End user interviews and internet-based research around emergency notification systems with focus on the usability of the system and   the technologies that support it.

6.	What were the key findings?

    For purposes of the Working Prototype, the Natoma Product Manager determined that the Prototype requires two basic functions/epics: the ability to establish a user profile based on alert notification types and location; and, the ability to create the notifications. 

7.	How were the findings documented? Where can future team members access the documentation?

    The findings of the interviews and research were documented as epics and user stories within Pivotal Tracker. The Product Manager provided team members with access to Pivotal Tracker, as needed, to review the user stories. Future team members requires access to the Pivotal Tracker project.

8.	How often are you testing with real people?

    Natoma team members conducted testing with each sprint. 

### PLAY 2 ADDRESS THE WHOLE EXPERIENCE, FROM START TO FINISH

We need to understand the different ways people will interact with our services, including the actions they take online, through a mobile application, on a phone, or in person. Every encounter — whether it's online or offline — should move the user closer towards their goal.

#### Checklist

1.	Understand the different points at which people will interact with the service – both online and in person
2.	Identify pain points in the current way users interact with the service, and prioritize these according to user needs
3.	Design the digital parts of the service so that they are integrated with the offline touch points people use to interact with the service
4.	Develop metrics that will measure how well the service is meeting user needs at each step of the service

#### Natoma’s Response to Key Questions
1.	What are the different ways (both online and offline) that people currently accomplish the task the digital service is designed to help with?

    California residents currently receive alert notifications via television, radio, internet alerts, mobile phone-based web applications for weather and traffic, police alerts, and via word of mouth. 

2.	Where are user pain points in the current way people accomplish the task?

    Alert notifications are usually driven by the type of alert requested – weather alerts are given on weather-based programs or weather-applications; traffic alerts are given on the radio or television in increments requiring the user to monitor them or through traffic-based application. Users are required to monitor or subscribe to a wide variety of services to stay informed of emergencies or other events.

3.	Where does this specific project fit into the larger way people currently obtain the service being offered?

    The Working Prototype, if further developed, would be an internet-based web application allowing users to sign up to receive alert notifications for events based on their choice of events and their selected location via email or mobile text messaging.

4.	What metrics will best indicate how well the service is working for its users?

    The diversity of alert notifications available through the services. The number of users registered to receive the service and performance metrics indicating the time between notification of an emergency event and distribution of the notification to end users.



### PLAY 3 MAKE IT SIMPLE AND INTUITIVE
Using a government service shouldn’t be stressful, confusing, or daunting. It’s our job to build services that are simple and intuitive enough that users succeed the first time, unaided.
#### Checklist
1.	Use a simple and flexible design style guide for the service. Use the U.S. Web Design Standards as a default
2.	Use the design style guide consistently for related digital services
3.	Give users clear information about where they are in each step of the process
4.	Follow accessibility best practices to ensure all people can use the service
5.	Provide users with a way to exit and return later to complete the process
6.	Use language that is familiar to the user and easy to understand
7.	Use language and design consistently throughout the service, including online and offline touch points

#### Natoma’s Response to Key Questions
1.	What primary tasks are the user trying to accomplish?

    Establish a web-based application that allows users to 1) register to participate 2) create a user login 3) create a user profile that includes selecting the medium for receiving the alert (email, text) providing location, providing phone numbers for text messages, providing an email address. 4) Establishing an alert notification process 5) Identifying open source based technologies on which this system can operate

2.	Is the language as plain and universal as possible?

    The Natoma team performed multiple reviews with the end user to validate that the language used in the prototype is minimal and straight forward.

3.	What languages is your service offered in?

    Natoma’s Working Prototype was built in standard American English. The team identified and created a user story requesting Spanish translation. This was deemed unnecessary to meet the MVP and was placed in the Icebox for future consideration.

4.	If a user needs help while using the service, how do they go about getting it?

    The Working Prototype provides an email address allowing users to contact the application administrator. The team identified and created a user story requesting a Frequently Asked Questions (FAQ) page. This was deemed unnecessary to meet the MVP and was placed in the Icebox for future consideration.

5.	How does the service’s design visually relate to other government services?  

    Natoma looked at the following government-sponsored web sites supporting alert notications:

    http://www.caloes.ca.gov/cal-oes-divisions/warning-center

    https://www.fcc.gov/consumers/guides/emergency-alert-system-eas

    http://www.calalerts.org/

    The service offered by the Working Prototype would be unique to existing State services but the language and visual design of the sites listed here were considered within the Prototype design. If the Prototype were being built for a known State agency, Natoma review that agency’s existing web pages and use established standards related to look and feel.

### PLAY 4 BUILD THE SERVICE USING AGILE AND ITERATIVE PRACTICES
We should use an incremental, fast-paced style of software development to reduce the risk of failure. We want to get working software into users’ hands as early as possible to give the design and development team opportunities to adjust based on user feedback about the service. A critical capability is being able to automatically test and deploy the service so that new features can be added often and be put into production easily.


#### Checklist
1.	Ship a functioning “minimum viable product” (MVP) that solves a core user need as soon as possible, no longer than three months from the beginning of the project, using a “beta” or “test” period if needed
2.	Run usability tests frequently to see how well the service works and identify improvements that should be made
3.	Ensure the individuals building the service communicate closely using techniques such as launch meetings, war rooms, daily standups, and team chat tools
4.	Keep delivery teams small and focused; limit organizational layers that separate these teams from the business owners
5.	Release features and improvements multiple times each month
6.	Create a prioritized list of features and bugs, also known as the “feature backlog” and “bug backlog”
7.	Use a source code version control system
8.	Give the entire project team access to the issue tracker and version control system
9.	Use code reviews to ensure quality


#### Natoma’s Response to Key Questions

1.	How long did it take to ship the MVP? If it hasn't shipped yet, when will it?

    The MVP was deployed after the first sprint – three (3) days 

2.	How long does it take for a production deployment?

    Natoma Integrated GitHub and the Jenkins GitHub web hook so that GitHub commits kick off Jenkins builds, run tests and updates/deploys of the Docker Hub Images to DockerHub. We used Ansible to deploy Dockerhub images to our working and production environments based on the environment specific configurations. Once automation was established, production deployment occurred within minutes.

3.	How many days or weeks are in each iteration/sprint?

    Five (5) days.  

2.	Which version control system is being used?

    Natoma used GitHub to maintain version control using a Team Foundation Server plugin. 

3.	How are bugs tracked and tickets issued? What tool is used?

    Each user story was entered into GitHub created with acceptance criteria that were then translated into test cases. The Natoma usability tester executed the tests cases within the prototype and tracked defects also within GitHub. Defects/bugs identified are created in TFS, triaged, assigned priority and severity, and tracked to completion.  Test cases were then automated for continued reuse with follow on sprints.

4.	How is the feature backlog managed? What tool is used?

    Features are written as user stories, which are entered into Pivotal Tracker as User Stories.  During Backlog Grooming the Product Owner prioritized the stories, and during Sprint Planning, the team moved the User Stories that were committed to into the Sprint Backlog and kept the remaining in the Product Backlog or Icebox.
  
5.	How often do you review and reprioritize the feature and bug backlog?

    At the end of each Sprint. Prototype development used five (5) day sprints.
 
6.	How do you collect user feedback during development? How is that feedback used to improve the service?

    In addition to scheduling reviews with subject matter experts that approximate users, the prototype was made continuously available to the entire Natoma team who were instructed to utilize all features of the system to evaluate quality and completeness based on the MVP. The team then provided suggestions for improvement in the form of new user stories that were either incorporated into the backlog for the following sprint or added to the icebox for future consideration.
   
7.	At each stage of usability testing, which gaps were identified in addressing user needs?

    Two gaps were identified. 1) Provide a mechanism for forgotten passwords. 2) Create a mechanism to use existing external data for existing notifications. The Project Director determined that these two gaps could be developed as future enhancements to the product since they are not necessary for MVP.  The user stories were added to the Icebox. 

### PLAY 5 STRUCTURE BUDGETS AND CONTRACTS TO SUPPORT DELIVERY
To improve our chances of success when contracting out development work, we need to work with experienced budgeting and contracting officers. In cases where we use third parties to help build a service, a well-defined contract can facilitate good development practices like conducting a research and prototyping phase, refining product requirements as the service is built, evaluating open source alternatives, ensuring frequent delivery milestones, and allowing the flexibility to purchase cloud computing resources.
The TechFAR Handbook provides a detailed explanation of the flexibilities in the Federal Acquisition Regulation (FAR) that can help agencies implement this play.

#### Checklist
1.	Budget includes research, discovery, and prototyping activities
2.	Contract is structured to request frequent deliverables, not multi-month milestones
3.	Contract is structured to hold vendors accountable to deliverables
4.	Contract gives the government delivery team enough flexibility to adjust feature prioritization and delivery schedule as the project evolves
5.	Contract ensures open source solutions are evaluated when technology choices are made
6.	Contract specifies that software and data generated by third parties remains under our control, and can be reused and released to the public as appropriate and in accordance with the law
7.	Contract allows us to use tools, services, and hosting from vendors with a variety of pricing models, including fixed fees and variable models like “pay-for-what-you-use” services
8.	Contract specifies a warranty period where defects uncovered by the public are addressed by the vendor at no additional cost to the government
9.	Contract includes a transition of services period and transition-out plan


#### Natoma’s Response to Key Questions

1.	What is the scope of the project? What are the key deliverables?
2.	What are the milestones? How frequent are they?
3.	What are the performance metrics defined in the contract (e.g., response time, system uptime, time period to address priority issues)?

Natoma’s Working Prototype is being submitted in response to a Request for Information (RFI) proposal There are no contractual requirements associated with this effort. As such, this Play does not apply.

### PLAY 6 ASSIGN ONE LEADER AND HOLD THAT PERSON ACCOUNTABLE 
There must be a single product owner who has the authority and responsibility to assign tasks and work elements; make business, product, and technical decisions; and be accountable for the success or failure of the overall service. This product owner is ultimately responsible for how well the service meets needs of its users, which is how a service should be evaluated. The product owner is responsible for ensuring that features are built and managing the feature and bug backlogs.

#### Checklist
1.	A product owner has been identified
2.	All stakeholders agree that the product owner has the authority to assign tasks and make decisions about features and technical implementation details
3.	The product owner has a product management background with technical experience to assess alternatives and weigh tradeoffs
4.	The product owner has a work plan that includes budget estimates and identifies funding sources
5.	The product owner has a strong relationship with the contracting officer

#### Natoma’s Response to Key Questions
1.	Who is the product owner?

    Veronica Westlund, Vice President COO of Natoma Technologies, Inc.  

2.	What organizational changes have been made to ensure the product owner has sufficient authority over and support for the project?

    As Vice President and COO of Natoma, the Product Owner has sufficient authority over the Working Prototype.

3.	What does it take for the product owner to add or remove a feature from the service?

    The Product Owner determined the Minimum Viable Product requirements or features with the initiation of the project. The project team working with an identified user community defined user stories. The Product Owner mapped user stories to product epics creating the MVP. She then evaluated additional user stories to determine if they resulted in additional features to be added to the Prototype based on the impact to MVP.


### PLAY 7 BRING IN EXPERIENCED TEAMS
We need talented people working in government who have experience creating modern digital services. This includes bringing in seasoned product managers, engineers, and designers. When outside help is needed, our teams should work with contracting officers who understand how to evaluate third-party technical competency so our teams can be paired with contractors who are good at both building and delivering effective digital services. The makeup and experience requirements of the team will vary depending on the scope of the project.

#### Checklist
1.	Member(s) of the team have experience building popular, high-traffic digital services
2.	Member(s) of the team have experience designing mobile and web applications
3.	Member(s) of the team have experience using automated testing frameworks
4.	Member(s) of the team have experience with modern development and operations (DevOps) techniques like continuous integration and continuous deployment
5.	Member(s) of the team have experience securing digital services
6.	A Federal contracting officer is on the internal team if a third party will be used for development work
7.	A Federal budget officer is on the internal team or is a partner
8.	The appropriate privacy, civil liberties, and/or legal advisor for the department or agency is a partner

#### Natoma’s Response
Natoma chose a multidisciplinary team of professionals who have worked together on Natoma projects in the past to build the Working Prototype. The Project Director, Technical Architect, Back End developer and DevOps Engineer worked on a Natoma project sponsored by the State of Nevada Health Exchange helping to create a popular high traffic digital service. The Technical Architect and the DevOps Engineer worked on the State of California Department of Finance to develop their budgeting system and the State of California Air Resources Board develop their Air Resources Board Equipment Registration (ARBER) system – both frequented digital systems. These Natoma team members also supported the development of web applications for these projects along with the Port of Los Angeles Truck Registry and the California Healthcare Event and Reporting Tool (CalHEART) for the California Department of Public Health. The project director assisted the California Department of Veterans Affiars with the development of their mobile application (CalVet app). Our Technical Architect is experienced using XUnit, NUnit, Capybara and Rspec, automated testing frameworks. He is also supporting the California Digital Web Services Intake Project that is using Scrum-based modern development and DevOps techniques including continuous integration, test driven development and continuous deployment. Our team is familiar with the processes and procedures necessary for securing digital services. In addition to the othe projects listed here, our team members also worked on the State of California Secretary of State’s VoteCal Voter Registration System that has been deployed in all 58 counties within California and integrated within other California State agencies.

Since the Working Prototype is not a federal effort, Natoma did not include federal officers and did not require privacy, civil liberties or legal advisor considerations and DevOps techniques including continuous integration and continuous deployment. 



#### PLAY 8 CHOOSE A MODERN TECHNOLOGY STACK

The technology decisions we make need to enable development teams to work efficiently and enable services to scale easily and cost-effectively. Our choices for hosting infrastructure, databases, software frameworks, programming languages and the rest of the technology stack should seek to avoid vendor lock-in and match what successful modern consumer and enterprise software companies would choose today. In particular, digital services teams should consider using open source, cloud-based, and commodity solutions across the technology stack, because of their widespread adoption and support by successful consumer and enterprise technology companies in the private sector.

#### Checklist
1.	Choose software frameworks that are commonly used by private-sector companies creating similar services
2.	Whenever possible, ensure that software can be deployed on a variety of commodity hardware types
3.	Ensure that each project has clear, understandable instructions for setting up a local development environment, and that team members can be quickly added or removed from projects
4.	Consider open source software solutions at every layer of the stack

#### Natoma’s Response to Key Questions


1.	What is your development stack and why did you choose it?

    Natoma selected the .NET core development stack with Angular 2. We chose this stack because we are aware that the State of California has already made a significant investment into .NET technologies and will likely be in a better position to support this technology stack than others.  This is also a stack in which Natoma has significant experience so we were able to field a team with the appropriate experience quickly for purposes of the Working Prototype response.

2.	Which databases are you using and why did you choose them?

    Natoma is using PostgreSQL for the project database. We are aware that the State of California is using it on other projects and it is easily installed using published Docker images.  Additionally, the Natoma team has experience with PostgreSQL allowing us to rapidly build out the Working Prototype team.

3.	How long does it take for a new team member to start developing?

    Natoma selected team members from within the Natoma community already familiar with the technology stacks and had worked together in the past as such, each developer on the team was able to start developing from day one.

### PLAY 9 DEPLOY IN A FLEXIBLE HOSTING ENVIRONMENT
Our services should be deployed on flexible infrastructure, where resources can be provisioned in real-time to meet spikes traffic and user demand. Our digital services are crippled when we host them in data centers that market themselves as “cloud hosting” but require us to manage and maintain hardware directly. This outdated practice wastes time, weakens our disaster recovery plans, and results in significantly higher costs.

#### Checklist
1.	Resources are provisioned on demand
2.	Resources scale based on real-time user demand
3.	Resources are provisioned through an API
4.	Resources are available in multiple regions
5.	We only pay for resources we use
6.	Static assets are served through a content delivery network
7.	Application is hosted on commodity hardware

#### Natoma’s Response to Key Questions
1.	Where is your service hosted?

    Amazon Web Services.

2.	What hardware does your service use to run?

    The hardware is abstracted by the cloud services used and that information is not provided by the service. The service runs on a virtual machine.  

3.	What is the demand or usage pattern for your service?

    This question does not apply to the Working Prototype.

4.	What happens to your service when it experiences a surge in traffic or load?

    This question does not apply to the Working Prototype.

5.	How much capacity is available in your hosting environment?

    Amazon’s cloud services are scaleable based on capacity. This does not apply to the Working Prototype.

6.	How long does it take you to provision a new resource, like an application server?

    Approximately five (5) minutes to provision a new Windows application server.

7.	How have you designed your service to scale based on demand?

    This question does not apply to the Working Prototype.

8.	How are you paying for your hosting infrastructure (e.g., by the minute, hourly, daily, monthly, fixed)?

    By the minute although AWS offers a fixed cost if reserving for a minimum of one year.

9.	Is your service hosted in multiple regions, availability zones, or data centers?

    No. This question does not apply to the Working Prototype.

10.	In the event of a catastrophic disaster to a datacenter, how long will it take to have the service operational?

    This question does not apply to the Working Prototype.

11.	What would be the impact of a prolonged downtime window?

    This question does not apply to the Working Prototype.

12.	What data redundancy do you have built into the system, and what would be the impact of a catastrophic data loss?

    This question does not apply to the Working Prototype. For the purposes of this prototype data, redundancy was not within scope.
  
13.	How often do you need to contact a person from your hosting provider to get resources or to fix an issue?

    This question does not apply to the Working Prototype.

### PLAY 10 AUTOMATE TESTING AND DEPLOYMENTS
Today, developers write automated scripts that can verify thousands of scenarios in minutes and then deploy updated code into production environments multiple times a day. They use automated performance tests which simulate surges in traffic to identify performance bottlenecks. While manual tests and quality assurance are still necessary, automated tests provide consistent and reliable protection against unintentional regressions, and make it possible for developers to confidently release frequent updates to the service.

#### Checklist
1.	Create automated tests that verify all user-facing functionality
2.	Create unit and integration tests to verify modules and components
3.	Run tests automatically as part of the build process
4.	Perform deployments automatically with deployment scripts, continuous delivery services, or similar techniques
5.	Conduct load and performance tests at regular intervals, including before public launch


#### Natoma’s Response to Key Questions
1.	What percentage of the code base is covered by automated tests?

    Natoma estimates that 75% of the code is covered by automated testing.

2.	How long does it take to build, test, and deploy a typical bug fix?

    Less than 10 minutes with deployment automation.

3.	How long does it take to build, test, and deploy a new feature into production?

    Less than 10 minutes with deployment automation

4.	How frequently are builds created?

    Builds are created with each check in of code. This occurred on a daily basis throughout Prototype development.

5.	What test tools are used?

    The following test tools are used:
        Unit testing / Unit integration testing are supported by Moq and XUnit
        GitHub using the Jenkins GitHub web hook so that GitHub commits kick off Jenkins builds, run tests and updates/deploys of the Docker Hub Images to DockerHub.   

6.	Which deployment automation or continuous integration tools are used?

    GitHub and the Jenkins GitHub web hook so that GitHub commits kick off Jenkins builds, run tests and updates/deploys of the Docker Hub Images to DockerHub. We used Ansible to deploy Dockerhub images to our working and production environments based on the environment specific configurations.  
 
7.	What is the estimated maximum number of concurrent users who will want to use the system?

    Natoma’s Working Prototype is anticipated to have ten or fewer concurrent users.  

8.	How many simultaneous users could the system handle, according to the most recent capacity test?

    This prototype does not require formal capacity testing. Informal testing demonstrated that five concurrent users resulted in no performance degradation.  

9.	How does the service perform when you exceed the expected target usage volume? Does it degrade gracefully or catastrophically?

    This question does not apply to the Working Prototype.

10.	What is your scaling strategy when demand increases suddenly?

    This question does not apply to the Working Prototype.

### PLAY 11 MANAGE SECURITY AND PRIVACY THROUGH REUSABLE PROCESSES 
Our digital services have to protect sensitive information and keep systems secure. This is typically a process of continuous review and improvement which should be built into the development and maintenance of the service. At the start of designing a new service or feature, the team lead should engage the appropriate privacy, security, and legal officer(s) to discuss the type of information collected, how it should be secured, how long it is kept, and how it may be used and shared. The sustained engagement of a privacy specialist helps ensure that personal data is properly managed. In addition, a key process to building a secure service is comprehensively testing and certifying the components in each layer of the technology stack for security vulnerabilities, and then to re-use these same pre-certified components for multiple services.
The following checklist provides a starting point, but teams should work closely with their privacy specialist and security engineer to meet the needs of the specific service.

#### Checklist
1.	Contact the appropriate privacy or legal officer of the department or agency to determine whether a System of Records Notice (SORN), Privacy Impact Assessment, or other review should be conducted
2.	Determine, in consultation with a records officer, what data is collected and why, how it is used or shared, how it is stored and secured, and how long it is kept
3.	Determine, in consultation with a privacy specialist, whether and how users are notified about how personal information is collected and used, including whether a privacy policy is needed and where it should appear, and how users will be notified in the event of a security breach
4.	Consider whether the user should be able to access, delete, or remove their information from the service
5.	“Pre-certify” the hosting infrastructure used for the project using FedRAMP
6.	Use deployment scripts to ensure configuration of production environment remains consistent and controllable


#### Natoma’s Response to Key Questions

1.	Does the service collect personal information from the user? How is the user notified of this collection?

    In order to receive alert notifications, user musts enter information regarding phone number, email address and location. The application includes a statement telling users that the purpose of the application is to obtain user emails and phone numbers in order to provide them with the alert notifications requested.

2.	Does it collect more information than necessary? Could the data be used in ways an average user wouldn't expect?

    The application only collects that information necessary to geocode their address in order to send requested notifications.  The data collected cannot be used in ways the average user would not expect.

3.	How does a user access, correct, delete, or remove personal information?

    A user can access the application with a valid user id and password and modify user information to remove personal details.  The Working Prototype does not include a delete account option at this time since it is not necessary for the MVP but users can overwrite existing information with non-personally identifiable data.

4.	Will any of the personal information stored in the system be shared with other services, people, or partners?

    No, personal information will not be shared with other services, people or partners.  However, email and mobile phone (if provided) data will be used by email and text services.

5.	How and how often is the service tested for security vulnerabilities?

    Natoma performed limited security testing for purposes of the Working Prototype given the limited nature of the development effort.

6.	How can someone from the public report a security issue?

    Yes, the site includes an email address for users to report any issues.


### PLAY 12 USE DATA TO DRIVE DECISIONS

At every stage of a project, we should measure how well our service is working for our users. This includes measuring how well a system performs and how people are interacting with it in real-time. Our teams and agency leadership should carefully watch these metrics to find issues and identify which bug fixes and improvements should be prioritized. Along with monitoring tools, a feedback mechanism should be in place for people to report issues directly.

#### Checklist
1.	Monitor system-level resource utilization in real time
2.	Monitor system performance in real-time (e.g. response time, latency, throughput, and error rates)
3.	Ensure monitoring can measure median, 95th percentile, and 98th percentile performance
4.	Create automated alerts based on this monitoring
5.	Track concurrent users in real-time, and monitor user behaviors in the aggregate to determine how well the service meets user needs
6.	Publish metrics internally
7.	Publish metrics externally
8.	Use an experimentation tool that supports multivariate testing in production

#### Key Questions
1.	What are the key metrics for the service?  

    This question does not apply to the Working Prototype.

2.	How have these metrics performed over the life of the service?

    This question does not apply to the Working Prototype.

3.	Which system monitoring tools are in place?

    Natoma has deployed DataDog to continuously monitor the availability of the prototype via ‘ping’ results.  Were this a high availability, high criticality, application Natoma would recommend monitoring the vitals (processor, memory, disk space) of the servers in each tier in addition to monitoring the network health.  However, server and AWS alerts can be added for this level of monitoring as well.

4.	What is the targeted average response time for your service? What percent of requests take more than 1 second, 2 seconds, 4 seconds, and 8 seconds?

    The Working Prototype does not require response time tracking.
    
5.	What is the average response time and percentile breakdown (percent of requests taking more than 1s, 2s, 4s, and 8s) for the top 10 transactions?

    Response time testing was not required for the Working Prototype. 

6.	What is the volume of each of your service’s top 10 transactions? What is the percentage of transactions started vs. completed?

    This question does not apply to the Working Prototype.

7.	What is your service’s monthly uptime target?

    This question does not apply to the Working Prototype.

8.	What is your service’s monthly uptime percentage, including scheduled maintenance? Excluding scheduled maintenance?

    This question does not apply to the Working Prototype.

9.	How does your team receive automated alerts when incidents occur?

     Natoma has deployed DataDog to continuously monitor the availability of the prototype via ‘ping’ results.  Were this a high availability, high criticality, application Natoma would recommend monitoring the vitals (processor, memory, disk space) of the servers in each tier in addition to monitoring the network health.  However, server and AWS alerts can be added for this level of monitoring as well.

10.	How does your team respond to incidents? What is your post-mortem process?

    Natoma has deployed DataDog to continuously monitor the availability of the prototype via ‘ping’ results.  Were this a high availability, high criticality, application Natoma would recommend monitoring the vitals (processor, memory, disk space) of the servers in each tier in addition to monitoring the network health.  However, server and AWS alerts can be added for this level of monitoring as well.

11.	Which tools are in place to measure user behavior?

    Natoma has integrated Google Analytics into the prototype.

12.	What tools or technologies are used for A/B testing?

    A/B testing has not been performed, Natoma has integrated Google Analytics into the prototype and this would support gathering analytics during A/B testing, if used.  

13.	How do you measure customer satisfaction?

    This question does not apply to the Working Prototype.

####PLAY 13 DEFAULT TO OPEN

When we collaborate in the open and publish our data publicly, we can improve Government together. By building services more openly and publishing open data, we simplify the public’s access to government services and information, allow the public to contribute easily, and enable reuse by entrepreneurs, nonprofits, other agencies, and the public.

### Checklist
1.	Offer users a mechanism to report bugs and issues, and be responsive to these reports
2.	Provide datasets to the public, in their entirety, through bulk downloads and APIs (application programming interfaces)
3.	Ensure that data from the service is explicitly in the public domain, and that rights are waived globally via an international public domain dedication, such as the “Creative Commons Zero” waiver
4.	Catalog data in the agency’s enterprise data inventory and add any public datasets to the agency’s public data listing
5.	Ensure that we maintain the rights to all data developed by third parties in a manner that is releasable and reusable at no cost to the public
6.	Ensure that we maintain contractual rights to all custom software developed by third parties in a manner that is publishable and reusable at no cost
7.	When appropriate, create an API for third parties and internal users to interact with the service directly
8.	When appropriate, publish source code of projects or components online
9.	When appropriate, share your development process and progress publicly

Key Questions

1.	How are you collecting user feedback for bugs and issues?

    End-users have the ability to provide feedback directly through a link on the About pages and on the Error page.  In addition, the site records page-by-page activity via Google Analytics and analysis of these metrics can provide insight into difficulties experienced by end-users.  

2.	If there is an API, what capabilities does it provide? Who uses it? How is it documented?

    The User Interface (UI) utilized an API to access the Prototype’s data access layer. It has been documented with Swagger.

3.	If the codebase has not been released under an open source license, explain why.

    This question does not apply. The codebase has been released to the GitHub directory as a public project.

4.	What components are made available to the public as open source?

    The entire project is available to the public as open source.

5.	What datasets are made available to the public?

    The Working Prototype application as currently envisioned is a data consumer and not a data provider.  As such, the datasets are not being made public at this time. The code is available to the public.

## Natoma’s ReadME.md File References the RFI Requirements
“The README.md file should also make reference to the following (items A through T in the RFI)”


## Requirement A. Assigned one leader and gave that person responsibility and held that person accountable for the quality of the prototype submitted. 

Natoma assigned Veronica Westlund, Chief Operations Officer (COO), as Product Manager for this Working Prototype development. As COO, Veronica has authority to make decisions regarding product features and technical implementation details as they relate to Natoma released products. Veronica has provided Technical Project Management and has served as a Technical Architect for Natoma for more than eight years. Upon review of the RFI, Veronica assembled a team of professionals knowledgeable in the design, development and release of digital services to recommend suitable technologies that have resulted in an intuitive and simple solution for the user community. Veronica then lead the team in the solicitation of user stories extracted from potential end-users and worked with the team to build the prototype using a Scrum-based approach to Working Prototype. Veronica had responsibility for negotiating user stories to ensure that functionality resulted in the creation of a minimum viable product while meeting the requirements of the RFI. 


## Requirement B. Assembled a multi-disciplinary and collaborative team that includes at a minimum five (5) of the labor categories as identified in Attachment B: PQVP DS-AD Labor Category Descriptions.

Natoma’s assembled a multi-disciplinary team who participated in daily scrum sessions and utilized Pivotal Tracker, SharePoint, GitHub and other collaborative tools to develop the Working Prototype. Note that some of the Natoma team members performed dual roles during development. Labor categories and participating resources:

Labor Category 1. Product Manager - Veronica Westlund 

Labor Category 2. Technical Architect - Thomas Ramirez

Labor Category 3. Interaction Designer/User Researcher/Usability Tester -  Eamonn Guiney 

Labor Category 4. Writer/Content Designer/Content Strategist – Veronica Westlund

Labor Category 6. Front End Web Developer -  Thomas Bertan

Labor Category 7. Backend Web Developer – Brian Riley

Labor Category 8. DevOps Engineer – Jeff Drewes

Labor Category 9. Security Engineer - Thomas Ramirez

Labor Category 10. Delivery Manager – Lynn Jones 

Labor Category 11. Agile Coach – Eamonn Guiney

Labor Category 12. Business Analyst – Eamonn Guiney

## Requirement C. Understood what people needed by including people in the prototype development and design process. 

Natoma applied a User-Centered Design approach to the creation of our Working Prototype. This included:

1.	The Natoma Project Director and Agile Coach reviewed the working prototype statement from the RFI to better understand and deconstruct the requirements of the prototype into epics.
2.	The Natoma Project Director coalesced a team of technical professionals to discuss how the identified epics would be architected based on the RFI requirements for simplicity, ease of use and open source. 
3.	Natoma asked Izzie Litman, a Senior Business Analyst and Natoma associate, to assist the team as an end user.
4.	The Natoma Project Director added additional analyst resources to the Scrum team to work with Izzie in establishing end user stories defining emergency and non-emergency notification needs. 
5.	The Natoma Researcher and Business analyst conducted internet-based research on the concepts for establishing an emergency notification network looking at the business needs of the individuals requesting notification as well as the technologies supporting such systems. Additional questions resulted from this research were posed to the end user to refine user stories and establish acceptance criteria. 
6.	A project was created in Pivotal Tracker and the epics and user stories were added. 
7.	The Natoma team reviewed and prioritized user stories and determined the design of the prototype using agreed upon technologies.
8.	Using the minimal viable product (MVP) approach, the Product Manager identified user stories belonging in backlog and assigned work to the team based on the most critical elements needed to provide a working prototype.
9.	Once the prototype was tangible enough to produce a response, the team, including the end user, implemented role play. This included testing within our team in order to gain realistic and effective feedback for improvements.
10.	After receiving prototype feedback based on the role play, the team began integrating the feedback into the backlog as part of the next iteration of the prototype. Some of the identified user stories were created as icebox items to demonstrate that we are taking a MVP approach.

## Requirement D. Used at least a minimum of three (3) "user-centric design" techniques and/or tools.
Natoma used the following user-centric design techniques to develop the Working Prototype:

1.	Explicit understanding of users, tasks and environments. Natoma identified simulated end users to provide initial user stories for the creation of an alert notification system. The Natoma Researcher and Business Analyst performed internet-based research to better understand the capabilities, business needs and technology supporting existing alert notification systems and developed a series of detailed questions that were then posed to the end user. The end user stories were enhanced and further deconstructed into more focused user stories that included testable acceptance criteria. Technical criteria were also captured as user stories.

2.	Users were involved in the design and development approach. The process was driven by user-centered evaluation. The process was iterative. Natoma adopted a scrum-based iterative approach to the Prototype development using five day sprints. The Product Manager identified three epics to develop and user stories were prioritized based on their necessity to building functioning epics. The Natoma team conducted daily scrums where all aspects of the design, build and test were reviewed as a team. When the epic was built out enough to creating a working prototype, the whole team conducted role playing to review and refine the design and additional user stories were captured. The end user participated in this process. This effort was performed with each iteration.

3.	Design team included multidisciplinary skills. See Requirement B regarding Natoma’s multidisciplinary and collaborative team approach to development.

## Requirement E. Used GitHub to document code commits.
 
 Natoma used GitHub to document code commits. 
 
 https://github.com/NatomaTechnologies/ADPQ2/commits/master
 
 
 
## Requirements F. Used Swagger to document the RESTful API and provided a link to the Swagger API

Natoma used Swagger to document the RESTful API. 

http://adpq2prodapi.natomadev.com:5050/swagger/ 


## Requirement G. Complied with Section 508 of the Americans with Disability Act and WCAG 2.0

Natoma used WAVE application plug-in for ADA compliance. W3C HTML validator checks the markup validity of the HTML 5 and helps determine conformance to the robust guideline in WCAG 2.0. The WAVE ADA validator allows testing of dynamically generated content from scripts and is used to identify accessibility problems in screens, both as they are initially rendered and also as they are modified through JavaScript.  This helps determine conformance to the perceivable and operable guidelines in WCAG 2.0 as well as Section 508 subsections (a), (c), (d), (g), (h), and (n).

## Requirement H. Created or used a design style guide and/or pattern library

Natoma created a design style guide and used PrimeNG as a pattern library.  PrimeNG is a collection of UI components for Angular 2.  All widgets are open source and free to use under MIT License.

## Requirement I. Performed usability tests with people

The Natoma Usability Tester and Business Analyst conducted functional testing of the acceptance criteria within the user stories assigned to each epic. Defects were logged and retesting performed to validate fixes within the sprints. In addition, the entire Natoma team including the end user exercised the functionality built with each iteration to identify additional user stories that would improve application design and use.


## Requirement J. Used an iterative approach where feedback informed subsequent work or version of the prototype

Natoma adopted a Scrum approach for development of the Working Prototype The Natoma team conducted daily scrums where all aspects of the design, build and test were reviewed as a team. When the epic was built out enough to creating a working prototype, the whole team conducted role playing to review and refine the design and additional user stories were captured. The end user participated in this process. This effort was performed with each iteration.


## Requirement K. Created a prototype that works on multiple devices and presents a responsive design 
Natoma’s Working Prototype is designed to be accessible using the web browsers Chrome or Safari on laptops or mobile phones. It provides email notifications and mobile phones text messages.

## Requirement L. Used at leave five (5) modern and open-source technologies regardless of architectural layer (front end, backend, etc)
Natoma used the following open-source tools and technologies in the development the Working Prototype:
1.	.Net Core

2.	Angular 2

3.	ASP.Net Core MVC

4.	Swashbuckler

5.	Swagger

6.	Docker

7.	Ansible

8.	Jenkins

9.	TypeScript

10.	DataDog

11.	Wave tool for Chrome

12.	PostgreSQL


In addition, Natoma utilized a free tier version of the following tools: 

1.	PivotalTracker (free for public projects – free 30 day for private projects)

2.	Twilio (Free tier restricted SMS tool)

3.	GitHub (free for public projects, pay for private)

4.	DockerHub (free for public repositories, pay for private)

## Requirement M. Deployed the prototype on an Infrastructure as a Service (IaaS) or Platform as Service (PaaS) provider and indicated which provider they used

Natoma deployed the Working Prototype on Amazon Web Services – an Infrastructure as a Service provider.

## Requirement N. Developed automated unit tests for their code
Natoma automated unit testing through GitHub and the Jenkins GitHub web hook so that GitHub commits kick off Jenkins builds, run tests and updates/deploys of the Docker Hub Images to DockerHub.    

## Requirement O. Set up or used a continuous integration system to automated the running of tests and continuously deployed their code to the IaaS or PaaS provider

Natoma configured continuous integration, automated execution of unit testing and continuous deployment through the GitHub and the Jenkins GitHub web hook so that GitHub commits kick off Jenkins builds, run tests and updates/deploys of the Docker Hub Images to DockerHub and the IaaS.

## Requirement P. Setup or used configuration management
Natoma set up and used GitHub through a TFS plug in for configuration management.

## Requirement Q. Setup or used continuous monitoring
Natoma used DataDog to perform continuous monitoring.

## Requirement R. Deployed their software in an open source container such as Docker (i.e.: utilized operating system level virtualization)

Natoma deployed the Working Prototype using Docker Containers to deploy the software.

## Requirement S. Provided sufficient documentation to install and run their prototype on another machine

Yes, this documentation can be found in the GitHub directory and was used in the “production” installation of the Working Prototype provided to the State for review.

## Requirement T. Prototype and underlying platforms used to create and run the prototype are openly licensed and free of charge

As stated in Requirement L Used at least Five (5) open-source technologies, Natoma build the Working Prototype and it underlying platform utilizing tools and techniques that are either openly licensed are free of charge. See the list of tools in Requirement L above. 






[prototype-url]:<http://adpq2prodweb.natomadev.com:5000>
[logo]: </Doc/NT_Logo_rgb_tight_transparent.gif>
[pt-url]:<https://www.pivotaltracker.com/projects/1976013>
