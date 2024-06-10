using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding 10 genres
            var genres = new List<Genre>()
            {
                new Genre ()
                {
                    Id = Guid.Parse("5568b5ce-2e7f-4bc4-8af0-a18c976c9531"),
                    Name = "Business"
                },
                new Genre ()
                {
                    Id = Guid.Parse("b63e0779-7a0e-4b76-9469-8e27beebf898"),
                    Name = "Romance"
                },
                new Genre ()
                {
                    Id = Guid.Parse("2cd9ea1b-1b93-4977-8587-f9329f0e2078"),
                    Name = "Action & Adventure"
                },
                new Genre ()
                {
                    Id = Guid.Parse("3c232235-c2d6-42ed-b6ac-569e7f75a192"),
                    Name = "Mystery"
                },
                new Genre ()
                {
                    Id = Guid.Parse("86d4e2c3-10ad-4a90-be21-9ec3acec8fc9"),
                    Name = "Thriller"
                },
                new Genre ()
                {
                    Id = Guid.Parse("f45196b2-6137-4bd3-94b1-21175ece64c4"),
                    Name = "Computers & Technology"
                },
                new Genre ()
                {
                    Id = Guid.Parse("7621b5bf-97f8-4ff2-884b-9caccdcc8513"),
                    Name = "Humor & Entertainment"
                },
                new Genre ()
                {
                    Id = Guid.Parse("3c99605d-496b-4b96-a04a-094477ef2c4d"),
                    Name = "Art, Music & Photography"
                },
                new Genre ()
                {
                    Id = Guid.Parse("510bdcf5-f312-47e2-addf-9778c9d110d1"),
                    Name = "Medicine"
                },
                new Genre ()
                {
                    Id = Guid.Parse("e8350553-ebd1-4830-b0ee-81f85e1b22cc"),
                    Name = "Sports & Outdoors"
                }
            }; 
            modelBuilder.Entity<Genre>().HasData(genres);

            // Seeding 50 products
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.Parse("da1346d3-02ee-43e2-b2ad-3947207666e8"),
                    Author = "Sima Dimitrijev, Maryann Karinch",
                    Publisher = "Armin Lear Press (April 8, 2021)",
                    Title = "Trial, Error, and Success",
                    GenreId = Guid.Parse("5568b5ce-2e7f-4bc4-8af0-a18c976c9531"),
                    Description = """
                        Everything in nature evolves by trial, error, and success. At the fundamental level, this means that the rigid laws of physics don't rule nature. At the level of your thinking, this means that no established one-size-fits-all science should inhibit your free-will decisions to try, fail, and succeed. As a guide to success by strategic decision making, this book will support your skeptical thinking and propensity for prudent use of expert advice.

                        Presenting real-life examples, the thinking in the book combines sharp analyses with broad analogies to show:
 
                        How to identify realistic knowledge and avoid harm due to overgeneralized concepts.
                        How to solve problems and create new knowledge by trial-and - error thinking.
                        How to reduce personal risk and maximize benefits by collective application of the trial-and - error process.
                        Read this book to learn things that our formal education shuns.That way, you'll be better prepared for the unexpected, less likely to conform when you shouldn't, more creative, and more likely to learn from both failures and successes of others.You'll see why machine learning and automatic actions cannot replace human intelligence and free - will decisions.
                        """,
                    ISBN = "978-1735617480",
                    Price = 18.60,
                    Stock = 32,
                    FrontCover = "images/products/front-cover/da1346d3-02ee-43e2-b2ad-3947207666e8.jpg",
                    BackCover = "images/products/back-cover/da1346d3-02ee-43e2-b2ad-3947207666e8.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("f9b07092-4ad7-4383-a9c0-da99b79a55a0"),
                    Author = "Danial Jiwani",
                    Publisher = "Independently published (August 21, 2020)",
                    Title = "Buffett's 2-Step Stock Market Strategy",
                    GenreId = Guid.Parse("5568b5ce-2e7f-4bc4-8af0-a18c976c9531"),
                    Description = """
                        Warren Buffett is one of the best investors of all time. But what is his strategy? Buffett's 2-Step Stock Market Strategy breaks down Buffett's 2-step strategy and compiles his best investing principles so that you can replicate his strategy when you invest in stocks.
                        Buffett's 2-Step Stock Market Strategy will teach you when Buffett buys and sells, what he looks for when researching a stock, and the biggest mistakes that beginners make when trying to replicate his strategy.
                        Danial Jiwani (the author) has seen some people lose over $100,000 in a stock because they did not properly understand Warren Buffett's strategy. This is the last thing Danial Jiwani wants to see happen to you. So, Buffett's 2-Step Stock Market Strategy will explain a proven investing strategy so that you know how to make money in stocks.
                        Danial Jiwani has taught over 2,000 readers how to make money in the stock market. They have found his advice helpful and insightful because they learned when to exactly buy a stock. Now, Danial Jiwani wants to share everything that he knows about stock market investing with his ultimate beginner's guide to the stock market.
                        """,
                    ISBN = "979-8677554032",
                    Price = 17.09,
                    Stock = 65,
                    FrontCover = "images/products/front-cover/f9b07092-4ad7-4383-a9c0-da99b79a55a0.jpg",
                    BackCover = "images/products/back-cover/f9b07092-4ad7-4383-a9c0-da99b79a55a0.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("ca3337d1-57b9-4d7e-a74c-0303fbc812af"),
                    Author = "Taylor Jenkins Reld",
                    Publisher = "Atria Books; Reprint edition (May 29, 2018)",
                    Title = "The Seven Husbands of Evelyn Hugo",
                    GenreId = Guid.Parse("b63e0779-7a0e-4b76-9469-8e27beebf898"),
                    Description = """
                        "If you're looking for a book to take on holiday this summer, The Seven Husbands of Evelyn Hugo has got all the glitz and glamour to make it a perfect beach read." -Bustle
                        From the New York Times bestselling author of Daisy Jones & the Six-an entrancing and "wildly addictive journey of a reclusive Hollywood starlet" (PopSugar) as she reflects on her relentless rise to the top and the risks she took, the loves she lost, and the long-held secrets the public could never imagine.
                        Aging and reclusive Hollywood movie icon Evelyn Hugo is finally ready to tell the truth about her glamorous and scandalous life. But when she chooses unknown magazine reporter Monique Grant for the job, no one is more astounded than Monique herself. Why her? Why now?
                        Monique is not exactly on top of the world. Her husband has left her, and her professional life is going nowhere. Regardless of why Evelyn has selected her to write her biography, Monique is determined to use this opportunity to jumpstart her career.
                        Summoned to Evelyn's luxurious apartment, Monique listens in fascination as the actress tells her story. From making her way to Los Angeles in the 1950s to her decision to leave show business in the '80s, and, of course, the seven husbands along the way, Evelyn unspools a tale of ruthless ambition, unexpected friendship, and a great forbidden love. Monique begins to feel a very real connection to the legendary star, but as Evelyn's story near its conclusion, it becomes clear that her life intersects with Monique's own in tragic and irreversible ways.
                        "Heartbreaking, yet beautiful" (Jamie Blynn, Us Weekly), The Seven Husbands of Evelyn Hugo is "Tinseltown drama at its finest" (Redbook): a mesmerizing journey through the splendor of old Hollywood into the harsh realities of the present day as two women struggle with what it means-and what it costs-to face the truth.
                        """,
                    ISBN = "978-1501161933",
                    Price = 9.99,
                    Stock = 25,
                    FrontCover = "images/products/front-cover/ca3337d1-57b9-4d7e-a74c-0303fbc812af.jpg",
                    BackCover = "images/products/back-cover/ca3337d1-57b9-4d7e-a74c-0303fbc812af.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("c41d3ab1-c672-428d-be5c-db7e3fa6c2b3"),
                    Author = "Kristin Lee",
                    Publisher = "Independently published (August 1, 2021)",
                    Title = "Always With Me: The Racing Hearts Series",
                    GenreId = Guid.Parse("b63e0779-7a0e-4b76-9469-8e27beebf898"),
                    Description = """
                        She's stubborn. He's experienced. Will their love of horse racing drag them across the finish line?
                        Brandon was my first love. My only love. Yeah, I was a hopeless romantic (and an idiot).
                        Thank God, the cocky, college guy didn't get my V-card.
                        Just my heart. Priorities, right?
                        But years later, I still won't let him in. Not after he wrecked me that night and shattered my romantic heart into pieces. Seeing his betrayal was one thing but feeling it was another.
                        But Brandon offers me the one thing he knows I can't refuse.horses to train. I will be strong. I won't let him mend the broken trust-the broken love.
                        I'm older. Wiser. And so is he. Sometimes sprinting towards the finish line isn't the right course of action. Maybe we need to take the long route. Will giving us a second chance at love and intimacy land us in the winner's circle?
                        If you like new adult contemporary romances, you will love the steamy, slow burn of Always With Me.
                        """,
                    ISBN = "979-8547989483",
                    Price = 12.99,
                    Stock = 42,
                    FrontCover = "images/products/front-cover/c41d3ab1-c672-428d-be5c-db7e3fa6c2b3.jpg",
                    BackCover = "images/products/back-cover/c41d3ab1-c672-428d-be5c-db7e3fa6c2b3.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("a4f8cd75-cae1-495d-bbd4-e5d50a344184"),
                    Author = "Mike Curtis",
                    Publisher = "Powerline Productions (August 7, 2022)",
                    Title = "The Key House",
                    GenreId = Guid.Parse("2cd9ea1b-1b93-4977-8587-f9329f0e2078"),
                    Description = """
                        A newly inherited house. A 150-year-old secret. And some mysterious, hidden clues.
                        Join Caleb and David in this fast-paced Hardy Boys meets The Goonies, edge-of-your-seat mystery/adventure, as they seek a rumored treasure and clear their great-great-grandfather's good name.
                        After moving into a newly inherited house, the Noland kids discover clues to a supposed treasure hidden by their great-great-grandfather. But when town rumors and some spiteful neighbors suggest foul play was involved, Caleb and David set out on a quest to prove otherwise.
                        With unexpected dangers looming large and threatening their mission, can the Noland kids discover the mysterious treasure, or whatever lies at the end of their treacherous adventure, before time runs out and all is lost?
                        """,
                    ISBN = "979-8986466408",
                    Price = 11.77,
                    Stock = 16,
                    FrontCover = "images/products/front-cover/a4f8cd75-cae1-495d-bbd4-e5d50a344184.jpg",
                    BackCover = "images/products/back-cover/a4f8cd75-cae1-495d-bbd4-e5d50a344184.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("14cbde27-b823-43eb-8d24-867d653abf34"),
                    Author = "Kristin F. Johnson",
                    Publisher = "KFJ Books (May 8, 2022)",
                    Title = "Fearless",
                    GenreId = Guid.Parse("2cd9ea1b-1b93-4977-8587-f9329f0e2078"),
                    Description = """
                        Jessie wants to be fearless like her deployed Army mom, so when she and her friends stumble onto a barn with terrified dogs inside, she steals one. But how will she help a scared dog when Jessie herself feels anything but fearless.
                        This timeless friendship story was a finalist for the 2023 MiPA Book Award.
                        Kristin F. Johnson is a former elementary media specialist. She has won multiple awards for her writing as well as two Minnesota State Arts Board grants. Fearless is her tenth book for kids.
                        """,
                    ISBN = "979-8986226514",
                    Price = 12.08,
                    Stock = 25,
                    FrontCover = "images/products/front-cover/14cbde27-b823-43eb-8d24-867d653abf34.jpg",
                    BackCover = "images/products/back-cover/14cbde27-b823-43eb-8d24-867d653abf34.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("134dad67-9ce9-4d31-9156-5e7676a9e529"),
                    Author = "Charles William Ayer",
                    Publisher = "Independently published (August 15, 2020)",
                    Title = "A Deadly Light",
                    GenreId = Guid.Parse("3c232235-c2d6-42ed-b6ac-569e7f75a192"),
                    Description = """
                        In New York City, Detective Sergeant Walter Hudson struggles to solve two seemingly unrelated murders while crime soars in the strike-riddled city under the incompetent leadership of Mayor Peter Nutting.Meanwhile, in Washington, D.C., President Thomas Bennett feels power, power that he does not want to relinquish, slip away as time runs out on his failed administration.Hudson's efforts to solve the murders soon find him inexorably lured into the treacherous worlds of political intrigue , unchecked power, and limitless wealth, worlds that he is ill-equipped to understand, as President Bennett and Mayor Nutting have to decide just how far they are willing to go to retain the power they so desperately crave.They didn't teach Detective Hudson how to handle any of this in the Police Academy, but he's going to have to learn fast, as he begins to peel back the layers of a monstrous plot that threatens not only the city he loves, but his country as well. It's a race against time, and Detective Hudson has a lot less time than he thinks.
                        """,
                    ISBN = "979-8675590919",
                    Price = 9.99,
                    Stock = 62,
                    FrontCover = "images/products/front-cover/134dad67-9ce9-4d31-9156-5e7676a9e529.jpg",
                    BackCover = "images/products/back-cover/134dad67-9ce9-4d31-9156-5e7676a9e529.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("d12eea59-dba7-42ce-a636-26763ae7c979"),
                    Author = "Neal Bailey",
                    Publisher = "Independently published (August 12, 2022)",
                    Title = "Muscle and Blood",
                    GenreId = Guid.Parse("3c232235-c2d6-42ed-b6ac-569e7f75a192"),
                    Description = """
                        A TRAILER PARK HERO. A TWO-FISTED MURDER MYSTERY. A HELL OF A RIDE.
                        Hal Taylor just wants a stable life. A good life. The life he feels was taken from him unfairly. Instead, he's a hired goon for the local crime boss, and his hopes have been tucked away in a dresser drawer. A cakewalk job to secure a wealthy young woman for her father seems like a chance to make some cash...until Hal's luck runs true to form.
                        Waking up on the floor of a hotel bathroom to the sounds of blaring sirens, he finds a body he doesn't recognize cooling in the tub, and from then on, he's on the run. Now Hal has to find out who killed the stranger, rescue the missing girl, and maybe--just maybe--get that community college application turned in.
                        WHEN YOU HAVE NOTHING, YOU HAVE NOTHING TO LOSE. JUST MUSCLE AND BLOOD.
                        """,
                    ISBN = "979-8474054728",
                    Price = 12.99,
                    Stock = 25,
                    FrontCover = "images/products/front-cover/d12eea59-dba7-42ce-a636-26763ae7c979.jpg",
                    BackCover = "images/products/back-cover/d12eea59-dba7-42ce-a636-26763ae7c979.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("9799baae-06cd-4741-a5f4-e19801a364c4"),
                    Author = "Nicola Sanders",
                    Publisher = "Independently published (February 6, 2023)",
                    Title = "Don't Let Her Stay",
                    GenreId = Guid.Parse("86d4e2c3-10ad-4a90-be21-9ec3acec8fc9"),
                    Description = """
                        It seemed only five minutes ago that Richard went to the station to pick up Chloe, and now they were here. I was so excited. Finally, I would meet my stepdaughter for the first time, and Chloe would meet her baby sister Evie.
                        Richard was over the moon that his daughter wanted to come back into his life. 'You'll love her,' he'd said, beaming. 'She's very sweet. She just had a wobble about me marrying again, but that's over now.'
                        Well, she was going to find out she had nothing to worry about. I'd been fantasising non-stop about the two of us becoming close. I had visions of us baking together while Richard was at work, chatting about her boyfriends, her studies, what kind of job she'd like to do. I would be someone she could rely on, someone she could talk to when she would have spoken to her mum if she'd still been alive.
                        Anyway, let's just say things haven't quite worked out that way.
                        Whenever we're alone, Chloe makes it clear that she hates me. But in front of her father, she's a perfect little angel. Richard says I'm not giving her a chance, but he doesn't see what I see. I don't trust Chloe, and I certainly won't leave her alone with Evie.
                        Because I know something isn't right with Chloe, and I will do everything possible to protect my family.
                        Before it's too late.
                        Fans of Freida McFadden, Daniel Hurst and Claire McGowan will be hooked by this totally addictive psychological thriller with a jaw-dropping twist!
                        """,
                    ISBN = "979-8374567144",
                    Price = 11.13,
                    Stock = 52,
                    FrontCover = "images/products/front-cover/9799baae-06cd-4741-a5f4-e19801a364c4.jpg",
                    BackCover = "images/products/back-cover/9799baae-06cd-4741-a5f4-e19801a364c4.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("6c79d2c5-c3a5-492b-b2a4-49a22cc35c53"),
                    Author = "Freida McFadden",
                    Publisher = "Grand Central Publishing (August 23, 2022)",
                    Title = "The Housemaid",
                    GenreId = Guid.Parse("86d4e2c3-10ad-4a90-be21-9ec3acec8fc9"),
                    Description = """
                        "Welcome to the family," Nina Winchester says as I shake her elegant, manicured hand. I smile politely, gazing around the marble hallway. Working here is my last chance to start fresh. I can pretend to be whoever I like. But I'll soon learn that the Winchesters' secrets are far more dangerous than my own.
                        Every day I clean the Winchesters' beautiful house top to bottom. I collect their daughter from school. And I cook a delicious meal for the whole family before heading up to eat alone in my tiny room on the top floor.
                        I try to ignore how Nina makes a mess just to watch me clean it up. How she tells strange lies about her own daughter. And how her husband Andrew seems more broken every day. But as I look into Andrew's handsome brown eyes, so full of pain, it's hard not to imagine what it would be like to live Nina's life. The walk-in closet, the fancy car, the perfect husband.
                        I only try on one of Nina's pristine white dresses once. Just to see what it's like. But she soon finds out. and by the time I realize my attic bedroom door only locks from the outside, it's far too late.
                        But I reassure myself: the Winchesters don't know who I really am.
                        They don't know what I'm capable of.
                        """,
                    ISBN = "978-1538742570",
                    Price = 8.11,
                    Stock = 25,
                    FrontCover = "images/products/front-cover/6c79d2c5-c3a5-492b-b2a4-49a22cc35c53.jpg",
                    BackCover = "images/products/back-cover/6c79d2c5-c3a5-492b-b2a4-49a22cc35c53.jpg"
                },

                new Product()
                {
                    Id = Guid.Parse("d708ddaf-afb5-47f2-bbfa-bc5c25cbe1b7"),
                    Author = "Neil Dagger",
                    Publisher = "Independently published (January 19, 2023)",
                    Title = "The ChatGPT Millionaire",
                    GenreId = Guid.Parse("f45196b2-6137-4bd3-94b1-21175ece64c4"),
                    Description = """
                        This is the simplest guide on how to make money quickly and easily with ChatGPT (Updated for GPT-4)
                        In this step-by-step guide I will share the secrets of how to:
                        ✔ Create passive income sources that keep on giving - in minutes.
                        ✔ Impress your customers by getting their projects finished extremely fast with high quality (with zero effort!)
                        ✔ Research, create and promote engaging content effortlessly.
                        ✔ Get time back to focus on what really matters.
                        We currently live in a world where businesses are paying hundreds of dollars to people for writing engaging articles and blogs, and thousands of dollars per month for social media marketing and SEO. Now using ChatGPT, anyone including you can do this really well - even if you have no experience in this! Most businesses are not aware of or are not using this right now - which is where you can come in and undercut existing providers while doing almost zero work - and I will show you how step-by-step - with instructions you can copy and paste. This market may become saturated a year from now - but this is the right time to start!
                        GET The ChatGPT Millionaire: Making money online has never been this EASY
                        """,
                    ISBN = "979-8374102581",
                    Price = 12.99,
                    Stock = 62,
                    FrontCover = "images/products/front-cover/d708ddaf-afb5-47f2-bbfa-bc5c25cbe1b7.jpg",
                    BackCover = "images/products/back-cover/d708ddaf-afb5-47f2-bbfa-bc5c25cbe1b7.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("31181ed5-9efa-4f72-b9e2-40120b5d03e6"),
                    Author = "Walter Isaacson",
                    Publisher = "Simon & Schuster (September 12, 2023)",
                    Title = "Elon Musk",
                    GenreId = Guid.Parse("f45196b2-6137-4bd3-94b1-21175ece64c4"),
                    Description = """
                        From the author of Steve Jobs and other bestselling biographies, this is the astonishingly intimate story of the most fascinating and controversial innovator of our era-a rule-breaking visionary who helped to lead the world into the era of electric vehicles, private space exploration, and artificial intelligence. Oh, and took over Twitter.
                        When Elon Musk was a kid in South Africa, he was regularly beaten by bullies. One day a group pushed him down some concrete steps and kicked him until his face was a swollen ball of flesh. He was in the hospital for a week. But the physical scars were minor compared to the emotional ones inflicted by his father, an engineer, rogue, and charismatic fantasist.
                        His father's impact on his psyche would linger. He developed into a tough yet vulnerable man-child, prone to abrupt Jekyll-and-Hyde mood swings, with an exceedingly high tolerance for risk, a craving for drama, an epic sense of mission, and a maniacal intensity that was callous and at times destructive.
                        At the beginning of 2022-after a year marked by SpaceX launching thirty-one rockets into orbit, Tesla selling a million cars, and him becoming the richest man on earth-Musk spoke ruefully about his compulsion to stir up dramas. "I need to shift my mindset away from being in crisis mode, which it has been for about fourteen years now, or arguably most of my life," he said.
                        It was a wistful comment, not a New Year's resolution. Even as he said it, he was secretly buying up shares of Twitter, the world's ultimate playground. Over the years, whenever he was in a dark place, his mind went back to being bullied on the playground. Now he had the chance to own the playground.
                        For two years, Isaacson shadowed Musk, attended his meetings, walked his factories with him, and spent hours interviewing him, his family, friends, coworkers, and adversaries. The result is the revealing inside story, filled with amazing tales of triumphs and turmoil, that addresses the question: are the demons that drive Musk also what it takes to drive innovation and progress?
                        """,
                    ISBN = "978-1982181284",
                    Price = 17.50,
                    Stock = 72,
                    FrontCover = "images/products/front-cover/31181ed5-9efa-4f72-b9e2-40120b5d03e6.jpg",
                    BackCover = "images/products/back-cover/31181ed5-9efa-4f72-b9e2-40120b5d03e6.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("11b6e1f3-9c0a-482e-9e12-9230faaafdd0"),
                    Author = "M Prefontaine",
                    Publisher = "CreateSpace Independent Publishing Platform (May 17, 2017)",
                    Title = "Difficult Riddles For Smart Kids",
                    GenreId = Guid.Parse("7621b5bf-97f8-4ff2-884b-9caccdcc8513"),
                    Description = """
                        Brain Teasers for Kids - Riddles for the Whole Family
                        "The mind once stretched by a new idea, never returns to its original dimensions." Ralph Waldo Emerson
                        This kids book is a collection of 300 brain teasing riddles and puzzles. Their purpose is to encourage children to think and stretch their minds. They are designed to test logic, lateral thinking as well as memory and to engage the brain in seeing patterns and connections between different things and circumstances.
                        They are laid out in three chapters which get more difficult as you go through the book, in the author's opinion at least. The answers are at the back of the book if all else fails.
                        These are more difficult riddles for kids and are designed to be attempted by children from 10 years onwards, as well as participation from the rest of the family.
                        It is a perfect activity book for kids who like problem solving. These activities can be shared with the whole family.
                        This book is one of a series of puzzle books for kids. The aim of all of them is to stretch children's brains through kids riddles and puzzles. They are kids books designed to challenge children to think laterally and more creatively.
                        """,
                    ISBN = "978-1546595908",
                    Price = 7.99,
                    Stock = 63,
                    FrontCover = "images/products/front-cover/11b6e1f3-9c0a-482e-9e12-9230faaafdd0.jpg",
                    BackCover = "images/products/back-cover/11b6e1f3-9c0a-482e-9e12-9230faaafdd0.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("aa78d80e-d5e9-4691-97de-a35f201826ed"),
                    Author = "Dan Gilden",
                    Publisher = "Independently published (November 21, 2020)",
                    Title = "Dad Jokes",
                    GenreId = Guid.Parse("7621b5bf-97f8-4ff2-884b-9caccdcc8513"),
                    Description = """
                        THE BEST STOCKING STUFFER IDEA FOR DADS, HUSBANDS, FRIENDS, AND COWORKERS!
                        "I don't tell dad jokes often, but when I do... ...he laughs!"
                        Splendid collection of 200 hilarious jokes, groan-worthy one-liners, and puns for every occasion, including well-known classics, hidden gems, and all-new material.
                        Great Father's Day gift and Christmas stocking stuffer for men.
                        Funny and unexpected gift for friends and office folks.
                        Make it personal! Let them know and sign the cover!
                        Don't be embarrassed about your Dad Jokes. Own them!
                        """,
                    ISBN = "979-8568514404",
                    Price = 7.95,
                    Stock = 92,
                    FrontCover = "images/products/front-cover/aa78d80e-d5e9-4691-97de-a35f201826ed.jpg",
                    BackCover = "images/products/back-cover/aa78d80e-d5e9-4691-97de-a35f201826ed.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("4c0fd36c-1978-4f5c-ad7f-504c37215736"),
                    Author = "Leia Bloom",
                    Publisher = "Let's Draw Press (November 3, 2023)",
                    Title = "How to Draw Anything in 3D",
                    GenreId = Guid.Parse("3c99605d-496b-4b96-a04a-094477ef2c4d"),
                    Description = """
                        Dive into a beautiful world of fantastical 3D drawings and watch your imagination come to life!
                        Do you want to design amazing 3D environments, capture your imagination on paper, and create vivid characters who seem to leap from the page? Are you intrigued by the magic of 3D drawing, but you don't know where to begin? Or do you need a simple guide with a collection of fun projects to try? Then keep reading!
                        Packed with tons of beginner-friendly drawing guides for the aspiring and experienced artist, this practical 3D drawing book reveals the secret to designing tons of amazing 3D creations, ranging from flowers and animals to buildings, portraits, cityscapes, mythical creatures and more.
                        Covering essential pencil drawing techniques for depth and realism, along with how you can master authentic light, shadow, and all the intricacies of illumination, this book seeks to give you the confidence and skills you need to awaken your inner artist, learn valuable new techniques, and feel the joys of creativity.

                        Book details:

                        A Practical Introduction To Essential 3D Drawing Techniques
                        Step-By-Step Instructions That Guide Beginner and Intermediate Artists Through Each Section
                        Expert Tips, Tricks, and Strategies For Mastering Complex Lighting and Shadows
                        Straightforward Guidance To Help You Construct Scenes and Objects From Simple Shapes
                        And Tons of Exciting Projects To Try, Including Plants, Animals, People, Cityscapes, Mythical Creatures, and Much More!
                        So if you want to break out your favorite pencils, enter the world of illusion, and dive into the fascinating realm of 3D art, How to Draw Anything in 3D is a valuable companion guide that will open the door to a whole new world of creativity.

                        Are you ready to explore the world of 3D art? Then scroll up and grab your copy today!
                        """,
                    ISBN = "978-1959885146",
                    Price = 14.68,
                    Stock = 52,
                    FrontCover = "images/products/front-cover/4c0fd36c-1978-4f5c-ad7f-504c37215736.jpg",
                    BackCover = "images/products/back-cover/4c0fd36c-1978-4f5c-ad7f-504c37215736.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("cff6c52a-6c09-43d5-a1c2-f6088af939d6"),
                    Author = "Paul Levin, Paul Terence",
                    Publisher = "Independently published (May 9, 2022)",
                    Title = "Easy Guitar Lessons for Kids + Video",
                    GenreId = Guid.Parse("3c99605d-496b-4b96-a04a-094477ef2c4d"),
                    Description = """
                        The first book is the most important at the beginning of your child's guitar learning.
                        The first book should arouse interest in learning guitar and be accessible for step-by-step easy and fun learning. It is very important to keep inspiration and joy in the early stages. Give your child the opportunity to enter the fascinating and wonderful world of music!
                        First guitar book for kids and teens of all ages;
                        Step by Step: Accessible education from scratch to any child;
                        Learn to Play Famous Guitar Songs for Children;
                        How to Read Music & Guitar Chords, Proper fit and more;
                        Secrets of the correct setting of the left and right hands;
                        Simple exercises to follow and practice;
                        Convenient large USA Letter print size;
                        The most popular and most interesting songs for children and teenagers;
                        Video accompaniment to all lessons by direct link inside the book;
                        2-in-1 Book: Guitar lessons and video + 30 Songs.
                        """,
                    ISBN = "979-8820795114",
                    Price = 19.17,
                    Stock = 24,
                    FrontCover = "images/products/front-cover/cff6c52a-6c09-43d5-a1c2-f6088af939d6.jpg",
                    BackCover = "images/products/back-cover/cff6c52a-6c09-43d5-a1c2-f6088af939d6.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("01cd923c-98c3-4e4c-9752-fffe1c7d3571"),
                    Author = "Ken McCarthy",
                    Publisher = "Independently published (December 6, 2023)",
                    Title = "What the Nurses Saw",
                    GenreId = Guid.Parse("510bdcf5-f312-47e2-addf-9778c9d110d1"),
                    Description = """
                        No human activity can ever be free from error, but to be clear, this book is not about the kind of error all human beings are prone to.
                        As you will learn from the eye-witness accounts and technical information presented in this book, calling the failed COVID protocols "errors" is not accurate.
                        What The Nurses Saw - It was Murder.
                        These protocols were explicitly ordered by those who took dictatorial control of the medical system early in the Panic (spring of 2020). Further, when they were shown to be demonstrably failing and harming many thousands of people, experienced healthcare professionals who raised informed concerns were silenced through demotion, firing, and organized campaigns of harassment promoted by the news media and enabled by companies like Google, Facebook, Twitter, and TikTok, in some cases in collaboration with the White House and the Department of Justice's FBI.
                        If this sounds very bad, it's because it is.
                        What the Nurses Saw is documentation of what happens in the real world when bureaucrats, in this case bureaucrats in Washington DC, take literal dictatorial control over the practice of medicine.
                        On a pure dollar and cents level, one of every five dollars spent in the U.S. is spent on the products of the medical services industry, as is one of every three tax dollars. The U.S., more than any country in the world, and by a large measure, has been colonized by this industry. As part of this process, the industry and its operatives have corrupted and perverted science, academia, and the news media. Now it's hard at work to weaken and degrade the last pillar that keeps the system even remotely functioning - the integrity of the nursing profession.
                        If we fail to support our good nurses, help them hold the line, and start aggressively turning things around, there is no practical limit to how far this totalitarian medical dictatorship which we in fact live under will go in its future abuse and exploitation of human beings.
                        """,
                    ISBN = "979-8870191508",
                    Price = 13.85,
                    Stock = 63,
                    FrontCover = "images/products/front-cover/01cd923c-98c3-4e4c-9752-fffe1c7d3571.jpg",
                    BackCover = "images/products/back-cover/01cd923c-98c3-4e4c-9752-fffe1c7d3571.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("df304aec-cf71-476d-b4fe-67899257e759"),
                    Author = "Daniel Z. Lieberman, Michael E. Long",
                    Publisher = "BenBella Books; First Edition (August 14, 2018)",
                    Title = "The Molecule of More",
                    GenreId = Guid.Parse("510bdcf5-f312-47e2-addf-9778c9d110d1"),
                    Description = """
                        Why are we obsessed with the things we want and bored when we get them?
                        Why is addiction "perfectly logical" to an addict?
                        Why does love change so quickly from passion to indifference?
                        Why are some people diehard liberals and others hardcore conservatives?
                        Why are we always hopeful for solutions even in the darkest times--and so good at figuring them out?
                        The answer is found in a single chemical in your brain: dopamine. Dopamine ensured the survival of early man. Thousands of years later, it is the source of our most basic behaviors and cultural ideas--and progress itself.
                        Dopamine is the chemical of desire that always asks for more--more stuff, more stimulation, and more surprises. In pursuit of these things, it is undeterred by emotion, fear, or morality. Dopamine is the source of our every urge, that little bit of biology that makes an ambitious business professional sacrifice everything in pursuit of success, or that drives a satisfied spouse to risk it all for the thrill of someone new. Simply put, it is why we seek and succeed; it is why we discover and prosper. Yet, at the same time, it's why we gamble and squander.
                        From dopamine's point of view, it's not the having that matters. It's getting something--anything--that's new. From this understanding--the difference between possessing something versus anticipating it--we can understand in a revolutionary new way why we behave as  we do in love, business, addiction, politics, religion - and we can even predict those behaviors in ourselves and others.
                        In The Molecule of More: How a Single Chemical in Your Brain Drives Love, Sex, and Creativity--and will Determine the Fate of the Human Race, George Washington University professor and psychiatrist Daniel Z. Lieberman, MD, and Georgetown University lecturer Michael E. Long present a potentially life-changing proposal: Much of human life has an unconsidered component that explains an array of behaviors previously thought to be unrelated, including why winners cheat, why geniuses often suffer with mental illness, why nearly all diets fail, and why the brains of liberals and conservatives really are different.
                        """,
                    ISBN = "978-1946885111",
                    Price = 16.26,
                    Stock = 52,
                    FrontCover = "images/products/front-cover/df304aec-cf71-476d-b4fe-67899257e759.jpg",
                    BackCover = "images/products/back-cover/df304aec-cf71-476d-b4fe-67899257e759.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("88e3175e-eb08-4fe2-b74e-37edb098b7d9"),
                    Author = "Lora S. Irish",
                    Publisher = "Fox Chapel Publishing; First Edition (June 1, 2015)",
                    Title = "Great Book of Carving Patterns",
                    GenreId = Guid.Parse("e8350553-ebd1-4830-b0ee-81f85e1b22cc"),
                    Description = """
                        If you've been wondering where to find inspiration for your next carving project, look no further. This treasure trove of classic designs from internationally recognized artist Lora S. Irish is for you. Inside you'll find 200 original patterns that make wonderful subjects for both relief and power carving.
                        This comprehensive collection will provide any woodcarving artist with a virtually unlimited design resource. From buffalo to bears, from wood spirits to wild horses, from sailboats to the Statue of Liberty, this wide-ranging collection covers all of today's most popular subjects.
                        Easy-to-reproduce outline patterns are ready to transfer to your next project. Each pattern can be used individually, in combination with others in the book, or as inspiration for creating new original art.
                        """,
                    ISBN = "978-1565238688",
                    Price = 24.99,
                    Stock = 42,
                    FrontCover = "images/products/front-cover/88e3175e-eb08-4fe2-b74e-37edb098b7d9.jpg",
                    BackCover = "images/products/back-cover/88e3175e-eb08-4fe2-b74e-37edb098b7d9.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("9cce3d25-ff00-4358-8c65-79968e99d8f9"),
                    Author = "Jackie Reardon, Hans Dekkers",
                    Publisher = "Mindset Publishers (April 13, 2023)",
                    Title = "Mindset: a mental guide for sport",
                    GenreId = Guid.Parse("e8350553-ebd1-4830-b0ee-81f85e1b22cc"),
                    Description = """
                        Learn to deal with pressure and enjoy challenges
                        This book teaches you how to be mentally strong. It guides you through the exact same exercises Olympic athletes, world-class performers and business leaders have done to perform at their very best when it matters most. You'll get all the practical tools to train how to stay relaxed and focused at the same time under all circumstances.
                        Mindset describes a new way of thinking in sport. It is written for athletes of all playing levels, coaches and parents of children engaged in (competitive) sports. You will be able to convert anger, impatience, tension and frustration into self-confidence, better focus and more pleasure, transforming your perception of sport and competition forever.
                        Elite performance coach and former professional tennis player Jackie Reardon has trained Olympic gold medallists and world champions using unorthodox exercises, with sport as a metaphor to help them improve their focus and heighten their awareness. Combining her expertise in professional sports and meditation she has developed a hands-on philosophy called 'Friendly Eyes' to guide athletes of all levels to reach their best. Friendly Eyes means: being kind to ourselves, being kind to others and observing without judgment.
                        Both the books and the video courses teach you how to improve your mindset through kindness. Read the rave reviews on friendlyeyes.com and join the mental revolution.
                        """,
                    ISBN = "978-9082797480",
                    Price = 14.95,
                    Stock = 64,
                    FrontCover = "images/products/front-cover/9cce3d25-ff00-4358-8c65-79968e99d8f9.jpg",
                    BackCover = "images/products/back-cover/9cce3d25-ff00-4358-8c65-79968e99d8f9.jpg"
                },

                new Product()
                {
                    Id = Guid.Parse("8686984b-3bc9-4e28-8514-3b868ed75229"),
                    Author = "Jeffrey DeMure AIA",
                    Publisher = "Fountainhead Publishing (May 26, 2021)",
                    Title = "Death to the Org Chart",
                    GenreId = Guid.Parse("5568b5ce-2e7f-4bc4-8af0-a18c976c9531"),
                    Description = """
                        Today's entrepreneurial organizations are about collaboration, teamwork, and results.
                        • • •
                        Can you imagine running your business without the Internet? When is the last time you used a fax machine to send a letter? These questions seem absurd. But think about this one: why are you using a 19th-century concept to organize your business? In 1854, a civil engineer created the first organizational chart (org chart) to define the hierarchy and reporting structure of the New York and Erie Railroad. Shockingly, after over 150 years, this antiquated concept still dominates corporate America. The inherent problem is that organizational charts serve next to no functional purpose on a day-to-day basis. It merely defines a rigid reporting structure that is out of touch with the 21st-century workplace.
                        Today's entrepreneurs need a tool that provides the perspective to view the organization as a unique living organism. Jeffrey DeMure, AIA has designed a new model for organizing your team that serves as an assessment tool to unearth imbalance within your organization and a roadmap for achieving that much-sought-after balance.
                        """,
                    ISBN = "978-1732207035",
                    Price = 22.95,
                    Stock = 24,
                    FrontCover = "images/products/front-cover/8686984b-3bc9-4e28-8514-3b868ed75229.jpg",
                    BackCover = "images/products/back-cover/8686984b-3bc9-4e28-8514-3b868ed75229.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("f3ad0493-bbcf-49a9-b295-634c509ed90a"),
                    Author = "Eisha Armstrong",
                    Publisher = "Vecteris (May 2, 2021)",
                    Title = "Productize",
                    GenreId = Guid.Parse("5568b5ce-2e7f-4bc4-8af0-a18c976c9531"),
                    Description = """
                        More and more traditional professional services firms are turning to "productization" as a strategy to grow, improve valuations, and to fend off new digital-first competitors. However, many of them will fail and waste a lot of money in the process. Productize first outlines the "Seven Deadly Productization Mistakes" made when pursuing a product strategy, then provides the blueprint for overcoming each of these missteps. It is designed to be a practical playbook for any leader of a professional services business who wants to successfully accelerate growth.
                        For companies that deliver highly customized services, new product development and commercialization are often outside of their core skills, processes, and mindsets. Productizing services typically requires organizations to think differently about how they work and how they create value for their customers. This change does not come easily.
                        Productize includes real-life case studies and stories featuring professional services leaders who have successfully led their organizations to create more scalable services and products. It also includes more than two dozen tools and templates to help your team implement the tactics so you don't have to start from scratch.

                        In this book, you'll learn:
                        • How to shift your culture to embrace a product-mindset
                        • The capabilities that you to be successful and whether or not you should acquire them or grow them internally
                        • How much money to invest in exploring and building more scalable solutions and products
                        • How to ensure there is a viable market for your product idea
                        • How to sequence investments in new product development
                        • How to successfully source and work with developers and data scientists
                        • How to inexpensively test your ideas before investing in development
                        • How to win the hearts and minds of your sales team to ensure your new products are commercially successful
                        • Bonus: Key point summaries at the end of each chapter to help you lock in what you learn
                        • Real Life Case Studies: featuring professional services leaders who have successfully led their organizations to create more scalable services and products

                        Productize draws on the 25+ years of experience that Eisha Armstrong has in successfully creating, launching and growing productized services. Eisha knows what works and what doesn't and she is passionate about making sure organizations learn from each other and avoid reinventing the wheel.
                        """,
                    ISBN = "978-1736929612",
                    Price = 18.22,
                    Stock = 52,
                    FrontCover = "images/products/front-cover/f3ad0493-bbcf-49a9-b295-634c509ed90a.jpg",
                    BackCover = "images/products/back-cover/f3ad0493-bbcf-49a9-b295-634c509ed90a.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("b1390bff-03f0-457c-9b61-039d14ec7e9c"),
                    Author = "The Montauk Group",
                    Publisher = "Independently published (December 14, 2023)",
                    Title = "Made For More",
                    GenreId = Guid.Parse("5568b5ce-2e7f-4bc4-8af0-a18c976c9531"),
                    Description = """
                        Made for More was created to help the ambitious professional upgrade their career and lifestyle. ?
                        This 90 day self-paced career relaunch is designed to help you find what's next.
                        That's Me. ⬇
                        • The successful professional with 35+ years of experience who wishes to reinvent themselves by repurposing their acquired skills and finding new work that brings them energy. ✨
                        • The recently laid off professional who needs to think creatively about a new career path. 🧭
                        • The lifelong employee who is flirting with the idea of entrepreneurship and wants to start exploring whether or not they should make the leap. 🤔
                        • The professional who is economically satisfied and looking to figure out how to give back and deepen their legacy. ✔
                        • Anyone wanting to reinvigorate their career. 💥

                        Made for More is designed to fit seamlessly into your schedule. You choose when and where to learn, ensuring progress at a pace that suits you best. With access to our comprehensive curriculum, change has never been easier.
                        Our career coaching program is ideal for those seeking to balance work and learning, ensuring career development without geographical constraints. Moreover, it allows for anonymity and confidentiality for those who prefer privacy in their career development journey.

                        It's time to take control of your career growth!
                        """,
                    ISBN = "979-8871842140",
                    Price = 29.99 ,
                    Stock = 74,
                    FrontCover = "images/products/front-cover/b1390bff-03f0-457c-9b61-039d14ec7e9c.jpg",
                    BackCover = "images/products/back-cover/b1390bff-03f0-457c-9b61-039d14ec7e9c.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("5db1a28b-6ba1-4b89-8e27-a634f6bc3dac"),
                    Author = "Casey McQuiston",
                    Publisher = "St. Martin's Griffin (May 14, 2019)",
                    Title = "Red, White & Royal Blue",
                    GenreId = Guid.Parse("b63e0779-7a0e-4b76-9469-8e27beebf898"),
                    Description = """
                        What happens when America's First Son falls in love with the Prince of Wales?
                        When his mother became President, Alex Claremont-Diaz was promptly cast as the American equivalent of a young royal. Handsome, charismatic, genius-his image is pure millennial-marketing gold for the White House. There's only one problem: Alex has a beef with the actual prince, Henry, across the pond. And when the tabloids get hold of a photo involving an Alex-Henry altercation, U.S./British relations take a turn for the worse.
                        Heads of family, state, and other handlers devise a plan for damage control: staging a truce between the two rivals. What at first begins as a fake, Instragramable friendship grows deeper, and more dangerous, than either Alex or Henry could have imagined. Soon Alex finds himself hurtling into a secret romance with a surprisingly unstuffy Henry that could derail the campaign and upend two nations and begs the question: Can love save the world after all? Where do we find the courage, and the power, to be the people we are meant to be? And how can we learn to let our true colors shine through? Casey McQuiston's Red, White & Royal Blue proves: true love isn't always diplomatic.
                        """,
                    ISBN = "978-1250316776",
                    Price = 20.59,
                    Stock = 52,
                    FrontCover = "images/products/front-cover/5db1a28b-6ba1-4b89-8e27-a634f6bc3dac.jpg",
                    BackCover = "images/products/back-cover/5db1a28b-6ba1-4b89-8e27-a634f6bc3dac.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("0b8a9931-5380-47b8-b8fd-3d46c5a67852"),
                    Author = "Christina Lauren",
                    Publisher = "Amazon Original Stories (January 23, 2024)",
                    Title = "The Exception to the Rule",
                    GenreId = Guid.Parse("b63e0779-7a0e-4b76-9469-8e27beebf898"),
                    Description = """
                        When Katrina "Kat" Vallia, an idealistic if somewhat naive 20-something American pediatrician travels halfway across the globe to volunteer in a poor African village, she looks at it as a means of closure. Following a bitter breakup with her unfaithful boyfriend, she decides to throw herself into her work without any distractions from a man. That's until she encounters Dr. Julian Kiron, a handsome, career driven pediatric oncologist. Even though she tries to fight it, Kat finds herself falling deeply for him; until she painfully realizes that they both want totally different things from life. Not willing to compromise for the other, they sadly say goodbye, cutting all ties. Five years later, Kat's happy world is turned upside down when she is given devastating news. She must now confront her past and the secret she's harbored for years. But will saving the one she loves most also rekindle the love she let slip away?
                        """,
                    ISBN = "978-0615739229",
                    Price = 12.00,
                    Stock = 52,
                    FrontCover = "images/products/front-cover/0b8a9931-5380-47b8-b8fd-3d46c5a67852.jpg",
                    BackCover = "images/products/back-cover/0b8a9931-5380-47b8-b8fd-3d46c5a67852.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("420e044f-2059-483b-9598-41da2d9da0f0"),
                    Author = "Colleen Hoover",
                    Publisher = "Atria Books; First Edition (August 2, 2016)",
                    Title = "It Ends with Us",
                    GenreId = Guid.Parse("b63e0779-7a0e-4b76-9469-8e27beebf898"),
                    Description = """
                        From the #1 New York Times bestselling author of It Starts with Us and All Your Perfects, a "brave and heartbreaking novel that digs its claws into you and doesn't let go, long after you've finished it" (Anna Todd, New York Times bestselling author) about a workaholic with a too-good-to-be-true romance can't stop thinking about her first love-soon to be a major motion picture starring Blake Lively and Justin Baldoni.
                        Lily hasn't always had it easy, but that's never stopped her from working hard for the life she wants. She's come a long way from the small town where she grew up-she graduated from college, moved to Boston, and started her own business. And when she feels a spark with a gorgeous neurosurgeon named Ryle Kincaid, everything in Lily's life seems too good to be true.
                        Ryle is assertive, stubborn, maybe even a little arrogant. He's also sensitive, brilliant, and has a total soft spot for Lily. And the way he looks in scrubs certainly doesn't hurt. Lily can't get him out of her head. But Ryle's complete aversion to relationships is disturbing. Even as Lily finds herself becoming the exception to his "no dating" rule, she can't help but wonder what made him that way in the first place.
                        As questions about her new relationship overwhelm her, so do thoughts of Atlas Corrigan-her first love and a link to the past she left behind. He was her kindred spirit, her protector. When Atlas suddenly reappears, everything Lily has built with Ryle is threatened.
                        An honest, evocative, and tender novel, It Ends with Us is "a glorious and touching read, a forever keeper. The kind of book that gets handed down" (USA TODAY).
                        """,
                    ISBN = "978-1501110368",
                    Price = 14.50,
                    Stock = 34,
                    FrontCover = "images/products/front-cover/420e044f-2059-483b-9598-41da2d9da0f0.jpg",
                    BackCover = "images/products/back-cover/420e044f-2059-483b-9598-41da2d9da0f0.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("4b8a4d0f-d00d-4a6d-8e92-a9e739c1ab55"),
                    Author = "Annette Oppenlander",
                    Publisher = "Oppenlander Enterprises LLC (February 16, 2017)",
                    Title = "47 Days",
                    GenreId = Guid.Parse("2cd9ea1b-1b93-4977-8587-f9329f0e2078"),
                    Description = """
                        "47 DAYS" is a novelette and an excerpt from the award-winning biographical novel, SURVIVING THE FATHERLAND--A True Coming-of-age Love Story Set in WWII Germany.
                        In March 1945 Hitler ordered his last propaganda command: send all 15 and 16-year old boys to defend the fatherland. 47 DAYS tells the true story of Günter and Helmut, best friends, who dared to defy and disobey. Without knowing how long the war might continue, they spent 47 harrowing days as fugitives on the run. Being caught meant certain execution.
                        SURVIVING THE FATHERLAND tells the true and heart-wrenching stories of Lilly and Günter struggling with the terror-filled reality of life in the Third Reich, each embarking on their own dangerous path toward survival, freedom, and ultimately each other. Based on the author's own family and anchored in historical facts, this story celebrates the resilience of the human spirit and the strength of war children.
                        """,
                    ISBN = "978-0997780062",
                    Price = 6.99,
                    Stock = 24,
                    FrontCover = "images/products/front-cover/4b8a4d0f-d00d-4a6d-8e92-a9e739c1ab55.jpg",
                    BackCover = "images/products/back-cover/4b8a4d0f-d00d-4a6d-8e92-a9e739c1ab55.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("f14d7b67-dbbd-485d-9572-db089aa09d34"),
                    Author = "Ernest Dempsey",
                    Publisher = "138 Publishing (September 14, 2021)",
                    Title = "The Secret of the Stones",
                    GenreId = Guid.Parse("2cd9ea1b-1b93-4977-8587-f9329f0e2078"),
                    Description = """
                        Sean Wyatt's learns his best friend has been kidnapped while working on a secret project. Sean and his new acquaintance, Allyson Webster, embark on a mission to solve a series of ancient clues they he hopes will lead to whoever kidnapped his friend. The riddles lead them on a dangerous chase through the southeastern United States and to a four thousand year-old secret that is bigger than anything they could have ever imagined.
                        """,
                    ISBN = "978-1944647865",
                    Price = 19.99,
                    Stock = 52,
                    FrontCover = "images/products/front-cover/f14d7b67-dbbd-485d-9572-db089aa09d34.jpg",
                    BackCover = "images/products/back-cover/f14d7b67-dbbd-485d-9572-db089aa09d34.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("8ac31a09-36d7-45af-8d85-96a550ae315e"),
                    Author = "Rob Baddorf",
                    Publisher = "Jar of Lightning (August 12, 2023)",
                    Title = "Recruited by the FBI",
                    GenreId = Guid.Parse("2cd9ea1b-1b93-4977-8587-f9329f0e2078"),
                    Description = """
                        When Robin gets in over his head working with the FBI against a suspected terrorist, his trust in God's goodness is pushed to the limits.
                        Robin and his two best friends, Chad and Anika, have the best after-school job ever--they work for the FBI! The team fire grappling hooks and rappel onto the roof of a bank. This caper is supposed to be quick and easy, in-and-out and nobody gets hurt. The team is there only to spy and find a secret book that reveals the company's true financial records. But when the job escalates, the team needs more help.
                        Only Robin won't allow it!
                        Having lost his own father in the line of duty, Robin won't let anyone else join his team. Or get too close! Yet, soon enough, Isabella, with her computer hacking skills, works her way into his heart. As their game of cat and mouse with the criminal CEO intensifies, Robin wrestles to keep his team safe. Can Robin do it on his own, or will he have to dig up an old bitterness in order to fully trust God again?
                        Get your copy of this faith-building action-adventure novel now!
                        """,
                    ISBN = "978-1956061468",
                    Price = 11.99,
                    Stock = 62,
                    FrontCover = "images/products/front-cover/8ac31a09-36d7-45af-8d85-96a550ae315e.jpg",
                    BackCover = "images/products/back-cover/8ac31a09-36d7-45af-8d85-96a550ae315e.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("8e47533c-1983-438b-9f2c-5efd0162b302"),
                    Author = "BJ Bourg",
                    Publisher = "Independently published (November 4, 2018)",
                    Title = "But Not Forgotten",
                    GenreId = Guid.Parse("3c232235-c2d6-42ed-b6ac-569e7f75a192"),
                    Description = """
                        Embattled former detective Clint Wolf is the newly appointed police chief for Mechant Loup, a small swampy town in southeast Louisiana. Usually a quiet town, the tranquility of the place is shattered when a human arm is found in the jowls of an alligator. Once it's determined the arm belongs to a reputable business owner, the race is on to find the man and figure out what happened to him. Little does Clint know that solving the case could unearth a plot so evil it would go down as the worst event in Louisiana history . . . and he might not live to see it.(NOTE: Originally published on December 6, 2015 by Amber Quill Press, LLC)
                        """,
                    ISBN = "978-1730882067",
                    Price = 10.87,
                    Stock = 25,
                    FrontCover = "images/products/front-cover/8e47533c-1983-438b-9f2c-5efd0162b302.jpg",
                    BackCover = "images/products/back-cover/8e47533c-1983-438b-9f2c-5efd0162b302.jpg"
                },

                new Product()
                {
                    Id = Guid.Parse("092157ca-50ab-49e4-9ce3-f491eb0b06e6"),
                    Author = "Jasmine Webb",
                    Publisher = "Independently published (September 26, 2023)",
                    Title = "Dead to Rights",
                    GenreId = Guid.Parse("3c232235-c2d6-42ed-b6ac-569e7f75a192"),
                    Description = """
                        Mackenzie-Mack-Owens was supposed to live out every millennial's dream, but instead it turned into a nightmare. Inheriting a house and bookstore, she was going to leave the New York City rat race after getting fired from her job and start a new life on the coast of Cornwall, in England. No more fourteen-hour work days and rats the size of small toddlers. Hello turquoise waters, white sand beaches and cobblestone streets.
                        Unfortunately, things don't exactly go as planned.
                        Not only does she have to share this new home with an eccentric grandmother she's never met, but when she opens the bookstore to find a dead body on the ground, the police immediately hone in on Mack and her grandmother as the prime suspects. This move wasn't supposed to mean a life spent in prison making toilet wine.
                        Instead of spending her days eating scones with jam and cream and selling books by the sea, she's now got to work with her grandmother to find the killer and clear her own name. Sounds simple, right? Armed with nothing more than her wits and snarky attitude, Mack is about to find out that the charming coastal town has a secret dark side. And the aggravating private investigator with the sexy smile she keeps running into has something to hide, but what?
                        As she gets closer to finding the killer, Mack realizes she's next on the hit list. Will she get to the bottom of this case before she finds herself at the bottom of the sea?
                        """,
                    ISBN = "979-8862487954",
                    Price = 12.99,
                    Stock = 72,
                    FrontCover = "images/products/front-cover/092157ca-50ab-49e4-9ce3-f491eb0b06e6.jpg",
                    BackCover = "images/products/back-cover/092157ca-50ab-49e4-9ce3-f491eb0b06e6.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("e37abde9-5fc7-4154-9df7-56c92fe2c330"),
                    Author = "Walter Sticht",
                    Publisher = "Independently published (February 1, 2020)",
                    Title = "Audacity",
                    GenreId = Guid.Parse("3c232235-c2d6-42ed-b6ac-569e7f75a192"),
                    Description = """
                        A rebellious yet principled private detective wants to stay away from a case he doesn't care to revisit. Thinking his side business as a restaurateur will keep him occupied, the restaurant ultimately leads to another case he comes to realize he is obligated to finish. In the process, he meets the people who will forever alter his life both for the good and bad.
                        """,
                    ISBN = "979-8607834807",
                    Price = 16.95,
                    Stock = 42,
                    FrontCover = "images/products/front-cover/e37abde9-5fc7-4154-9df7-56c92fe2c330.jpg",
                    BackCover = "images/products/back-cover/e37abde9-5fc7-4154-9df7-56c92fe2c330.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("c24b0349-0ee8-4e05-b5d9-220888902d17"),
                    Author = "J. D. Robb",
                    Publisher = "St. Martin's Press (January 23, 2024)",
                    Title = "Random in Death",
                    GenreId = Guid.Parse("86d4e2c3-10ad-4a90-be21-9ec3acec8fc9"),
                    Description = """
                        Jenna's parents had finally given in, and there she was, at a New York club with her best friends, watching the legendary band Avenue A, carrying her demo in hopes of slipping it to the guitarist, Jake Kincade. Then, from the stage, Jake catches her eye, and smiles. It's the best night of her life.
                        It's the last night of her life.
                        Minutes later, Jake's in the alley getting some fresh air, and the girl from the dance floor comes stumbling out, sick and confused and deathly pale. He tries to help, but it's no use. He doesn't know that someone in the crowd has jabbed her with a needle-and when his girlfriend Nadine arrives, she knows the only thing left to do for the girl is call her friend, Lieutenant Eve Dallas.
                        After everyone on the scene is interviewed, lab results show a toxic mix of substances in the victim's body-and for an extra touch of viciousness, the needle was teeming with infectious agents. Dallas searches for a pattern: Had any boys been harassing Jenna? Was she engaging in risky behavior or caught up in something shady? But there are no obvious clues why this levelheaded sixteen-year-old, passionate about her music, would be targeted.
                        And that worries Dallas. Because if Jenna wasn't targeted, if she was just the random, unlucky victim of a madman consumed by hatred, there are likely more deaths to come.
                        """,
                    ISBN = "978-1250289544",
                    Price = 20.98,
                    Stock = 61,
                    FrontCover = "images/products/front-cover/c24b0349-0ee8-4e05-b5d9-220888902d17.jpg",
                    BackCover = "images/products/back-cover/c24b0349-0ee8-4e05-b5d9-220888902d17.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("58fda9f9-296c-4784-976e-01bffffb20dc"),
                    Author = "Alex Michaelides",
                    Publisher = "Celadon Books; Reprint edition (May 4, 2021)",
                    Title = "The Silent Patient",
                    GenreId = Guid.Parse("86d4e2c3-10ad-4a90-be21-9ec3acec8fc9"),
                    Description = """
                        "An unforgettable-and Hollywood-bound-new thriller... A mix of Hitchcockian suspense, Agatha Christie plotting, and Greek tragedy." - Entertainment Weekly
                        The Silent Patient is a shocking psychological thriller of a woman's act of violence against her husband-and of the therapist obsessed with uncovering her motive.
                        Alicia Berenson's life is seemingly perfect. A famous painter married to an in-demand fashion photographer, she lives in a grand house with big windows overlooking a park in one of London's most desirable areas. One evening her husband Gabriel returns home late from a fashion shoot, and Alicia shoots him five times in the face, and then never speaks another word.
                        Alicia's refusal to talk, or give any kind of explanation, turns a domestic tragedy into something far grander, a mystery that captures the public imagination and casts Alicia into notoriety. The price of her art skyrockets, and she, the silent patient, is hidden away from the tabloids and spotlight at the Grove, a secure forensic unit in North London.
                        Theo Faber is a criminal psychotherapist who has waited a long time for the opportunity to work with Alicia. His determination to get her to talk and unravel the mystery of why she shot her husband takes him down a twisting path into his own motivations-a search for the truth that threatens to consume him....
                        """,
                    ISBN = "978-1250301703",
                    Price = 14.35,
                    Stock = 46,
                    FrontCover = "images/products/front-cover/58fda9f9-296c-4784-976e-01bffffb20dc.jpg",
                    BackCover = "images/products/back-cover/58fda9f9-296c-4784-976e-01bffffb20dc.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("940da020-03da-4ff0-91fd-455089dbc7d6"),
                    Author = "Amina Akhtar",
                    Publisher = "Mindy's Book Studio (February 1, 2024)",
                    Title = "Almost Surely Dead",
                    GenreId = Guid.Parse("86d4e2c3-10ad-4a90-be21-9ec3acec8fc9"),
                    Description = """
                        "Amina Akhtar's Almost Surely Dead is a witty, fresh psychological thriller that's part stalker thriller, part ghost story. This book took turns I never saw coming. I was up all night, tearing through the pages as the mysterious pieces came together, leading to an explosive conclusion." -Mindy Kaling
                        A psychological thriller with a twist, Almost Surely Dead is a chilling account of how one woman's life spins out of control after a terrifying-and seemingly random-attempt on her life.
                        Dunia Ahmed lives an ordinary life-or she definitely used to. Now she's the subject of a true crime podcast. She's been missing for over a year, and no one knows if she's dead or alive. But her story has listeners obsessed, and people everywhere are sporting merch that demands "Find Dunia!"
                        In the days before her disappearance, Dunia is a successful pharmacist living in New York. The daughter of Pakistani immigrants, she's coping with a broken engagement and the death of her mother. But then something happens that really shakes up her world: someone tries to murder her.
                        When her would-be killer winds up dead, Dunia figures the worst is over. But then there's another attempt on her life.and another. And police suspect someone close to her may be the culprit. Dunia struggles to make sense of what's happening. And as childhood superstitions seep into her reality, she becomes convinced that someone-or something-is truly after her.
                        """,
                    ISBN = "978-1662507588",
                    Price = 20.00,
                    Stock = 54,
                    FrontCover = "images/products/front-cover/940da020-03da-4ff0-91fd-455089dbc7d6.jpg",
                    BackCover = "images/products/back-cover/940da020-03da-4ff0-91fd-455089dbc7d6.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("ba352fa1-d7c2-4ec6-9759-6e6928db37cc"),
                    Author = "James Holler",
                    Publisher = "Independently published (December 11, 2022)",
                    Title = "The Microsoft Office 365 Bible",
                    GenreId = Guid.Parse("f45196b2-6137-4bd3-94b1-21175ece64c4"),
                    Description = """
                        NOW! Stop wasting time and money trying to figure out everything yourself and master all the functions of the Office Suite!
                        If you are a fan of PC and use it for work, entertainment, or anything else, mastering main Microsoft Programs is a MUST.
                        I can't tell you enough how many people I see not just struggling to use a program like EXCEL, WORD, POWERPOINT, ONE NOTE, ONE DRIVE, OUTLOOK, TEAMS, ACCESS, PUBLISHER, and others, but also. wasting so much time doing things that should take minutes and even seconds instead of hours and days.
                        On top of that, on average most people use less than 5% of programs' full potential at any given time.
                        For this exact reason, I created this amazing, in-depth book BUNDLE - to help you master these programs in no time, even if you don't have any experience.
                        """,
                    ISBN = "979-8368031231",
                    Price = 27.61,
                    Stock = 23,
                    FrontCover = "images/products/front-cover/ba352fa1-d7c2-4ec6-9759-6e6928db37cc.jpg",
                    BackCover = "images/products/back-cover/ba352fa1-d7c2-4ec6-9759-6e6928db37cc.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("943223bc-8c3a-4683-b140-a24bf62131c1"),
                    Author = "Chris Fregly, Antje Barth, Shelbee Elgenbrode",
                    Publisher = "O'Reilly Media; 1st edition (December 19, 2023)",
                    Title = "Generative AI on AWS",
                    GenreId = Guid.Parse("f45196b2-6137-4bd3-94b1-21175ece64c4"),
                    Description = """
                        Companies today are moving rapidly to integrate generative AI into their products and services. But there's a great deal of hype (and misunderstanding) about the impact and promise of this technology. With this book, Chris Fregly, Antje Barth, and Shelbee Eigenbrode from AWS help CTOs, ML practitioners, application developers, business analysts, data engineers, and data scientists find practical ways to use this exciting new technology.

                        You'll learn the generative AI project life cycle including use case definition, model selection, model fine-tuning, retrieval-augmented generation, reinforcement learning from human feedback, and model quantization, optimization, and deployment. And you'll explore different types of models including large language models (LLMs) and multimodal models such as Stable Diffusion for generating images and Flamingo/IDEFICS for answering questions about images.

                        Apply generative AI to your business use cases
                        Determine which generative AI models are best suited to your task
                        Perform prompt engineering and in-context learning
                        Fine-tune generative AI models on your datasets with low-rank adaptation (LoRA)
                        Align generative AI models to human values with reinforcement learning from human feedback (RLHF)
                        Augment your model with retrieval-augmented generation (RAG)
                        Explore libraries such as LangChain and ReAct to develop agents and actions
                        Build generative AI applications with Amazon Bedrock
                        """,
                    ISBN = "978-1098159221",
                    Price = 59.99,
                    Stock = 26,
                    FrontCover = "images/products/front-cover/943223bc-8c3a-4683-b140-a24bf62131c1.jpg",
                    BackCover = "images/products/back-cover/943223bc-8c3a-4683-b140-a24bf62131c1.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("9cf22402-02f4-4c07-bf32-14bd8af5e692"),
                    Author = "Sam Newman",
                    Publisher = "O'Reilly Media; 2nd edition (September 28, 2021)",
                    Title = "Building Microservices",
                    GenreId = Guid.Parse("f45196b2-6137-4bd3-94b1-21175ece64c4"),
                    Description = """
                        As organizations shift from monolithic applications to smaller, self-contained microservices, distributed systems have become more fine-grained. But developing these new systems brings its own host of problems. This expanded second edition takes a holistic view of topics that you need to consider when building, managing, and scaling microservices architectures.
                        Through clear examples and practical advice, author Sam Newman gives everyone from architects and developers to testers and IT operators a firm grounding in the concepts. You'll dive into the latest solutions for modeling, integrating, testing, deploying, and monitoring your own autonomous services. Real-world cases reveal how organizations today manage to get the most out of these architectures.
                        Microservices technologies continue to move quickly. This book brings you up to speed.
                         Get new information on user interfaces, container orchestration, and serverless
                         Align system design with your organization's goals
                         Explore options for integrating a service with your system
                         Understand how to independently deploy microservices
                         Examine the complexities of testing and monitoring distributed services
                         Manage security with expanded content around user-to-service and service-to-service models
                        """,
                    ISBN = "978-1492034025",
                    Price = 45.10,
                    Stock = 82,
                    FrontCover = "images/products/front-cover/9cf22402-02f4-4c07-bf32-14bd8af5e692.jpg",
                    BackCover = "images/products/back-cover/9cf22402-02f4-4c07-bf32-14bd8af5e692.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("72da32f6-144d-4039-831c-9f9cb5eeca69"),
                    Author = "Joseph Nguyen",
                    Publisher = "Independently published (March 28, 2022)",
                    Title = "Don't Believe Everything You Think",
                    GenreId = Guid.Parse("7621b5bf-97f8-4ff2-884b-9caccdcc8513"),
                    Description = """
                        Learn how to overcome anxiety, self-doubt & self-sabotage without needing to rely on motivation or willpower.
                        In this book, you'll discover the root cause of all psychological and emotional suffering and how to achieve freedom of mind to effortlessly create the life you've always wanted to live.
                        Although pain is inevitable, suffering is optional.
                        This book offers a completely new paradigm and understanding of where our human experience comes from, allowing us to end our own suffering and create how we want to feel at any moment.

                        In This Book, You'll Discover:

                        • The root cause of all psychological and emotional suffering and how to end it
                        • How to become unaffected by negative thoughts and feelings
                        • How to experience unconditional love, peace, and joy in the present, no matter what our external circumstances look like
                        • How to instantly create a new experience of life if you don't like the one you're in right now
                        • How to break free from a negative thought loop when we inevitably get caught in one
                        • How to let go of anxiety, self-doubt, self-sabotage, and any self-destructive habits
                        • How to effortlessly create from a state of abundance, flow, and ease
                        • How to develop the superpower of being okay with not knowing and uncertainty
                        • How to access your intuition and inner wisdom that goes beyond the limitations of thinking
                        • No matter what has happened to you, where you are from, or what you have done, you can still find total peace, unconditional love, complete fulfillment, and an abundance of joy in your life.

                        No person is an exception to this. Darkness only exists because of the light, which means even in our darkest hour, light must exist.
                        """,
                    ISBN = "979-8427063852",
                    Price = 22.49,
                    Stock = 64,
                    FrontCover = "images/products/front-cover/72da32f6-144d-4039-831c-9f9cb5eeca69.jpg",
                    BackCover = "images/products/back-cover/72da32f6-144d-4039-831c-9f9cb5eeca69.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("dc3c290e-1d16-4b73-9171-50ecf7d66ebe"),
                    Author = "Rob Elliott",
                    Publisher = "Revell (August 1, 2010)",
                    Title = "Laugh-Out-Loud Jokes for Kids",
                    GenreId = Guid.Parse("7621b5bf-97f8-4ff2-884b-9caccdcc8513"),
                    Description = """
                        What happens to race car drivers when they eat too much? They get indy-gestion. Laugh-Out-Loud Jokes for Kids provides children ages 7-10 many hours of fun and laughter. Young readers will have a blast sharing this collection of hundreds of one-liners, knock knock jokes, tongue twisters, and more with their friends and family! This mega-bestselling book will have children rolling on the floor with laughter and is sure to be a great gift idea for any child.
                        """,
                    ISBN = "978-0800788032",
                    Price = 4.99,
                    Stock = 120,
                    FrontCover = "images/products/front-cover/dc3c290e-1d16-4b73-9171-50ecf7d66ebe.jpg",
                    BackCover = "images/products/back-cover/dc3c290e-1d16-4b73-9171-50ecf7d66ebe.jpg"
                },

                new Product()
                {
                    Id = Guid.Parse("c2ac69c2-ca86-4ef7-981a-1c79396448c0"),
                    Author = "Ethan Miller",
                    Publisher = "Independently published (January 8, 2024)",
                    Title = "Shower Thoughts",
                    GenreId = Guid.Parse("7621b5bf-97f8-4ff2-884b-9caccdcc8513"),
                    Description = """
                        Dive into the intriguing world of "Shower Thoughts: 200 Droplets of Wisdom," where each page turns a simple shower moment into a profound realization. This collection brings together 200 unique and often whimsical reflections that occur in the solitude of the shower, offering a glimpse into the hidden depths of everyday thinking. From lighthearted oddities to surprising truths about life, these thoughts capture the extraordinary in the ordinary. Each 'droplet' serves as a reminder of the unexpected wisdom that can spring from our most routine moments. Perfect for a quick read or a thoughtful pause, this book is a treasure trove of small yet significant insights that will amuse, inspire, and enlighten you-one shower thought at a time.
                        """,
                    ISBN = "979-8874363079",
                    Price = 8.99,
                    Stock = 112,
                    FrontCover = "images/products/front-cover/c2ac69c2-ca86-4ef7-981a-1c79396448c0.jpg",
                    BackCover = "images/products/back-cover/c2ac69c2-ca86-4ef7-981a-1c79396448c0.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("fca8c31f-ab90-423b-92fb-40739046961a"),
                    Author = "Various",
                    Publisher = "Hal Leonard; 1st edition (January 1, 2014)",
                    Title = "100 of the Most Beautiful Piano Solos Ever",
                    GenreId = Guid.Parse("3c99605d-496b-4b96-a04a-094477ef2c4d"),
                    Description = """
                        (Piano Solo Songbook). 100 pop and classical standards that every piano player should master, including: Air on the G String * Bridge over Troubled Water * Canon in D * Clair de Lune * Fields of Gold * Fur Elise * I Dreamed a Dream * I Will Always Love You * Imagine * Lullaby of Birdland * Memory * Misty * Moon River * On My Own * Over the Rainbow * The Shadow of Your Smile * Smile * Stardust * Summertime * Sunrise, Sunset * Time After Time * Unexpected Song * The Way You Look Tonight * We've Only Just Begun * What a Wonderful World * Yesterday * You Raise Me Up * Your Song * and more!
                        """,
                    ISBN = "978-1476814766",
                    Price = 25.77,
                    Stock = 58,
                    FrontCover = "images/products/front-cover/fca8c31f-ab90-423b-92fb-40739046961a.jpg",
                    BackCover = "images/products/back-cover/fca8c31f-ab90-423b-92fb-40739046961a.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("263ef2ad-e665-45e0-a1cc-c49d5549f7c3"),
                    Author = "Dave Grohl",
                    Publisher = "Dey Street Books; First Edition (October 5, 2021)",
                    Title = "The Storyteller",
                    GenreId = Guid.Parse("3c99605d-496b-4b96-a04a-094477ef2c4d"),
                    Description = """
                        Having entertained the idea for years, and even offered a few questionable opportunities ("It's a piece of cake! Just do 4 hours of interviews, find someone else to write it, put your face on the cover, and voila!") I have decided to write these stories just as I have always done, in my own hand. The joy that I have felt from chronicling these tales is not unlike listening back to a song that I've recorded and can't wait to share with the world, or reading a primitive journal entry from a stained notebook, or even hearing my voice bounce between the Kiss posters on my wall as a child.

                        This certainly doesn't mean that I'm quitting my day job, but it does give me a place to shed a little light on what it's like to be a kid from Springfield, Virginia, walking through life while living out the crazy dreams I had as young musician. From hitting the road with Scream at 18 years old, to my time in Nirvana and the Foo Fighters, jamming with Iggy Pop or playing at the Academy Awards or dancing with AC/DC and the Preservation Hall Jazz Band, drumming for Tom Petty or meeting Sir Paul McCartney at Royal Albert Hall, bedtime stories with Joan Jett or a chance meeting with Little Richard, to flying halfway around the world for one epic night with my daughters.the list goes on. I look forward to focusing the lens through which I see these memories a little sharper for you with much excitement.
                        """,
                    ISBN = "978-0063076099",
                    Price = 16.10,
                    Stock = 102,
                    FrontCover = "images/products/front-cover/263ef2ad-e665-45e0-a1cc-c49d5549f7c3.jpg",
                    BackCover = "images/products/back-cover/263ef2ad-e665-45e0-a1cc-c49d5549f7c3.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("579c441b-ec0e-4515-afd6-1c901640f5e2"),
                    Author = "Wendy Fox",
                    Publisher = "Independently published (November 8, 2023)",
                    Title = "101 Facts About Taylor Swift",
                    GenreId = Guid.Parse("3c99605d-496b-4b96-a04a-094477ef2c4d"),
                    Description = """
                        Do you think you know everything about Taylor Swift?
                        Get ready to challenge your knowledge in 101 Facts About Taylor Swift: Quotes, Quizzes, Journals, and More!
                        If you've ever sung along to a Taylor Swift song, or if you've just discovered her amazing tunes, get ready to become the ultimate Swift expert!
                        Filled with fun facts, this book is like a backstage pass to Taylor's life! Get the scoop on her songs, find fun tidbits about her shows, and see what makes Taylor tick. You'll even get to read some of Taylor's most uplifting quotes that'll make you feel like you can conquer the world!
                        🎶 Challenge your friends (and yourself) with fun quizzes! Do you know the name of Taylor's first album? Can you list all of her cats' names? It's time to put your Swift knowledge to the test and see who's the biggest fan in your squad.
                        But wait, there's more! Grab a pen and get ready to fill the pages with your own stories, dreams, and ideas, inspired by Taylor's lyrics. The journal sections in the book are all about you-your life, hopes, and adventures. It's like writing your own set of songs, with a little help from Taylor herself.
                        From her days strumming a guitar in Nashville to lighting up stages all around the globe, Taylor's not just a singer; she's a storyteller and a dreamer, just like you. "101 Facts About Taylor Swift" isn't just about reading-it's about playing, writing, and dreaming BIG!
                        So grab your glittery pens, get comfy, and dive into the magical world of Taylor Swift. You're not just a fan-you're on a fabulous Swiftie adventure!
                        """,
                    ISBN = "979-8867019495",
                    Price = 6.99,
                    Stock = 200,
                    FrontCover = "images/products/front-cover/579c441b-ec0e-4515-afd6-1c901640f5e2.jpg",
                    BackCover = "images/products/back-cover/579c441b-ec0e-4515-afd6-1c901640f5e2.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("45b33b7f-0c71-4631-8f74-ac8b8cc636f4"),
                    Author = "Dorothy Calimeris, Lulu Cook RDN",
                    Publisher = "Rockridge Press (April 11, 2017)",
                    Title = "The Complete Anti-Inflammatory Diet for Beginners",
                    GenreId = Guid.Parse("510bdcf5-f312-47e2-addf-9778c9d110d1"),
                    Description = """
                        Reduce inflammation and ease chronic pain with this beginner-friendly anti-inflammatory cookbook
                        Did you know making dietary changes, like eliminating processed foods, can help lower the inflammation believed to be a key contributor to chronic pain. With recipes and shopping lists, this essential anti-inflammation cookbook makes it easy for you to start and follow an anti-inflammatory diet that is easily customizable for specific inflammatory. conditions.
                        
                        What sets this inflammation diet cookbook apart:

                        • EASY MEAL PLANNING: This book includes a simple 2-week meal plan featuring anti-inflammatory ingredients and handy shopping lists to help kick-start the diet.
                        • DISCOVER SIMPLE, SATISFYING RECIPES: The majority of these healthy recipes require just 5 easy-to-source main ingredients found at most grocery stores. Find a range of mediterranean-style meals from roast chicken with a side of white beans to a hearty lentil & beet salad.
                        • FOOD COACHING: Consult this cookbook's helpful lists to find out which foods to enjoy and which foods to avoid on an anti-inflammatory diet.
                        Make a simple change in your diet to reduce your body's inflammation with The Complete Anti-Inflammatory Diet for Beginners.
                        """,
                    ISBN = "978-1623159047",
                    Price = 12.80,
                    Stock = 82,
                    FrontCover = "images/products/front-cover/45b33b7f-0c71-4631-8f74-ac8b8cc636f4.jpg",
                    BackCover = "images/products/back-cover/45b33b7f-0c71-4631-8f74-ac8b8cc636f4.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("db9e68b1-fe91-4944-9d9f-570135496b57"),
                    Author = "Jessie Inchauspe",
                    Publisher = "S&S/Simon Element (April 5, 2022)",
                    Title = "Glucose Revolution",
                    GenreId = Guid.Parse("510bdcf5-f312-47e2-addf-9778c9d110d1"),
                    Description = """
                        Improve all areas of your health-your sleep, cravings, mood, energy, skin, weight-and even slow down aging with easy, science-based hacks to manage your blood sugar while still eating the foods you love.
                        Glucose, or blood sugar, is a tiny molecule in our body that has a huge impact on our health. It enters our bloodstream through the starchy or sweet foods we eat. Ninety percent of us suffer from too much glucose in our system-and most of us don't know it.
                        The symptoms? Cravings, fatigue, infertility, hormonal issues, acne, wrinkles. And over time, the development of conditions like type 2 diabetes, polycystic ovarian syndrome, cancer, dementia, and heart disease.

                        Drawing on cutting-edge science and her own pioneering research, biochemist Jessie Inchauspé offers ten simple, surprising hacks to help you balance your glucose levels and reverse your symptoms-without going on a diet or giving up the foods you love. For example:
                        • How eating foods in the right order will make you lose weight effortlessly
                        • What secret ingredient will allow you to eat dessert and still go into fat-burning mode
                        • What small change to your breakfast will unlock energy and cut your cravings

                        Both entertaining, informative, and packed with the latest scientific data, this book presents a new way to think about better health. Glucose Revolution is chock-full of tips that can drastically and immediately improve your life, whatever your dietary preferences.
                        """,
                    ISBN = "978-1982179410",
                    Price = 16.99,
                    Stock = 58,
                    FrontCover = "images/products/front-cover/db9e68b1-fe91-4944-9d9f-570135496b57.jpg",
                    BackCover = "images/products/back-cover/db9e68b1-fe91-4944-9d9f-570135496b57.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("3d596eab-ccb5-491d-b02d-d891c3b5abf3"),
                    Author = "Daniel Trevor",
                    Publisher = "Pivotal Performance Publishing (September 25, 2023)",
                    Title = "UNHOLY TRINITY",
                    GenreId = Guid.Parse("510bdcf5-f312-47e2-addf-9778c9d110d1"),
                    Description = """
                        In 1930 the US obesity rate was 1 percent. Today, it's almost 50 percent. Never before has a population gotten so fat and sick so fast.
                        What's happening? The UNHOLY TRINITY.
                        Join Heart Attack Survivor Turned Wellness Warrior, Daniel Trevor, as he sheds light on the self-inflicted diseases of our time.
                        Discover the truth about Big Food, Big Pharma, and Big Medicine, where financial interests often overshadow your health and well-being.
                        UNHOLY TRINITY has been endorsed by some of the most prominent physicians, scientists, and health writers in the world, including a winner of the Nobel Prize in Medicine for discovering Nitric Oxide.

                        Find out:

                        • What's the best overall predictor of your longevity?
                        • Why is chronic high blood sugar a pre-cancerous condition?
                        • Why do people with high cholesterol live the longest?
                        • What's the most important low-cost health test you've never had?
                        • What's the number one killer of women 10X breast cancer?
                        • Why do low-fat diets lower men's testosterone?
                        • Why are stents unnecessary 85% of the time?
                        • What large studies proved stents don't prevent heart attacks?
                        • Why can't stress tests predict your heart attack?
                        • What population eats the most meat and enjoys the longest lifespan?
                        • What's in most packaged foods to stimulate your opiate receptors?
                        • Why are Blue Zones called vegetarian when in fact they eat meat?

                        Discover why consuming industrially refined and processed carbohydrates, sugar, and vegetable seed oils (UNHOLY TRINITY) accelerates aging, triggers severe weight gain, and started many new-age ailments.
                        Symptom-free, Daniel Trevor thought he was "Mr. Healthy" but was an undiagnosed Type 2 diabetic, which was the root cause of his undiagnosed heart disease, undiagnosed fatty liver disease, and undiagnosed osteoporosis. Having studied technical data throughout his life he decided to dive into medical and nutrition science to discover how this could happen to him.
                        Using the latest 21st century science that he researched and discovered, he was able to reverse all four silent conditions. Find out how.
                        This book is your practical guide to escape from destructive dietary habits and the hidden agendas of massive industries - get a copy now!
                        """,
                    ISBN = "979-8986040813",
                    Price = 22.93,
                    Stock = 64,
                    FrontCover = "images/products/front-cover/3d596eab-ccb5-491d-b02d-d891c3b5abf3.jpg",
                    BackCover = "images/products/back-cover/3d596eab-ccb5-491d-b02d-d891c3b5abf3.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("d1706805-ddce-4eaf-8c25-4064112bd20f"),
                    Author = "Bill O'Neill",
                    Publisher = "LAK Publishing (October 24, 2022)",
                    Title = "How To Survive A Freakin' Bear Attack",
                    GenreId = Guid.Parse("e8350553-ebd1-4830-b0ee-81f85e1b22cc"),
                    Description = """
                        Would you know what to do if a bear charged at you?
                        What about if you were out driving and an earthquake struck? Or a tornado?
                        What about if you were bitten by a snake in the middle of nowhere, or caught in an avalanche while out skiing or hiking? What if you found yourself the sole survivor of some awful incident, and had to make a new home for yourself stranded on a desert island? Would you know how to spear a fish, or how to distill pure water using only a plastic bottle? Could you make your own rope, your own torch, or tell which of the unfamiliar plants and animals around you may or may not be edible?
                        How To Survive A Freakin' Bear Attack. And 127 Other Survival Hacks You'll Hopefully Never Need has all the answers you could ever need!

                        Packed with step-by-step survival guides to some of the most remarkable scenarios imaginable.
                        Real life advice for some of life's challenges and unpredictable situations.
                        Chapters covering everything from first aid and CPR to climbing trees, building rope bridges, and lighting a campfire using only your phone.
                        Fun and fascinating, yet bursting with real-world knowledge, tips, and advice!
                        Who knows if, when, or indeed how you might need to know any of the 128 extraordinary survival skills included in this book. But life is anything but predictable.

                        Hopefully, you'll never need to know how to flee a sinking ship or a burning building, or be in any kind of situation where you might need to safely package up a severed body part or perform abdominal surgery on yourself.
                        But given that we never quite know what's around the corner the hints and tips included here may one day - hopefully! -come in useful.
                        """,
                    ISBN = "978-1648450914",
                    Price = 23.99,
                    Stock = 92,
                    FrontCover = "images/products/front-cover/d1706805-ddce-4eaf-8c25-4064112bd20f.jpg",
                    BackCover = "images/products/back-cover/d1706805-ddce-4eaf-8c25-4064112bd20f.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("7f2e5b45-4b64-4f26-8a54-787facf48c69"),
                    Author = "Sam Fury",
                    Publisher = "SF Nonfiction Books; Illustrated edition (August 7, 2019)",
                    Title = "The Useful Knots Book",
                    GenreId = Guid.Parse("e8350553-ebd1-4830-b0ee-81f85e1b22cc"),
                    Description = """
                        Discover the Only Knots You'll Ever Need!
                        The Useful Knots Book is a no-nonsense knot guide on how to tie the 25+ most practical rope knots.
                        It comes with easy to follow instructions, pictures, and tips on when to best use each knot.
                        Teach yourself knot tying today, because it's easy, fun, and useful.

                        Get it now.

                        The Ultimate Knots Guide
                        • Explanations of common knots and ropes terms
                        • Easy to follow instructions and clear pictures
                        • Tips for proper rope care
                        • Advice on how to choose right knot for the job
                         • All the fundamental boy scout knots
                        """,
                    ISBN = "978-1925979022",
                    Price = 5.98,
                    Stock = 92,
                    FrontCover = "images/products/front-cover/7f2e5b45-4b64-4f26-8a54-787facf48c69.jpg",
                    BackCover = "images/products/back-cover/7f2e5b45-4b64-4f26-8a54-787facf48c69.jpg"
                },
                new Product()
                {
                    Id = Guid.Parse("5cdfc1a7-ac0f-459c-b91a-788546e384dd"),
                    Author = "Kurt Taylor",
                    Publisher = "Red Panda Press (November 17, 2022)",
                    Title = "The Golfer's Excuse Handbook",
                    GenreId = Guid.Parse("e8350553-ebd1-4830-b0ee-81f85e1b22cc"),
                    Description = """
                        Do you have a friend or family member who loves golf?
                        Better yet, do you know the frustration and hilarity that comes with trying to play this beautiful sport?
                        Everyone who has experienced the game of golf understands that it can be frustrating, unfair, and embarrassing to play. Sometimes, if you are the sad individual who hit a poor shot, you might be tempted to provide an excuse to your playing partners. A good excuse can relieve the tension, and maybe even bring a few laughs.
                        Sometimes, though, an excuse can be hilarious just by its lack of credulity. A golfer can chalk their bad shot up to anything, absolutely anything, and that's when the excuse becomes a funny moment. Golfers share these stories with their friends, and it's not the shot they remember, only the hilarious excuse.
                        The Golfer's Excuse Handbook contains 250 excuses on a range of subjects all over the golf course, and a few that extend beyond the links!

                        Five different chapters, one for each group of excuses and the topics likely to draw a golfer's ire.
                        Each chapter features interesting golf history and trivia, as well!
                        There are also entertaining golf quotes from players, world leaders, and other well-known individuals within these pages.
                        Chapter subjects include the golf course itself, a golfer's playing partners, and the weather!
                        This book will entertain any fan of the game, from those who watch the pros to anyone who dares to swing a club. Perhaps this book can start a conversation with friends and family members about who has the worst excuses on the course, or who has said the most outrageous line in a moment of shame.

                        Maybe there's a golfer in your life that needs help with their excuse game! This book can also serve any golfer with a ton of ideas on how to deflect the embarrassment of that bad shot. And, if you have someone in your life who is particularly unskilled, they may need multiple excuses to make it through a round!
                        This book is bound to bring a smile to anyone who has experienced the struggle of golf. It will make readers smile, laugh out loud, and most importantly, appreciate the joy and pain that is the game of golf.
                        Grab a copy of The Golfer's Excuse Handbook and tee up the fun today!
                        """,
                    ISBN = "979-8887680132",
                    Price = 11.90,
                    Stock = 190,
                    FrontCover = "images/products/front-cover/5cdfc1a7-ac0f-459c-b91a-788546e384dd.jpg",
                    BackCover = "images/products/back-cover/5cdfc1a7-ac0f-459c-b91a-788546e384dd.jpg"
                },
            };
            modelBuilder.Entity<Product>().HasData(products);

            // Seeding 5 companies
            var companies = new List<Company>()
            {
                new Company ()
                {
                    Id = Guid.Parse("be0478d4-6e0a-4e9d-a177-b7d35de4dc49"),
                    Name = "Bosh Technology",
                    Address = "1005 Harron Drive, Baltimore City",
                    PhoneNumber = "4434579013",
                    Email = "technology@bosh.tech.com"
                },
                new Company ()
                {
                    Id = Guid.Parse("7963fc2d-b529-4bd0-9720-37160d69558e"),
                    Name = "KMS Solution",
                    Address = "116 Indiana Avenue, Makaha City",
                    PhoneNumber = "8086954120",
                    Email = "supplies@kms.solution.com"
                },
                new Company ()
                {
                    Id = Guid.Parse("f506bbdc-a84d-4a64-b894-d0155da7b9b7"),
                    Name = "Green Land",
                    Address = "1363 Benson Street, Niagara City",
                    PhoneNumber = "7152510523",
                    Email = "supplies@greenland.com"
                },
                new Company ()
                {
                    Id = Guid.Parse("c04b0234-4ab3-4e35-9924-a23630a3d912"),
                    Name = "Fast Delivery",
                    Address = "4650 Stonepot Road, Newark City",
                    PhoneNumber = "9083835330",
                    Email = "supplies@fast.del.com"
                },
                new Company ()
                {
                    Id = Guid.Parse("d798bf41-7b16-4588-aac0-2c8e3b405cdc"),
                    Name = "Book Master",
                    Address = "4832 Hedge Street, Piscataway City",
                    PhoneNumber = "9085995862",
                    Email = "supplies@book.mas.us"
                }
            };
            modelBuilder.Entity<Company>().HasData(companies);
        }
    }
}
