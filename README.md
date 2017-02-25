# ADPQ2
![Natoma Technologies, Inc. Logo][logo]


# ADPQ Prototype

Natoma Technologies, Inc. (Natoma) is pleased to respond to the California Office of Systems Integration RFI #? (RFI) and is thankful for this opportunity to present our prototype and our approach. 

# [Link to the prototype web-application][prototype-url]  
Address: <<need to add this when available>> http://prototype.natomadev.com/HHSPrototype/


## In Brief

Natoma assembled a team, extracted RFI requirements, chose technologies, organized our stories into sprints, found people to fill-in for end-users to help us understand their needs and concerns, designed a prototype, built a little, tested with end-users, and repeated; while also building the website and supporting infrastructure.  

---
## Staffing 
We examined the RFI and determined the skills needed to deliver the prototype. Our multidisciplinary and collaborative team is led by Veronica Westlund, Vice President & COO, and includes:

| **Labor Category**                                            | **Individual**          |
|---------------------------------------------------------------|-------------------------|
|    Product Manager                                            |    Veronica Westlund    |
|    Technical Architect                                        |    Thomas Ramirez       |
|    Front End Web Developer                                    |    Bruce Longacre       |
|    Backend Web Developer                                      |    Thomas Ramirez       |
|    Security Engineer                                          |    Thomas Ramirez       |
|    Delivery Manager                                           |    Eamonn Guiney        |
|    DevOps Engineer                                            |    Veronica Westlund    |
|    Interaction Designer/User   Researcher/Usability Tester    |    Eamonn Guiney        |
|    Writer/Content Designer/Content Strategist                 |    Noelle Ilayan        |
|    Business Analyst                                           |    Noelle Ilayan        |

Given the prototype scope, we decided to omit the Visual Designer, Agile Coach, and Digital Performance Analyst.  We reached out to the community and had Yuri Kimura, a director at [Stanford Youth Solutions][sys-url], volunteer to help us understand our end-users.   

--- 

## Planning
The RFI defines the requirements for this prototype, we combed through it carefully and identified the functional and non-functional [requirements][req-doc], and became familiar with them. We considered our timeline and implemented **five-day** sprint durations.  

Next, we brainstormed suitable approaches to deliver the [requirements][req-doc] and wrote User [Stories][all-stories-dir] on Post-its. This session doubled as a Sprint Planning ceremony, during which we estimated story points and team capacity. We moved our Stories to be delivered during the Sprint into our Sprint Backlog and updated our [Kanban][url-kanban-dir] displayed in our workroom. Stories that we could not commit to delivering during the Sprint were stored in our Product Backlog.  This approach put the major work areas in parallel – see [diagram][approach-doc]. 

Being that our team is proficient in using Microsoft TFS, we decided to use it to manage user stories, code, and defects for the prototype.  We entered our user stories, and associated tasks to [those stories][initial-reqs-doc].   

---

## Prototype Development
### Initial Iteration
Our immediate need was to make the prototype available for improvement:
+ Drew [conceptual data model][conceptual-data-model-doc], realized the site usage data (metadata) would drive design decisions; incorporated site analytics to the [conceptual data model][conceptual-data-model-doc], and added a story for capturing analytic data to our [backlog][backlog-dir]. 
+ Brainstormed the web pages/components needed and their flow [Session1][Session1-dir] [Session2][Session2-dir]
+ Built the first iteration of the prototype as static html

### Subsequent Iterations
+ End-user feedback prioritized changes to user interface and behavior (see below)
+ Made [architecture decisions][architecture-doc]
+ Wrote [use cases][use-case-dir]
+ Built web, application, and persistence tiers
+ Converted static HTML to MVC4
+ Multiple iterations, made work-in-progress available to the entire team, collected end-user feedback

---
## Human Centered Design
The [initial prototype][prototype-V1-screenshots] facilitated Human Centered Design activities summarized below, with additional details [here][Human-Centered-Design-doc]: 
### Secondary Research
Applied internet research to gather context. This allowed us to gain a better understanding and develop interview questions

### Interview
[Interviewed Yuri Kimura][Yuri-Kimura-IV-doc], an individual working in the field, to gain further insight and understanding of the intended end-users. This included a preparation of both broad and specific questions, recording Yuri’s responses exactly as spoken, and communicating our interpretations upon completion of the interview for validation.  

### Determine what to prototype
Post-interview, we prioritized critical elements to build, test, and deliver.

### Role playing/ usability tests with people
Once our prototype was tangible enough to produce a response, we implemented role play. This included testing within our team in order to gain realistic and effective feedback for improvements.  

### Integrate feedback and iterate
After receiving prototype feedback, we were able to integrate the feedback into our work. This allowed us to refine our product and built the next iteration of our prototype.  Items that there was insufficient time to complete are documented in our latest [backlog extract][backlog-doc]

### Data Driven Decisions
Integrated Google Analytics to generate [usage data][google-analytics-doc] to guide future decision making. 

## US Digital Services Playbook
Team worked through [Playbook questions][Playbook-doc] and recorded decisions/answers.  

--- 

## Technology Decisions

