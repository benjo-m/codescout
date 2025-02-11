using api.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace api.Data;

public class SeedData
{
    private ApplicationContext _context;

    public SeedData(ApplicationContext context)
    {
        _context = context;
    }

    public void GenerateData()
    {
        // Check if data is already generated
        if (!_context.Roles.Any())
        {
            var roles = new[]
            {
                new Role { Name = "Recruiter" },
                new Role { Name = "Candidate" }
            };

            var workArrangements = new[]
            {
                new WorkArrangement { Name = "Office" },
                new WorkArrangement { Name = "Remote" },
                new WorkArrangement { Name = "Hybrid" }
            };

            var positions = new[]
            {
                new Position { Title = "Frontend Developer" },
                new Position { Title = "Backend Developer" },
                new Position { Title = "Full Stack Developer" },
                new Position { Title = "Software Engineer" },
                new Position { Title = "Data Scientist" },
                new Position { Title = "Data Analyst" },
                new Position { Title = "DevOps Engineer" },
                new Position { Title = "UI/UX Designer" },
                new Position { Title = "System Administrator" },
                new Position { Title = "Network Engineer" },
                new Position { Title = "Security Analyst" },
                new Position { Title = "Database Administrator" },
                new Position { Title = "Quality Assurance Engineer" },
                new Position { Title = "Machine Learning Engineer" },
                new Position { Title = "Cybersecurity Engineer" },
                new Position { Title = "Cloud Architect" }
            };

            var technologies = new[]
            {
                new Technology { Name = "Angular" },
                new Technology { Name = "Apollo" },
                new Technology { Name = "ASP.NET" },
                new Technology { Name = "AWS Amplify" },
                new Technology { Name = "Assembly" },
                new Technology { Name = "Bootstrap" },
                new Technology { Name = "C#" },
                new Technology { Name = "C++" },
                new Technology { Name = "COBOL" },
                new Technology { Name = "Clojure" },
                new Technology { Name = "CodeIgniter" },
                new Technology { Name = "D3.js" },
                new Technology { Name = "Dart" },
                new Technology { Name = "Django" },
                new Technology { Name = "Django REST framework" },
                new Technology { Name = "Docker" },
                new Technology { Name = "Elixir" },
                new Technology { Name = "Erlang" },
                new Technology { Name = "Express.js" },
                new Technology { Name = "F#" },
                new Technology { Name = "FastAPI" },
                new Technology { Name = "Flask" },
                new Technology { Name = "Flask-RESTful" },
                new Technology { Name = "Fortran" },
                new Technology { Name = "GraphQL" },
                new Technology { Name = "Go" },
                new Technology { Name = "Groovy" },
                new Technology { Name = "Haskell" },
                new Technology { Name = "Hibernate" },
                new Technology { Name = "HTML" },
                new Technology { Name = "Java" },
                new Technology { Name = "JavaScript" },
                new Technology { Name = "Jasmine" },
                new Technology { Name = "Jenkins" },
                new Technology { Name = "Jupyter" },
                new Technology { Name = "JUnit" },
                new Technology { Name = "jQuery" },
                new Technology { Name = "Keras" },
                new Technology { Name = "Kotlin" },
                new Technology { Name = "Kubernetes" },
                new Technology { Name = "Laravel" },
                new Technology { Name = "Less" },
                new Technology { Name = "Lua" },
                new Technology { Name = "Matlab" },
                new Technology { Name = "MobX" },
                new Technology { Name = "Mocha" },
                new Technology { Name = "Node.js" },
                new Technology { Name = "NumPy" },
                new Technology { Name = "Objective-C" },
                new Technology { Name = "OpenShift" },
                new Technology { Name = "Pandas" },
                new Technology { Name = "Perl" },
                new Technology { Name = "PHP" },
                new Technology { Name = "PyTorch" },
                new Technology { Name = "Pytest" },
                new Technology { Name = "Python" },
                new Technology { Name = "React" },
                new Technology { Name = "Redux" },
                new Technology { Name = "Ruby" },
                new Technology { Name = "Ruby on Rails" },
                new Technology { Name = "R" },
                new Technology { Name = "Rust" },
                new Technology { Name = "Sass" },
                new Technology { Name = "Scala" },
                new Technology { Name = "Scikit-Learn" },
                new Technology { Name = "Shell" },
                new Technology { Name = "Socket.io" },
                new Technology { Name = "Spring" },
                new Technology { Name = "Spring Boot" },
                new Technology { Name = "Stripe" },
                new Technology { Name = "Symfony" },
                new Technology { Name = "Swift" },
                new Technology { Name = "Tailwind CSS" },
                new Technology { Name = "TensorFlow" },
                new Technology { Name = "Three.js" },
                new Technology { Name = "TypeScript" },
                new Technology { Name = "Unity" },
                new Technology { Name = "Vue.js" },
                new Technology { Name = "VueX" },
                new Technology { Name = "Visual Basic" },
                new Technology { Name = "Webpack" }
            };

            _context.Roles.AddRange(roles);
            _context.WorkArrangements.AddRange(workArrangements);
            _context.Positions.AddRange(positions);
            _context.Technologies.AddRange(technologies);

            _context.SaveChanges();

            var companies = new[]
            {
                new Company { Name = "TechNova Inc.", Address = new Address { Country = "USA", City = "New York", Street = "Broadway St", PostalCode = "10001" } },
                new Company { Name = "CodeCrafters Ltd.", Address = new Address { Country = "Canada", City = "Toronto", Street = "Main St", PostalCode = "M5V 2L6" } },
                new Company { Name = "CyberTech Innovations", Address = new Address { Country = "UK", City = "London", Street = "Oxford St", PostalCode = "W1D 1AA" } },
                new Company { Name = "ByteForge Solutions", Address = new Address { Country = "France", City = "Paris", Street = "Champs-Élysées", PostalCode = "75008" } },
                new Company { Name = "DataMinds Corporation", Address = new Address { Country = "Germany", City = "Berlin", Street = "Brandenburg Gate", PostalCode = "10117" } },
            };

            _context.Companies.AddRange(companies);
            _context.SaveChanges();

            var recruiters = new[]
            {
                new User { Username = "john_doe", Email = "john.doe@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), Role = _context.Roles.Find(1)! },
                new User { Username = "jane_smith", Email = "jane.smith@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("secret456"), Role = _context.Roles.Find(1)! },
                new User { Username = "mike_jackson", Email = "mike.jackson@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass123word"), Role = _context.Roles.Find(1)! },
                new User { Username = "sara_adams", Email = "sara.adams@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("secure789"), Role = _context.Roles.Find(1)! },
                new User { Username = "david_clark", Email = "david.clark@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("p@ssw0rd"), Role = _context.Roles.Find(1)! },
           };   

            var companyList = _context.Companies.ToList();

            for (int i = 0; i < companies.Length; i++)
            {
                recruiters[i].Company = companyList[i];
            }

            _context.Users.AddRange(recruiters);
            _context.SaveChanges();

            var projects = new List<Project>();
            Random random = new Random();
            var recruiterList = _context.Users.ToList();
            int posCount = _context.Positions.ToList().Count + 1;

            List<string> projectNames = new List<string>
            {
                "Smart Home Automation App",
                "Personal Finance Tracker",
                "Event Management Platform",
                "Fitness Progress Tracker",
                "Social Media Dashboard",
                "Inventory Management System",
                "Job Application Tracker",
                "Real-time Collaboration Tool",
                "E-commerce Analytics App",
                "Habit Formation Tracker",
                "Expense Splitter App",
                "Team Task Manager",
                "Remote Learning Platform",
                "Travel Itinerary Planner",
                "Healthcare Appointment System",
                "Property Listing Portal",
                "Online Survey Builder",
                "Content Scheduling Tool",
                "Virtual Classroom Manager",
                "Restaurant Reservation System",
                "Freelancer Project Bidding Platform",
                "Client Relationship Management (CRM) System",
                "Warehouse Stock Control",
                "Video Content Hosting Platform",
                "Multi-Currency Trading App"
            };

            List<string> projectDescriptions = new List<string>
            {
                "Smart Home Automation App: A mobile app that allows users to control and monitor their home devices, such as lights, thermostats, and security systems. It includes voice command integration and real-time notifications. Users can create automation routines and track energy usage. The app syncs with smart home hubs and supports multiple device types.",
                "Personal Finance Tracker: This app helps users manage their personal finances by tracking income, expenses, and savings goals. It features customizable budgets, transaction categorization, and financial reports. Users can link their bank accounts for real-time updates and receive alerts for upcoming bills. The app also includes goal-setting tools for long-term financial planning.",
                "Event Management Platform: A web-based platform for organizing and managing events, such as conferences, weddings, or corporate gatherings. Users can create event pages, sell tickets, and track registrations. The platform provides tools for sending reminders, managing guest lists, and communicating with attendees. It also offers analytics for event organizers to assess performance.",
                "Fitness Progress Tracker: A fitness app designed to track workouts, nutrition, and physical progress over time. Users can log exercise routines, monitor calorie intake, and set fitness goals. The app includes workout plans, a progress graph, and integration with fitness wearables. It supports sharing achievements with friends or personal trainers.",
                "Social Media Dashboard: A tool for managing and scheduling posts across multiple social media platforms from a single interface. Users can create, edit, and schedule content, view engagement metrics, and track follower growth. The app provides analytics on post performance and allows team collaboration. It supports platforms like Facebook, Instagram, Twitter, and LinkedIn.",
                "Inventory Management System: A business app designed to track and manage inventory levels, orders, and suppliers. It features automated reordering, barcode scanning, and stock level alerts. Users can generate reports on inventory turnover and profitability. The system integrates with accounting tools and provides a user-friendly dashboard for real-time updates.",
                "Job Application Tracker: A personal tool for managing job applications, interviews, and networking contacts. Users can add new job listings, track application status, and store interview notes. The app includes reminders for follow-ups and deadlines. It also offers analytics to help users track their job search progress over time.",
                "Real-time Collaboration Tool: A cloud-based collaboration app that allows teams to work together on documents, spreadsheets, and presentations in real-time. The app supports version control, inline commenting, and role-based permissions. It also integrates with popular productivity suites like Google Workspace and Microsoft Office. Notifications keep team members up-to-date on changes.",
                "E-commerce Analytics App: A tool for online store owners to analyze sales data, track customer behavior, and measure product performance. The app includes customizable reports on revenue, traffic, and conversion rates. It provides insights into customer demographics and purchasing habits. The app integrates with popular e-commerce platforms like Shopify and WooCommerce.",
                "Habit Formation Tracker: A mobile app that helps users build and maintain positive habits by setting daily, weekly, or monthly goals. It provides reminders and visual progress tracking through streaks and achievements. Users can log habits, receive motivational quotes, and set custom rewards for consistency. The app includes data visualization to track long-term progress.",
                "Expense Splitter App: A mobile app designed to help groups split expenses for shared activities such as trips, dining, or rent. Users can create groups, log expenses, and calculate who owes what. The app allows real-time updates, payment tracking, and currency conversions for international expenses. It also integrates with payment services for easy settlements.",
                "Team Task Manager: A task management app designed for teams to track project tasks, assign responsibilities, and set deadlines. It includes features for creating task lists, setting priorities, and providing status updates. Users can collaborate on tasks through comments and attachments. The app offers an overview dashboard and integrates with popular team communication tools.",
                "Remote Learning Platform: A platform for conducting online classes, sharing educational resources, and facilitating student-teacher interaction. It supports video conferencing, assignment submissions, and real-time quizzes. Teachers can track student progress, grade assignments, and provide feedback. The platform integrates with LMS systems and supports various multimedia content types.",
                "Travel Itinerary Planner: A travel app that helps users organize their trips by creating detailed itineraries, booking accommodations, and tracking flight information. Users can add destinations, activities, and restaurants to their plans. The app provides weather forecasts and travel alerts. It also allows sharing itineraries with friends or family.",
                "Healthcare Appointment System: A scheduling platform for healthcare providers and patients to manage appointments, medical records, and billing. Patients can book appointments online, receive reminders, and view their medical history. Healthcare providers can track patient visits, prescribe medications, and manage staff schedules. The system integrates with insurance and billing systems.",
                "Property Listing Portal: A web-based platform for real estate agents and property owners to list, search, and manage property listings. It features advanced search filters, photo galleries, and virtual tours. Users can schedule property visits and communicate with agents directly through the platform. The app supports rental and sale listings.",
                "Online Survey Builder: A customizable tool for creating and distributing online surveys, polls, and quizzes. Users can design surveys with various question types, set logic rules, and distribute them via email or social media. The app includes analytics to track responses and generate reports. It supports exporting data to popular formats like CSV and Excel.",
                "Content Scheduling Tool: A tool designed for content creators to plan, schedule, and publish content across multiple platforms. It includes a visual content calendar, drag-and-drop functionality, and automated posting. The tool provides performance analytics and integrates with blogging platforms, social media, and content management systems.",
                "Virtual Classroom Manager: A platform for teachers and students to conduct virtual classes, share resources, and collaborate on projects. It includes video conferencing, real-time chat, and screen-sharing features. Teachers can assign homework, track attendance, and grade assignments. The platform integrates with educational tools and supports multiple languages.",
                "Restaurant Reservation System: A mobile and web app for booking tables at restaurants, with features for viewing menus, setting preferences, and receiving confirmations. Restaurants can manage table availability, view customer preferences, and handle cancellations. The app also includes a rating system for customer feedback and integrates with POS systems.",
                "Freelancer Project Bidding Platform: A marketplace where freelancers can bid on projects posted by clients. The platform allows clients to post detailed job descriptions, set budgets, and review freelancer profiles. Freelancers can submit proposals, negotiate terms, and track project progress. The platform includes payment gateways and review systems for both parties.",
                "Client Relationship Management (CRM) System: A CRM tool designed to help businesses manage customer interactions, sales pipelines, and marketing efforts. It includes contact management, deal tracking, and lead generation tools. Users can track communications, schedule follow-ups, and generate reports on customer data. The CRM integrates with email platforms and marketing tools.",
                "Warehouse Stock Control: A warehouse management app for tracking stock levels, order fulfillment, and supplier information. It includes barcode scanning, real-time stock updates, and reorder notifications. Users can generate inventory reports, track shipments, and monitor warehouse performance. The app integrates with e-commerce platforms and ERP systems.",
                "Video Content Hosting Platform: A web-based platform for hosting, sharing, and streaming video content. Users can upload videos, organize them into playlists, and track views. The platform includes features for live streaming, video monetization, and viewer engagement. It supports multiple video formats and offers customization options for branding.",
                "Multi-Currency Trading App: A financial trading app that allows users to buy and sell stocks, currencies, and commodities in multiple markets. The app provides real-time market data, trade execution, and portfolio tracking. Users can set custom alerts, view performance reports, and conduct in-depth analysis using charts and graphs. The app supports various international currencies."
            };

            for (int i = 0; i < recruiterList.Count(); i++)
            {
                int last = i * 5 + 5;
                for (int j = i * 5; j < last; j++)
                {
                    Project project = new Project
                    {
                        Name = projectNames[j],
                        Description = projectDescriptions[j],
                        DueDate = new DateOnly(2024, random.Next(1, 13), random.Next(1, 29)),
                        Technologies = new List<Technology>
                        {
                            _context.Technologies.Find(random.Next(1, 10))!,
                            _context.Technologies.Find(random.Next(1, 10))!,
                            _context.Technologies.Find(random.Next(1, 10))!,
                            _context.Technologies.Find(random.Next(1, 10))!
                        },
                        Position = _context.Positions.Find(random.Next(1, posCount))!,
                        WorkArrangement = _context.WorkArrangements.Find(random.Next(1, 4))!,
                        Recruiter = recruiterList[i],
                        Company = recruiterList[i].Company!
                    };

                    projects.Add(project);
                }
            }

            _context.Projects.AddRange(projects);
            _context.SaveChanges();

            var candidates = new[]
            {
                new User { Username = "jtaylor", Email = "jacob.taylor@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), Role = _context.Roles.Find(2)!, Biography = "Growing up in a serene village, my thirst for knowledge was insatiable. Scholarships paved my way through higher education. In my field of choice, I made strides with innovative contributions, earning respect. Known for my empathy, I devote time to volunteering and mentorship, aiming to uplift others. Fueled by ambition and compassion, I march ahead, determined to leave a lasting legacy."},
                new User { Username = "billy_j", Email = "william.johnson@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), Role = _context.Roles.Find(2)!, Biography = "From a tranquil hamlet, I embraced a love for learning from an early age. Scholarships propelled me through higher education. Flourishing in my field, I left an indelible mark with pioneering work. Revered for my empathy, I commit to volunteerism and mentorship, aspiring to inspire. Fueled by ambition and kindness, I forge ahead, intent on shaping a brighter future." },
                new User { Username = "em_brown", Email = "emma.brown@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), Role = _context.Roles.Find(2)!, Biography = "Raised in a serene countryside, I fostered an unyielding thirst for knowledge from a young age. Scholarships paved my path to higher education. Flourishing in my chosen field, I made significant strides and earned acclaim. Revered for my compassion, I dedicate myself to philanthropy and mentorship, aiming to empower others. Fueled by ambition and empathy, I press onward, determined to make a meaningful impact." },
                new User { Username = "mike_j", Email = "mike.jackson@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), Role = _context.Roles.Find(2)!, Biography = "Born in a tranquil village, my curiosity knew no bounds. Scholarships propelled me into higher education. In my field, I excelled, making significant strides and earning recognition. Known for my empathy, I devote myself to volunteer work and mentorship, striving to uplift those around me. Fueled by ambition and compassion, I march forward, driven to create positive change."},
                new User { Username = "dave_c", Email = "david.clark@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), Role = _context.Roles.Find(2)!, Biography = "Growing up in a serene countryside, I cultivated a deep love for learning. Scholarships became my ticket to higher education. In my chosen field, I thrived, making groundbreaking contributions and earning respect. Revered for my empathy, I dedicate myself to volunteering and mentorship, hoping to make a difference in the lives of others. Fueled by ambition and compassion, I push forward, determined to leave my mark on the world." }
            };

            _context.Users.AddRange(candidates);
            _context.SaveChanges();

            var awards = new[]
            {
                new Award { Name = "First Submission", Description = "Submit your first solution" },
                new Award { Name = "Domain Expert", Description = "Use one technology for 10 different projects" },
                new Award { Name = "Solution Specialist", Description = "10 projects completed" },
                new Award { Name = "Polyglot Developer", Description = "Work with 20 different technologies" },
                //new Award { Name = "Social Butterfy", Description = "Add 5 friends" },
            };

            _context.Awards.AddRange(awards);
            _context.SaveChanges();
        }
    }


    public void GenerateMessages()
    {
        if (!_context.Messages.Any())
        {
            Random random = new Random();

            for (global::System.Int32 i = 1; i < 6; i++)
            {
                for (global::System.Int32 j = 1; j < 6; j++)
                {
                    if (j == i) continue;

                    for (global::System.Int32 k = 1; k < 31; k++)
                    {
                        DateTime dt = new DateTime(random.Next(2010, 2023), random.Next(1, 13), random.Next(1, 8), random.Next(1, 24), random.Next(1,60), random.Next(1, 60));

                        _context.Messages.Add(new Message { Text = $"Message{k}", DateSent = dt, Received = false, Deleted = false, SenderId = j, ReceiverId = i });
                    }
                }
            }

            _context.SaveChanges();
        }
    }
}