
#region types of api:
//1)Restful Api : support xml ,json ,...=>Get ,Post,Put ,Delete 
//2)Soap Api : Support Xml only 
//3)GraphQl Api  
#endregion

#region Types Of Arcturctures
/*1) 3Tears Arcturctures    => DAl ,PAl ,BLL
//2)Onion Layer Arcturcture => 
1)Domin layer {Core Layer}(one per Project :1) have Models {Tables} 
                                            2) Core Of Project [Interfaces] but not implemented
                          
2)Repository layer(can be one Or Many) every database have one Repsitory Layer:1)DBContext    
                                                                               2)Repository [part of bussiness]
                           
3)Service layer (can be one or many):1) have services such as :wallet ,basket,Token Service
                                     2)implement interfaces of Domain layer

4)Presentation Layer (can be one or more):Can be Api Or Mvc

 
 */

#endregion

#region Steps of inject from database
/*
1)in program file => ask ClR to creat an Object From DbContext of StoreDbContext
2)send this object in Constructor of StoreDbContext Then send with options of constructor chaning of DbContext
 
 
 */
#endregion

#region migrations & update database 
/*
 1) add-migrations -o(standardFor Output)Folder1/Migrations(folder Of Migrations)
 2)To Update-database(Apply new Migrations on database) Automatically when Running Project
            StoreDbContext db = new StoreDbContext();
            //InValid Cause there is no ParameterLess Constructor in StorDbContext 
            await db.Database.MigrateAsync();
solution => Ask CLR To Create Object From DbContext To inject it 

 */
#endregion

#region Path Notes
/*
1) (..)=> back to solution
2)\\ => using if the input of function is Path
3) / => if input of function is string cause it not has meaning in string
 */
#endregion

#region Specification Design Pattern
/*
==> Support Open For Extention(الريبوزاتري بتاعتي زي ما هي وقابله للزياده عليها بحاجات جديده) 
    Closed For Modification(مش بعدل علي الكود القديم عشان حاجه جديده )
==> Help for solve Problem of Lazzy loading

==>inClude navigational Prop Of any Generic Type 
==>build the Query With dynamic Form 
EX:
_dbContext.Product.Where(P=>P.id==id).include(P=>P.ProductBrand).InClude(P=>P.ProductType)
my Problem Have 3 Parts:
1)_dbContext.Product                                   => EntryPoint{StartOf Query}

2)Where(P=>P.id==id)                                   => Where Condition

3)include(P=>P.ProductBrand).InClude(P=>P.ProductType) => List Of InCludes

In Core Layer:
1)Interface => has signatuer Of Properties for each part of problem {one for Where conditions,another For list Of include,DBContext Not have Prop Cause Its A Fixed Per all Entities

2)Class => Implement the interface
-----------------------------------------------------------------------------------------------------------------------
1)Ispecification : interface that has a signiture of Properity Ex: InClude,Criteries(Where),OrderBy
2)BaseSpecification : Class That implement ISpecification & has Functions That Set Values in Prop
3)Product&Brand&TypeWithSpec : an Example Class that Used BaseSpecification Class 
4)Evaluator : a class that has Function that take Specs & return form of Query

Cycle of Request : Product&Brand&TypeWithSpec => BaseSpecification => Evaluator => Controller
 */

#endregion

#region Types Of Error
/*
1)Not Found =>if we want to get Product and not exist in DataBase 
2)Server Error => if we want to do processes at null value 
3)BadRequest()
4)BadReques{id}=> if we enter an string instead of int 
 */
#endregion

#region MiddleWare
/*
  app.UseDeveloperExceptionPage();=>this middle were used to send execption page details about the exception to Developer : (at .net 6 this middle were included to application by default but at .net 5 Dev must add it to can use it)
difference between 
1)app.UseStatusCodePagesWithReExecute("/errors/{0}") => will execute endPoint /errors/{0} and get the response
2)app.UseStatusCodePagesWithRedirects("/errors/{0}")=> will redirect to endPoint /errors/{0} first then execute the endPoint 

 
 */
#endregion

#region IEnumerable - IQueriable - ICollection - IReadOnlyList
/*
 1)IEnumerable :
Exist => name Space :System.Collections
Used  =>1) To Retrive Only Data & not need To do any filterations or any Operations like { Update - Delete - Add} on it
        2) if i iterate on the list of Records
2)IQueriable :
Exist => nameSpace :System.linq
Used  =>TO Retrive Data & do Filteration on it at database Fisterly

3)IReadOnlyList :
Exist => nameSpace : System.Collections.Generic
Used  =>To Retrive Only Data & not need To do any filterations or any Operations like { Update - Delete - Add} on it
        & not need To iterate on the List of Records
4)ICollection :
Exist => nameSpace :System.Collections
Used  => To Retrive Data which needed To do  Operations like { Update - Delete - Add} on it
 
 
 */