We selected our toolset and platform for the prototype, for additional details refer to Play 8 in the [Playbook][Playbook-doc].   

| Tool                         | Tool Type                             | License                 |
|------------------------------|---------------------------------------|-------------------------|
| Visual Studio Community 2013 | IDE                                   | [Microsoft][MS-License] |
| MVC 4                        | UI framework                          | Apache                  |
| AutoMapper                   | Class mapping                         | Open Source             |
| Bootstrap                    | UI layout                             | MIT                     |
| Moq                          | Unit test support                     | BSD Clause 2            |
| Knockout                     | Javascript framework                  | MIT                     |
| jQuery                       | Javascript framework                  | MIT                     |
| Razor                        | HTML generation                       | Apache                  |
| TFS                          | Source control/continuous integration | Proprietary             |
| WAVE Plugin                  | ADA compliance checking               | Open Standards Based    |
| Chrome Vox                   | Screen reading checking               | Apache                  |

## Platform

| Item                         |  Category             | 
|------------------------------|-----------------------|
|    Amazon   Web Services     | Platform as a Service | 
|    Google   Analytics        | Usage Metadata        | 
|    Application   Insights    | Continuous Monitoring | 

### Notes:
-All items are free, or without cost for the duration of the prototype.  
-Prototype does **not** use a database; persistence is via XML files, see [architecture decisions][architecture-doc].   
-Prototype is built on Microsoft’s default style sheet.

---

## Testing
To ensure quality we:
+ Planned testing, wrote [test cases][test-case-dir], checked coverage with a [requirements traceability matrix][rtm-doc]
+ Executed six cycles of testing, [recorded results][test-results-dir], [summarized testing][test-results-doc] and documented [defects][defect-doc].  
+ ADA compliance testing via WAVE plugin.

Testing included multiple devices (smart phones, desktop web browser), and multiple screen sizes.

---

## Deployment
- Configured continuous integration (TFS) to execute unit testing, see [sample report][Continuous_Integration_Rpt-doc] .  
- *Ongoing - not complete* automating deployment to a development server.  
- Performing configuration management manually, see [change log][change-log-doc]
- Added continuous monitoring to our ‘production’ site, see [sample report][Monitor-Sample-doc]
- *ongoing - not complete* Docker, within the available timeframe, we did not manage to implement Docker.  Instead we utilize AMI images to simplify and reduce deployment risk.  
- Documented how to [deploy][deployment-doc] the prototype.

(939 words)
 
[prototype-url]: <http://prototype.natomadev.com/HHSPrototype/>
[sys-url]:<http://www.youthsolutions.org/>
[url-kanban-dir]:</Doc/Planning/Kanban>
[approach-doc]:</Doc/Planning/Prototype-Approach.pdf>
[conceptual-data-model-doc]:</Doc/Architecture/Conceptual-Data-Model.pdf>
[backlog-dir]:</Doc/Planning/Backlog>
[backlog-doc]:</Doc/Planning/Backlog/Backlog-TFS-Extract-20160608.pdf>
[req-doc]:</Doc/Requirements/Functional-and-Technical-Requirements.pdf
[all-stories-dir]:</Doc/Planning/Stories>
[logo]: </Doc/NT_Logo_rgb_tight_transparent.gif>
[initial-reqs-doc]:</Doc/Planning/Stories/User-Stories-Tasks-Initial.pdf>
[Session1-dir]:</Doc/Human-Centered-Design/Session1/>
[Session2-dir]:</Doc/Human-Centered-Design/Session2/>
[architecture-doc]:</Doc/Architecture/Prototype-Architecture.pdf>  
[prototype-V1-screenshots]:</Doc/Human-Centered-Design/Interviews-Analysis/Iteration-One-Screen-Shots.pdf>
[Yuri-Kimura-IV-doc]:</Doc/Human-Centered-Design/Interviews-Analysis/Yuri_User-Feedback-Session1.txt>
[Playbook-doc]:</Doc/Human-Centered-Design/Playbook/Playbook-CHHS-Prototype.pdf>
[MS-License]:<https://www.visualstudio.com/en-us/dn877550.aspx> 
[use-case-dir]:</Doc/Requirements/Use-Cases/>
[test-case-dir]:</Doc/Test/Test-Planning/>
[test-results-doc]:</Doc/Test/Test-Execution/Test-Results-Summary.pdf>
[test-results-dir]:</Doc/Test/Test-Execution/>
[defect-doc]:</Doc/Test/Defects/Defect-Summary.pdf>
[Continuous_Integration_Rpt-doc]:</Doc/Test/Automated-Unit-Test-Reports/Continuous_Integration_Rpt.pdf>
[change-log-doc]:</Doc/Configuration-Management/Change-Log.pdf>
[Monitor-Sample-doc]:</Doc/Monitoring/Monitoring-Sample.pdf>
[Human-Centered-Design-doc]:</Doc/Human-Centered-Design/Human-centered-design.pdf>
[deployment-doc]:</Doc/Deployment/Deployment-Instructions.pdf>
[google-analytics-doc]:</Doc/Analytics/Analytics-Sample.pdf>
[rtm-doc]:</Doc/Requirements/Simple-RTM.pdf>