#endregion

#region Short Circut
/*
p=>(!BrandId.HasValue|| p.ProductBrandId==BrandId)//this mean that if BrandId is null it Return True so it not check at p.ProductBrandId==BrandId & if it not null so it will return false so it will search at another condition to get true 
 
 */
#endregion

#region Pagination
/*
Pagination : is a Technique to breake Large Recordes into small (sets of Recordes) Portions called Pages
- For Ex: there are 100 Product : so when we use pagination technique  we will divide it into 10 pages every page has 10 products so if we used Pagination Technique we Required:
1)Page size => Number Of Product which show in every page 
2)Page Index => Number Of page Ex : page 6 

Note: in Pagination Technique we Move in the same Page but with different View 
--------------------------------------------------------------------------------------------------------------------------
Pagination Devide into 2 wayes :
                    1)Back-end & Front-end(Connected Archecture) 
Advantages : Not load into Ram 
DisAdvantages : has more Requests at database To get more data

                      2) Front-end (DisConnected Archecture)
Advantages : Required one Request To get All data
DisAdvantages : It store all data in Ram Since Send First Request So it Load At Ram
*/
#endregion

#region Basket Module
/*
 Process Of Adding Data into Cart done inMemory dataBase This Data has Expire time in InMemory then Removed it 
InMemory Database : Exist In Ram(Local Storage) Or Server which store database
 Redis : it's a dictionary Contain [Key : value] not tables like Server,
 1)Key : the Id Of Cart (Guid) 
 2)Value : List<BasketItems> Items 
- Use Case:
1)Real Time Chat ,real Time Notification
2)Caching : Load Data in Ram 
3)Streaming : EX=>live in Facebook
-----------------------------------------------------------------------------------------------------------------------
#Functions :
1)Get Basket
2)Update Basket
3)Delete Basket
 */
#endregion

#region Security Module
/*
 inClude From 3 steps :
1)Idenitifcation : User Identified In The Application (Has Account In The Application)
2)Authentication :Who is this User in Sign In Step
3)Authorization : What are Permissions which User Has
-----------------------------------------------------------------------------------------------------------------
difference Between Using this with parameter & not Use it : 
public static  IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {}
Useing=> This IServiceCollection Considered it as Caller so when Call this Function In Program we Call with Class implement this Interface (Services) => builder.Services.AddIdentityServices();

public static  IServiceCollection AddIdentityServices( IServiceCollection Services)
        {}
 => Send IServiceCollection only in Function it Consider As Parameter & when Calling This Function we need an to Pass an Object From it =>  AddIdentityServices(Services);
 */
#endregion

#region Account Module
/*
 Register end-Point :
----------------------
Take RegisterDTO => UserName ,PhoneNumber ,Email ,Password
Return => Email ,UserName ,Token
---------------------------------------------------------------------------------------------------
Login end-Point : use SignIn Manager
Take LoginDTO => Email ,Password
Return UserDTO =>Email,Token,UserName
# We Can Controlle entered data in Register with 2 wayes =>
1)with data annotation 
2)with addIdentity(Options=>{}) in Program File 
*/
#endregion

#region Jwt
/*
 Token : encryption Strings 
1)Header : {Algorithm :HMAcsh1245 , Type:"jwt" }
2)Payload (Data ,Claims ,inputs):2 Parts of Payload =>
        2.1)Register Claims :Sub ,Audiens,Issuer ,iat => Predefiend (fixed for all Users)   
        2.2)Private Claims : name ,password,Phone number
 */
#endregion

#region Differenec between await & Result 
/*
 await => Stop Execute next Code until Execuite the line which Use it 
Result => Stop Execute next Code Until Execuite the line which use it and get the Result of Excuting  
 */
#endregion

#region Difference between inheritance & Aggregation
/*
1)Inheritance (IS a ): Part Time Employee Is a Employee
2)Aggregation (Has a):=>Room Has walls [Mandatory]               *Room Has Chairs[Optional]

 */
#endregion

#region Order Services
/*
1)Create Order [Buyer Email ,Basket Id ,Delivery Method Id, Shipping Address]
2)Get Oredr For Specific User[Buyer Email]
3)Get Oredr by id Specific User [Buyer Email,Order Id]
 
 */
#endregion

#region Difference between Services layer & Repository layer
/*
Services : For Creating Object such as Method Of Creating Order,Method For Creating Token
Repository : For Method Of DbSets Such as CRUD Operations 
*/
#endregion
