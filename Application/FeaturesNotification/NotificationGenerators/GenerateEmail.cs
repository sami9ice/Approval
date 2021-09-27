using Application.FeaturesNotification.Notifications;
using Application.FeaturesNotification.Utilities;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeaturesNotification.NotificationGenerators
{
    /// <summary>
    ///
    /// </summary>
    public class GenerateEmail
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMediator mediator;
        private readonly RoleManager<Group> roleManager;
        private readonly AssetURLS assetUrls;
        private readonly UserManager<User> userManager;
        private readonly IGeneratePassword getpassword;


        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateEmail"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="assetUrls">The asset urls.</param>
        /// <param name="userManager">The user manager.</param>
        public GenerateEmail(IApplicationDbContext applicationDbContext,
            IMediator mediator,
            RoleManager<Group> roleManager,
            IOptionsSnapshot<AssetURLS> assetUrls,
            UserManager<User> userManager,
            IGeneratePassword getpassword)
        {
            this.applicationDbContext = applicationDbContext;
            this.mediator = mediator;
            this.roleManager = roleManager;
            this.assetUrls = assetUrls.Value;
            this.userManager = userManager;
            this.getpassword = getpassword;
        }
        /// <summary>
        /// Gets the users to notify asynchronous.
        /// </summary>
        /// <param name="notificationReceivers">The notification receivers.</param>
        /// <param name="notification">The notification.</param>
        /// <returns></returns>
        public async Task<List<User>> GetUsersToNotifyAsync(NotificationReceiver notificationReceivers, NotificationMessage notification)
        {
            var users = new List<User>();
            try
            {
                if (notificationReceivers != null)
                {
                    var tempUserList = new List<User>();
                    if (!string.IsNullOrWhiteSpace(notificationReceivers.User) && notificationReceivers.User.Contains(@","))
                    {
                        var roles = notificationReceivers.User.Split(@",");
                        foreach (var role in roles)
                        {
                            //var usersInRole = (List<User>)await userManager.GetUsersInRoleAsync(role);
                            //new users from userlocation mapping
                            var userMappings = await applicationDbContext.UserGroups
                                .Where(u => u.group.ApprovalLevelId == notificationReceivers.group.ApprovalLevelId && u.group.Name.ToLower() == role.ToLower())
                                .Include(u => u.User)
                                .ToListAsync();
                            var usersInRole = userMappings.Select(u => u.User).ToList();
                            //end
                            if (usersInRole != null)
                            {
                                tempUserList.AddRange(usersInRole);
                            }
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(notificationReceivers.User) && !notificationReceivers.User.Contains(@","))
                    {
                        //var usersInRole = (List<User>)await userManager.GetUsersInRoleAsync(notificationReceivers.Roles);
                        //new users from userlocation mapping
                        var userMappings = await applicationDbContext.UserGroups
                            .Where(u => u.group.ApprovalLevelId == notificationReceivers.group.ApprovalLevelId && u.group.Name.ToLower() == notificationReceivers.User.ToLower())
                            .Include(u => u.User)
                            .ToListAsync();
                        var usersInRole = userMappings.Select(u => u.User).ToList();
                        //end
                        if (usersInRole != null)
                        {
                            tempUserList.AddRange(usersInRole);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(notificationReceivers.UserEmails) && notificationReceivers.UserEmails.Contains(@","))
                    {
                        var userEmails = notificationReceivers.UserEmails.Split(@",");
                        foreach (var userEmail in userEmails)
                        {
                            var user = await userManager.FindByEmailAsync(userEmail);
                            if (user != null)
                            {
                                tempUserList.Add(user);
                            }
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(notificationReceivers.UserEmails) && !notificationReceivers.UserEmails.Contains(@","))
                    {
                        var user = await userManager.FindByEmailAsync(notificationReceivers.UserEmails);
                        if (user != null)
                        {
                            tempUserList.Add(user);
                        }
                    }

                    users = tempUserList;
                }

                //if (notification.NotificationType == NotificationType.Approval)
                //{
                //    //If it is approval get the list of notifiers from the current approvers
                //    if (notification?.SubmitApprovalCommandResponse?.ExpectedCurrentAndNextWorkflowApprovers?.CurrentWorkflowApprovers != null)
                //    {
                //        var workflowApprovers = notification?.SubmitApprovalCommandResponse
                //            ?.ExpectedCurrentAndNextWorkflowApprovers?.CurrentWorkflowApprovers;

                //        if (workflowApprovers != null && workflowApprovers?.Approvers?.Count > 0)
                //        {
                //            var approvalUserTempList = new List<User>();
                //            var currentApprovalUser = new User();
                //            foreach (var approver in workflowApprovers?.Approvers)
                //            {
                //                currentApprovalUser = new User();
                //                currentApprovalUser.FullName = approver.FullName;
                //                currentApprovalUser.Email = approver.Email;
                //                currentApprovalUser.UserName = approver.UserName;
                //                currentApprovalUser.Id = approver.Id;

                //                approvalUserTempList.Add(currentApprovalUser);
                //            }

                //            users = approvalUserTempList;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
            }

            return users;
        }
        /// <summary>
        /// Generates the user details.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        public async Task<string> GenerateUserDetails(NotificationMessage notification, string template)
        {
            try
            {
                var user = notification.User;

                if (!string.IsNullOrWhiteSpace(template))
                {
                    if (template.Contains(@"{FullName}"))
                    {
                        template = template.Replace(@"{FullName}", user.Fullname);
                    }
                    if (template.Contains(@"{Email}"))
                    {
                        template = template.Replace(@"{Email}", user.Email);
                    }
                    if (template.Contains(@"{UserName}"))
                    {
                        template = template.Replace(@"{UserName}", user.UserName);
                    }
                    if (template.Contains(@"{PIN}"))
                    {
                        var base64EncodedBytes = System.Convert.FromBase64String(user.PIN);
                        var decodedData = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                        user.PIN = decodedData;

                        template = template.Replace(@"{PIN}", user.PIN);
                    }
                    if (template.Contains(@"{GroupName}"))
                    {
                        template = template.Replace(@"{GroupName}", user.Department);
                        //var roles = notification.NewUserRoles;
                        //var rolesString = "";
                        //if (roles != null && roles.Count > 0)
                        //{
                        //    foreach (var role in roles)
                        //    {
                        //        if (!string.IsNullOrWhiteSpace(role))
                        //        {
                        //            rolesString += role + ",";
                        //        }
                        //    }
                        //}
                       // template = template.Replace(@"{Roles}", rolesString);
                    }

                    //if (template.Contains(@"{UserFullName}"))
                    //{
                    //    template = template.Replace(@"{UserFullName}", user.Fullname);
                    //}
                    //if (template.Contains(@"{UserEmail}"))
                    //{
                    //    template = template.Replace(@"{UserEmail}", user.Email);
                    //}
                    //if (template.Contains(@"{Username}"))
                    //{
                    //    template = template.Replace(@"{Username}", user.UserName);
                    //}
                    //if (template.Contains(@"{Roles}"))
                    //{
                    //    var roles = notification.NewUserRoles;
                    //    var rolesString = "";
                    //    if (roles != null && roles.Count > 0)
                    //    {
                    //        foreach (var role in roles)
                    //        {
                    //            if (!string.IsNullOrWhiteSpace(role))
                    //            {
                    //                rolesString += role + ",";
                    //            }
                    //        }
                    //    }
                    //    template = template.Replace(@"{Roles}", rolesString);
                    //}
                }
            }
            catch (Exception ex)
            {
            }
            return template;
        }
        /// <summary>
        /// Generates the contract details.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        //public async Task<string> GenerateContractDetails(NotificationMessage notification, string template)
        //{
        //    try
        //    {
        //        var currentContract = notification.MainEntity as Contract;
        //        var contract = await applicationDbContext.Contracts
        //            .Where(a => a.Id == currentContract.Id)
        //            .Include(a => a.Location)
        //            .Include(a => a.Vendor)
        //            .FirstOrDefaultAsync();

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            if (template.Contains(@"{ContractName}"))
        //            {
        //                template = template.Replace(@"{ContractName}", contract.ContractName);
        //            }
        //            if (template.Contains(@"{ContractReferenceNumber}"))
        //            {
        //                template = template.Replace(@"{ContractReferenceNumber}", contract.ReferenceNumber);
        //            }
        //            if (template.Contains(@"{ContractVendorName}"))
        //            {
        //                template = template.Replace(@"{ContractVendorName}", contract.Vendor?.Name);
        //            }
        //            if (template.Contains(@"{ContractStartDate}"))
        //            {
        //                template = template.Replace(@"{ContractStartDate}", contract.StartDate.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //            if (template.Contains(@"{ContractEndDate}"))
        //            {
        //                template = template.Replace(@"{ContractEndDate}", contract.EndDate.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return template;
        //}
        /// <summary>
        /// Generates the employee details.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        //public async Task<string> GenerateEmployeeDetails(NotificationMessage notification, string template)
        //{
        //    try
        //    {
        //        var currentEmployee = notification.MainEntity as Employee;
        //        var employee = await applicationDbContext.Employees
        //            .Where(a => a.Id == currentEmployee.Id)
        //            .Include(a => a.Location)
        //            .Include(a => a.Department)
        //            .Include(a => a.Ministry)
        //            .Include(a => a.Unit)
        //            .FirstOrDefaultAsync();

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            if (template.Contains(@"{EmployeeFullName}"))
        //            {
        //                template = template.Replace(@"{EmployeeFullName}", $"{employee?.FirstName} {employee?.LastName}");
        //            }
        //            if (template.Contains(@"{EmployeeEmail}"))
        //            {
        //                template = template.Replace(@"{EmployeeEmail}", employee.Email);
        //            }
        //            if (template.Contains(@"{EmployeeMinistry}"))
        //            {
        //                template = template.Replace(@"{EmployeeMinistry}", employee?.Ministry?.Name);
        //            }
        //            if (template.Contains(@"{EmployeeDepartment}"))
        //            {
        //                template = template.Replace(@"{EmployeeDepartment}", employee?.Department?.Name);
        //            }
        //            if (template.Contains(@"{EmployeeUnit}"))
        //            {
        //                template = template.Replace(@"{EmployeeUnit}", employee?.Unit?.Name);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // ignored
        //    }

        //    return template;
        //}
        /// <summary>
        /// Generates the asset details.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        public async Task<string> GenerateAssetDetails(NotificationMessage notification, string template)
        {
            try
            {
                var currentAsset = notification.Data;
                var asset = await applicationDbContext.Data
                    .Where(a => a.Id == currentAsset.Id)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrWhiteSpace(template))
                {
                    if (template.Contains(@"{FormId}"))
                    {
                        template = template.Replace(@"{FormId}", asset.FormId);
                    }
                    if (template.Contains(@"{ProductName}"))
                    {
                        template = template.Replace(@"{ProductName}", asset.ProductName);
                    }
                    if (template.Contains(@"{Cost}"))
                    {
                        template = template.Replace(@"{Cost}", asset.Cost);
                        //"N",new CultureInfo("en-US")
                    }
                    if (template.Contains(@"{ICCID}") && asset.ICCID!=null)
                    {
                        template = template.Replace(@"{ICCID}", asset.ICCID);
                    }
                    else
                    {
                        template = template.Replace(@"{ICCID}", "To be provided by store");
                    }
                    if (template.Contains(@"{AcctManagergName}"))
                    {
                        template = template.Replace(@"{AcctManagergName}", asset.AcctManagergName);
                    }
                    if (template.Contains(@"{DateCreated}"))
                    {
                        template = template.Replace(@"{DateCreated}", asset.DateCreated.ToString("MMMM dd, yyyy HH:mm"));
                    }
                    if (template.Contains(@"{firstName}"))
                    {
                        template = template.Replace(@"{firstName}", asset.firstName);
                    }
                    if (template.Contains(@"{lastName}"))
                    {
                        template = template.Replace(@"{lastName}", asset.lastName);
                    }
                    if (template.Contains(@"{MDN}") && asset.MDN!=null)
                    {
                        template = template.Replace(@"{MDN}", asset.MDN.ToString());
                    }
                    else
                    {
                        template = template.Replace(@"{MDN}", "To be provided by store");
                    }
                    if (template.Contains(@"{EmialAddress}"))
                    {
                        template = template.Replace(@"{EmialAddress}", asset.EmialAddress);
                    }
                    //if (template.Contains(@"{AssetDescription}"))
                    //{
                    //    template = template.Replace(@"{AssetDescription}", asset.Description);
                    //}
                    if (template.Contains(@"{CustomerId}") && asset.CustomerId!=null)
                    {
                        template = template.Replace(@"{CustomerId}", asset.CustomerId);
                    }
                    else
                    {
                        template = template.Replace(@"{CustomerId}", "To be provided by Finance Billing");
                    }

                }
            }
            catch (Exception ex)
            {
                // ignored
            }

            return template;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="notification"></param>
        ///// <param name="template"></param>
        ///// <returns></returns>
        //public async Task<string> GenerateAssetCheckOutDetails(NotificationMessage notification, string template)
        //{
        //    try
        //    {
        //        var currentAsset = notification.MainEntity as Asset;
        //        var currentAssetCheckOut = notification.SubEntity as AssetCheckOut;
        //        var assetCheckOut = currentAssetCheckOut;
        //        var LocationFrom = await applicationDbContext.Locations.Where(l => l.Id == currentAssetCheckOut.LocationFrom)
        //            .FirstOrDefaultAsync();
        //        var LocationTo = await applicationDbContext.Locations.Where(l => l.Id == currentAssetCheckOut.LocationTo)
        //            .FirstOrDefaultAsync();
        //        var checkedOutToEmployee =
        //            await applicationDbContext.Employees.Where(e => e.Id == currentAssetCheckOut.AssignedUserId)
        //                .FirstOrDefaultAsync();
        //        var CheckedOutToUser = $"{checkedOutToEmployee?.FirstName} {checkedOutToEmployee?.LastName}";
        //        //var assetCheckOut = await (from checkout in applicationDbContext.AssetCheckOuts
        //        //    join asset in applicationDbContext.Assets on checkout.AssetId equals asset.Id
        //        //    join user in applicationDbContext.Employees on checkout.AssignedUserId equals user.Id
        //        //    join locationFrom in applicationDbContext.Locations on checkout.LocationFrom equals locationFrom.Id
        //        //    join locationTo in applicationDbContext.Locations on checkout.LocationTo equals locationTo.Id
        //        //    where checkout.Id == currentAssetCheckOut.Id
        //        //    select new
        //        //    {
        //        //        CheckoutType = checkout.CheckoutType,
        //        //        ReturnDate = checkout.ReturnDate,
        //        //        LocationFrom = locationFrom,
        //        //        CheckedOutToUser = $"{user.FirstName} {user.LastName} (${user.StaffId})",
        //        //        LocationTo = locationTo,
        //        //        AssignmentType = checkout.CheckoutTo,
        //        //        Description = checkout.Comment

        //        //    }).FirstOrDefaultAsync();

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            if (template.Contains(@"{CheckoutType}"))
        //            {
        //                template = template.Replace(@"{CheckoutType}", assetCheckOut?.CheckoutType.ToString());
        //            }
        //            if (template.Contains(@"{CheckoutReturnDate}"))
        //            {
        //                template = template.Replace(@"{CheckoutReturnDate}", assetCheckOut?.ReturnDate?.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //            if (template.Contains(@"{CheckoutFromLocation}"))
        //            {
        //                template = template.Replace(@"{CheckoutFromLocation}", LocationFrom.Name);
        //            }
        //            if (template.Contains(@"{CheckoutTo}"))
        //            {
        //                template = template.Replace(@"{CheckoutTo}", CheckedOutToUser);
        //            }
        //            if (template.Contains(@"{CheckoutToLocation}"))
        //            {
        //                template = template.Replace(@"{CheckoutToLocation}", LocationTo.Name);
        //            }
        //            if (template.Contains(@"{AssignmentType}"))
        //            {
        //                template = template.Replace(@"{AssignmentType}", assetCheckOut?.CheckoutTo.ToString());
        //            }
        //            if (template.Contains(@"{CheckoutDescription}"))
        //            {
        //                template = template.Replace(@"{CheckoutDescription}", assetCheckOut?.Comment);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // ignored
        //    }

        //    return template;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="notification"></param>
        ///// <param name="template"></param>
        ///// <returns></returns>
        //public async Task<string> GenerateAssetReservationDetails(NotificationMessage notification, string template)
        //{
        //    try
        //    {
        //        var currentAsset = notification.MainEntity as Asset;
        //        var currentAssetReservation = notification.SubEntity as ReservedAsset;

        //        var reservationLocation = await applicationDbContext.Locations.Where(l => l.Id == currentAssetReservation.LocationId)
        //            .FirstOrDefaultAsync();
        //        var reservedToEmployee =
        //            await applicationDbContext.Employees.Where(e => e.Id == currentAssetReservation.ReserveFor)
        //                .FirstOrDefaultAsync();
        //        var reservedToUser = $"{reservedToEmployee?.FirstName} {reservedToEmployee?.LastName}";

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            if (template.Contains(@"{ReservedFor}"))
        //            {
        //                template = template.Replace(@"{ReservedFor}", reservedToUser);
        //            }
        //            if (template.Contains(@"{ReservationStartDate}"))
        //            {
        //                template = template.Replace(@"{ReservationStartDate}", currentAssetReservation?.StartDate.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //            if (template.Contains(@"{ReservationEndDate}"))
        //            {
        //                template = template.Replace(@"{ReservationEndDate}", currentAssetReservation?.EndDate.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //            if (template.Contains(@"{ReservationLocation}"))
        //            {
        //                template = template.Replace(@"{ReservationLocation}", reservationLocation.Name);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return template;
        //}

        //public async Task<string> GenerateAssetCheckInDetails(NotificationMessage notification, string template)
        //{
        //    try
        //    {
        //        var currentAsset = notification.MainEntity as Asset;
        //        var currentAssetCheckIn = notification.SubEntity as AssetCheckIn;

        //        var checkInLocation = await applicationDbContext.Locations.Where(l => l.Id == currentAssetCheckIn.LocationId)
        //            .FirstOrDefaultAsync();

        //        var existingCheckOut = await applicationDbContext.AssetCheckOuts
        //            .Where(c => c.AssetId == currentAssetCheckIn.AssetId)
        //            .OrderByDescending(c => c.DateCreated)
        //            .FirstOrDefaultAsync();

        //        var checkedOutToEmployee =
        //            await applicationDbContext.Employees.Where(e => e.Id == existingCheckOut.AssignedUserId)
        //                .FirstOrDefaultAsync();
        //        var checkOutToUser = $"{checkedOutToEmployee?.FirstName} {checkedOutToEmployee?.LastName}";

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            if (template.Contains(@"{CheckedOutTo}"))
        //            {
        //                template = template.Replace(@"{CheckedOutTo}", checkOutToUser);
        //            }
        //            if (template.Contains(@"{CheckOutStartDate}"))
        //            {
        //                template = template.Replace(@"{CheckOutStartDate}", existingCheckOut?.CheckOutAt.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //            if (template.Contains(@"{CheckOutEndDate}"))
        //            {
        //                template = template.Replace(@"{CheckOutEndDate}", existingCheckOut?.ReturnDate?.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //            if (template.Contains(@"{CheckInLocation}"))
        //            {
        //                template = template.Replace(@"{CheckInLocation}", checkInLocation.Name);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return template;
        //}
        /// <summary>
        /// Generates the asset service details.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        //public async Task<string> GenerateAssetServiceDetails(NotificationMessage notification, string template)
        //{
        //    try
        //    {
        //        var currentAsset = notification.MainEntity as Asset;
        //        var currentAssetService = notification.SubEntity as ServiceAsset;

        //        var assetService = await applicationDbContext.ServiceAssets
        //            .Where(s => s.Id == currentAssetService.Id)
        //            .Include(s => s.ServiceType)
        //            .FirstOrDefaultAsync();

        //        currentAssetService = assetService;
        //        var serviceLocation = await applicationDbContext.Locations.Where(l => l.Id == currentAssetService.LocationId)
        //            .FirstOrDefaultAsync();
        //        var vendor = await applicationDbContext.Vendors.Where(v => v.Id == currentAssetService.Vendor)
        //            .FirstOrDefaultAsync();

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            if (template.Contains(@"{ServiceVendor}"))
        //            {
        //                template = template.Replace(@"{ServiceVendor}", vendor.Name);
        //            }
        //            if (template.Contains(@"{TypeOfService}"))
        //            {
        //                template = template.Replace(@"{TypeOfService}", currentAssetService?.ServiceType.ServiceTypeName);
        //            }
        //            if (template.Contains(@"{ServiceStartDate}"))
        //            {
        //                template = template.Replace(@"{ServiceStartDate}", currentAssetService?.StartDate.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //            if (template.Contains(@"{ServiceEndDate}"))
        //            {
        //                template = template.Replace(@"{ServiceEndDate}", currentAssetService?.EndDate.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return template;
        //}
        ///// <summary>
        ///// Generates the initiator details.
        ///// </summary>
        ///// <param name="initiator">The initiator.</param>
        ///// <param name="location">The location.</param>
        ///// <param name="initiatedDate">The initiated date.</param>
        ///// <param name="template">The template.</param>
        ///// <returns></returns>
        public async Task<string> GenerateInitiatorDetails(User initiator, DateTimeOffset initiatedDate, string template)
        {
            try
            {
               var Creator= userManager.FindByNameAsync(initiator.CreatedBy).Result;
                var RoleNames = userManager.GetRolesAsync(Creator).Result.FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(template))
                {
                    if (template.Contains(@"{ApprovedBy}") && initiator?.CreatedBy!=null)
                    {
                        template = template.Replace(@"{ApprovedBy}", initiator?.CreatedBy);
                    }
                    else
                    {
                        template = template.Replace(@"{ApprovedBy}", "Admin");
                    }
                    if (template.Contains(@"{UserName}"))
                    {
                        template = template.Replace(@"{ApprovedBy}", initiator?.UserName);
                    }

                    if (template.Contains(@"{DateCreated}"))
                    {
                        template = template.Replace(@"{DateCreated}", initiatedDate.ToString("MMMM dd, yyyy HH:mm"));
                    }
                    if (template.Contains(@"{ApprovalGroupName}") && RoleNames!=null)
                    {
                        template = template.Replace(@"{ApprovalGroupName}", RoleNames);
                    }
                    else
                    {
                        template = template.Replace(@"{ApprovalGroupName}", "No Group Assigned Yet");
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return template;
        }
        public async Task<string> GenerateInitiatorDetailsForApproval(User initiator, DateTimeOffset initiatedDate, string template)
        {
            try
            {
                var Creator = userManager.FindByNameAsync(initiator.UserName).Result;
                var RoleNames = userManager.GetRolesAsync(Creator).Result.FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(template))
                {
                    if (template.Contains(@"{ApprovedBy}"))
                    {
                        template = template.Replace(@"{ApprovedBy}", initiator?.Fullname);
                    }

                    if (template.Contains(@"{DateCreated}"))
                    {
                        template = template.Replace(@"{DateCreated}", initiatedDate.ToString("MMMM dd, yyyy HH:mm"));
                    }
                    if (template.Contains(@"{ApprovalGroupName}"))
                    {
                        template = template.Replace(@"{ApprovalGroupName}", RoleNames +", Phone Number: "+initiator.PhoneNo);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return template;
        }
        /// <summary>
        /// Generates the asset disbursement details.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        //public async Task<string> GenerateAssetDisbursementDetails(NotificationMessage notification, string template)
        //{
        //    try
        //    {
        //        var disbursement = notification.DisbursementEntity as Disbursement;

        //        var disbursementLocation = await applicationDbContext.Locations.Where(l => l.Id == disbursement.LocationId)
        //            .FirstOrDefaultAsync();

        //        var disbursementToEmployee =
        //            await applicationDbContext.Employees.Where(e => e.Id == disbursement.EmployeeId)
        //                .FirstOrDefaultAsync();
        //        var disbursementToUser = $"{disbursementToEmployee?.FirstName} {disbursementToEmployee?.LastName}";

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            if (template.Contains(@"{DisbursedTo}"))
        //            {
        //                template = template.Replace(@"{DisbursedTo}", disbursementToUser);
        //            }
        //            if (template.Contains(@"{DisbursementDate}"))
        //            {
        //                template = template.Replace(@"{DisbursementDate}", disbursement?.DateCreated.ToString("MMMM dd, yyyy HH:mm"));
        //            }
        //            if (template.Contains(@"{DisbursementLocation}"))
        //            {
        //                template = template.Replace(@"{DisbursementLocation}", disbursementLocation.Name);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return template;
        //}
        ///// <summary>
        ///// Generates the view link.
        ///// </summary>
        ///// <param name="notification">The notification.</param>
        ///// <param name="template">The template.</param>
        ///// <param name="link">The link.</param>
        ///// <param name="linkText">The link text.</param>
        ///// <returns></returns>
        public async Task<string> GenerateViewLink(NotificationMessage notification, string template, string link, string linkText)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(template))
                {
                    if (template.Contains(@"{ItemURL}"))
                    {
                        if (!string.IsNullOrWhiteSpace(link))
                        {
                            if (link.Contains(@"{UserId}"))
                            {
                                var asset = notification.MainEntity;
                                link = link.Replace(@"{Id}", asset?.Id);
                            }
                            //if (link.Contains(@"{contractId}"))
                            //{
                            //    var contract = notification.MainEntity as Contract;
                            //    link = link.Replace(@"{contractId}", contract?.Id);
                            //}
                        }
                        template = template.Replace(@"{ItemURL}", link);
                    }
                  var EmailLogoURL=  applicationDbContext.Configuration.Where(a => a.Name == "EmailLogoURL").FirstOrDefault();
                    if (template.Contains(@"{EmailLogoURL}"))
                    {
                       // this.assetUrls.EmailLogoURL = "https://selfcare.cyberspace.net.ng/Cyberspace4G.LTE.jpg"
                        template = template.Replace(@"{EmailLogoURL}", this.assetUrls.EmailLogoURL = EmailLogoURL.Value);
                    }

                    //if (template.Contains(@"{LinkText}"))
                    //{
                    //    template = template.Replace(@"{LinkText}", linkText);
                    //}
                }
            }
            catch (Exception ex)
            {
            }
            return template;
        }

        ///// <summary>
        ///// Generates the recipient details and save.
        ///// </summary>
        ///// <param name="notification">The notification.</param>
        ///// <param name="template">The template.</param>
        ///// <returns></returns>
        public async Task<bool> GenerateRecipientDetailsAndSave(NotificationMessage notification, string template)
        {
            bool saved = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(template))
                {
                    var tempTemplate = template;
                    List<NotificationMessages> notificationMessagesList = new List<NotificationMessages>();

                    //var notificationReceivers = await applicationDbContext.NotificationReceivers.Where(n =>
                    //        n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
                    //    .FirstOrDefaultAsync();

                   // var users =await userManager.FindByIdAsync( notification.Data.UserId);

                    foreach (var user in notification.UserEmailAdd)
                    {
                        template = tempTemplate;

                        NotificationMessages notificationMessages = new NotificationMessages();

                        if (template.Contains(@"{RecipientFullName}"))
                        {
                            template = template.Replace(@"{RecipientFullName}", notification.Data.CreatedBy);
                        }
                        if (template.Contains(@"{UserName}"))
                        {
                            template = template.Replace(@"{UserName}",user.Split('@')[0].ToString());
                        }

                        notificationMessages.NotificationActionType = notification.NotificationActionType;
                        notificationMessages.Body = template;
                        notificationMessages.To = user;
                        notificationMessagesList.Add(notificationMessages);
                        getpassword.SendMailForCreateUser(notification.Data.CreatedBy, notificationMessages.To, notificationMessages.Body);

                    }

                    if (notificationMessagesList != null && notificationMessagesList.Count > 0)
                    {
                        applicationDbContext.NotificationMessages.AddRange(notificationMessagesList);
                        int saveStatus = await applicationDbContext.SaveChangesAsync();
                        saved = saveStatus > 0 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return saved;
        }

        ///// <summary>
        ///// Generates the initiator details and save.
        ///// </summary>
        ///// <param name="notification">The notification.</param>
        ///// <param name="template">The template.</param>
        ///// <param name="initiator">The initiator.</param>
        ///// <returns></returns>
        //public async Task<bool> GenerateInitiatorDetailsAndSave(NotificationMessage notification, string template, User initiator)
        //{
        //    bool saved = false;
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            var tempTemplate = template;
        //            List<NotificationMessages> notificationMessagesList = new List<NotificationMessages>();

        //            template = tempTemplate;

        //            NotificationMessages notificationMessages = new NotificationMessages();

        //            if (template.Contains(@"{RecipientFullName}"))
        //            {
        //                template = template.Replace(@"{RecipientFullName}", initiator.FullName);
        //            }
        //            if (notification.NoMoreApproval && notification.NotificationType == NotificationType.Approval)
        //            {
        //                if (notification.NotificationActionType == NotificationActionType.AssetCheckOut)
        //                {
        //                    string stringToReplace = "Please be informed that the following activity has happened on asset management solution for the state:";
        //                    string replacementString = "Please be informed that approval is now complete and you can now proceed to the store to retrieve your requested item.";
        //                    template = template.Replace(stringToReplace, replacementString);
        //                }
        //                if (notification.NotificationActionType == NotificationActionType.AssetCheckIn)
        //                {
        //                    string stringToReplace = "Please be informed that the following activity has happened on asset management solution for the state:";
        //                    string replacementString = "Please be informed that approval is now complete and you can now proceed to the store to return the specified item.";
        //                    template = template.Replace(stringToReplace, replacementString);
        //                }

        //                notificationMessages.NotificationActionType = notification.NotificationActionType;
        //                notificationMessages.Body = template;
        //                notificationMessages.To = initiator.Email;
        //                notificationMessagesList.Add(notificationMessages);

        //                if (notificationMessagesList != null && notificationMessagesList.Count > 0)
        //                {
        //                    applicationDbContext.NotificationMessages.AddRange(notificationMessagesList);
        //                    int saveStatus = await applicationDbContext.SaveChangesAsync();
        //                    saved = saveStatus > 0 ? true : false;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return saved;
        //}
        ///// <summary>
        ///// Generates the concerned recipient details and save.
        ///// </summary>
        ///// <param name="notification">The notification.</param>
        ///// <param name="template">The template.</param>
        ///// <param name="users">The users.</param>
        ///// <returns></returns>
        public async Task<bool> GenerateConcernedRecipientDetailsAndSave(NotificationMessage notification, string template, List<User> users)
        {
            bool saved = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(template))
                {
                    var tempTemplate = template;
                    List<NotificationMessages> notificationMessagesList = new List<NotificationMessages>();

                    foreach (var user in users)
                    {
                        template = tempTemplate;

                        NotificationMessages notificationMessages = new NotificationMessages();

                        if (template.Contains(@"{RecipientFullName}"))
                        {
                            template = template.Replace(@"{RecipientFullName}", user.UserName);
                        }
                        if (template.Contains(@"{UserName}"))
                        {
                            template = template.Replace(@"{UserName}", user.UserName);
                        }
                        notificationMessages.NotificationActionType = notification.NotificationActionType;
                        notificationMessages.Body = template;
                        notificationMessages.To = user.Email;
                        notificationMessagesList.Add(notificationMessages);

                       var Status= getpassword.SendMailForCreateUser(user.CreatedBy, notificationMessages.To, notificationMessages.Body);
                        saved = Status.Result;
                    }

                    if (notificationMessagesList != null && notificationMessagesList.Count > 0)
                    {
                        applicationDbContext.NotificationMessages.AddRange(notificationMessagesList);
                        int saveStatus = await applicationDbContext.SaveChangesAsync();
                       // saved = saveStatus > 0 ? true : false;
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return saved;
        }

        //public async Task<bool> EmployeeCreated(NotificationMessage notification)
        //{
        //    bool generated = false;

        //    try
        //    {
        //        var employee = notification.MainEntity as Employee;

        //        var initiator = await userManager.FindByNameAsync(employee?.CreatedBy);

        //        var initiatorLocation = await applicationDbContext.Locations
        //            .Where(l => l.Id == employee.LocationId).FirstOrDefaultAsync();

        //        var initiatedDate = employee.DateCreated;

        //        var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
        //            .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
        //            .FirstOrDefaultAsync();

        //        var template = notificationMessageTemplate?.MessageTemplate;

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            var itemActionName = "New Employee";

        //            if (notification.NotificationType == NotificationType.Approval)
        //            {
        //                itemActionName += " Approval";
        //            }

        //            if (template.Contains(@"{ItemActionName}"))
        //            {
        //                template = template.Replace(@"{ItemActionName}", itemActionName);
        //            }
        //        }

        //        template = await GenerateEmployeeDetails(notification, template);
        //        template = await GenerateInitiatorDetails(initiator, initiatorLocation, initiatedDate, template);
        //        var linkText = "";

        //        var link = "";
        //        if (notification.NotificationType == NotificationType.Approval)
        //        {
        //            link = assetUrls.ViewEmployeeApproval;
        //        }
        //        else
        //        {
        //            link = assetUrls.ViewEmployee;
        //        }
        //        template = await GenerateViewLink(notification, template, link, linkText);

        //        generated = await GenerateRecipientDetailsAndSave(notification, template);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        /// <summary>
        /// Users the created.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns></returns>
        public async Task<bool> UserCreated(NotificationMessage notification)
        {
            bool generated = false;
            var Pin = notification.User.PIN;


            try
            {
                var user = notification.User;
                //var Pin = user.PIN;
                var initiator = await userManager.FindByNameAsync(user?.Fullname);
                //var userLocationMapping = await applicationDbContext.
                //    .Where(u => u.UserId == user.Id)
                //    .Include(u => u.group.GroupName).FirstOrDefaultAsync();

              //  var initiatorLocation = userLocationMapping?.GroupdId;

                var initiatedDate = user.DateCreated;

                var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
                    .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
                    .FirstOrDefaultAsync();

                var template = notificationMessageTemplate?.MessageTemplate;

                if (!string.IsNullOrWhiteSpace(template))
                {
                    var itemActionName = "New User";

                    if (notification.NotificationType == NotificationType.Approval)
                    {
                        itemActionName += " Approval";
                    }

                    if (template.Contains(@"{ItemActionName}"))
                    {
                        template = template.Replace(@"{ItemActionName}", itemActionName);
                    }
                }

                template = await GenerateUserDetails(notification, template);
                template = await GenerateInitiatorDetails(initiator, /*initiatorLocation*/ initiatedDate, template);
                var linkText = "";
                //get link from Db
                var Config = await applicationDbContext.Configuration
                    .Where(n => n.Name == "Link" && n.IsDeleted == false)
                    .FirstOrDefaultAsync();
                var link = Config.Value;
                //var link = "https://localhost:44305/api/v1/UserAccount/ResetPassword";

                //if (notification.NotificationType == NotificationType.Approval)
                //{
                //    link = assetUrls.ViewUserApproval;
                //}
                //else
                //{
                //    link = assetUrls.ViewUser;
                //}
                template = await GenerateViewLink(notification, template, link, linkText);

                generated = await GenerateRecipientDetailsAndSave(notification, template);

                if (!string.IsNullOrWhiteSpace(template))
                {
                    var password = notification?.NewUserPassword;
                    if (template.Contains(@"{Password}"))
                    {
                        template = template.Replace(@"{Password}", password);
                    }
                    var htmlToReplace = "display: none;";
                    var htmlToReplaceWith = "";
                    template = template.Replace(htmlToReplace, htmlToReplaceWith);

                    htmlToReplace = "Details of the user";
                    htmlToReplaceWith = "Your details";
                    template = template.Replace(htmlToReplace, htmlToReplaceWith);
                    //notify newly created user
                    var users = new List<User>();
                    user.PIN = Pin;
                    users.Add(user);
                    bool generatedForUser = await GenerateConcernedRecipientDetailsAndSave(notification, template, users);
                }
            }
            catch (Exception ex)
            {
            }

            return generated;
        }
        public async Task<bool> ResetPassword(NotificationMessage notification)
        {
            bool generated = false;
            var Pin = notification.User.PIN;


            try
            {
                var user = notification.User;
                //var Pin = user.PIN;
                var initiator = await userManager.FindByNameAsync(user?.Fullname);
                //var userLocationMapping = await applicationDbContext.
                //    .Where(u => u.UserId == user.Id)
                //    .Include(u => u.group.GroupName).FirstOrDefaultAsync();

                //  var initiatorLocation = userLocationMapping?.GroupdId;

                var initiatedDate = user.DateCreated;

                var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
                    .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
                    .FirstOrDefaultAsync();

                var template = notificationMessageTemplate?.MessageTemplate;

                if (!string.IsNullOrWhiteSpace(template))
                {
                    var itemActionName = "New User";

                    if (notification.NotificationType == NotificationType.ResetPassword)
                    {
                        itemActionName += " Password Reset";
                    }

                    if (template.Contains(@"{ItemActionName}"))
                    {
                        template = template.Replace(@"{ItemActionName}", itemActionName);
                    }
                }

                template = await GenerateUserDetails(notification, template);
                template = await GenerateInitiatorDetails(initiator, /*initiatorLocation*/ initiatedDate, template);
                var linkText = "";
                //get link from Db
                var Config = await applicationDbContext.Configuration
                    .Where(n => n.Name == "Link" && n.IsDeleted == false)
                    .FirstOrDefaultAsync();
                var link = Config.Value;
                //var link = "https://localhost:44305/api/v1/UserAccount/ResetPassword";

                //if (notification.NotificationType == NotificationType.Approval)
                //{
                //    link = assetUrls.ViewUserApproval;
                //}
                //else
                //{
                //    link = assetUrls.ViewUser;
                //}
                template = await GenerateViewLink(notification, template, link, linkText);

                generated = await GenerateRecipientDetailsAndSave(notification, template);

                if (!string.IsNullOrWhiteSpace(template))
                {
                    var password = notification?.NewUserPassword;
                    if (template.Contains(@"{Password}"))
                    {
                        template = template.Replace(@"{Password}", password);
                    }
                    var htmlToReplace = "display: none;";
                    var htmlToReplaceWith = "";
                    template = template.Replace(htmlToReplace, htmlToReplaceWith);

                    htmlToReplace = "Details of the user";
                    htmlToReplaceWith = "Your details";
                    template = template.Replace(htmlToReplace, htmlToReplaceWith);
                    //notify newly created user
                    var users = new List<User>();
                    user.PIN = Pin;
                    users.Add(user);
                    bool generatedForUser = await GenerateConcernedRecipientDetailsAndSave(notification, template, users);
                    generated = generatedForUser;
                }
            }
            catch (Exception ex)
            {
            }

            return generated;
        }


        //public async Task<bool> ContractCreated(NotificationMessage notification)
        //{
        //    bool generated = false;

        //    try
        //    {
        //        var contract = notification.MainEntity as Contract;

        //        var initiator = await userManager.FindByNameAsync(contract?.CreatedBy);

        //        var initiatorLocation = await applicationDbContext.Locations
        //            .Where(l => l.Id == contract.LocationId).FirstOrDefaultAsync();

        //        var initiatedDate = contract.DateCreated;

        //        var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
        //            .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
        //            .FirstOrDefaultAsync();

        //        var template = notificationMessageTemplate?.MessageTemplate;

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            var itemActionName = "New Contract";

        //            if (notification.NotificationType == NotificationType.Approval)
        //            {
        //                itemActionName += " Approval";
        //            }

        //            if (template.Contains(@"{ItemActionName}"))
        //            {
        //                template = template.Replace(@"{ItemActionName}", itemActionName);
        //            }
        //        }

        //        template = await GenerateContractDetails(notification, template);
        //        template = await GenerateInitiatorDetails(initiator, initiatorLocation, initiatedDate, template);
        //        var linkText = "";

        //        var link = "";
        //        if (notification.NotificationType == NotificationType.Approval)
        //        {
        //            link = assetUrls.ViewContractApproval;
        //        }
        //        else
        //        {
        //            link = assetUrls.ViewContract;
        //        }
        //        template = await GenerateViewLink(notification, template, link, linkText);

        //        generated = await GenerateRecipientDetailsAndSave(notification, template);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        public async Task<bool> AssetCreated(NotificationMessage notification)
        {
            bool generated = false;

            try
            {
                var asset = notification.Data ;

                var initiator = await userManager.FindByNameAsync(asset?.CreatedBy);

                //var initiatorLocation = await roleManager.Roles
                //    .Where(l => l.Id == initiator.GroupId).FirstOrDefaultAsync();

                var initiatedDate = asset.DateCreated;

                var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
                    .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
                    .FirstOrDefaultAsync();

                var template = notificationMessageTemplate?.MessageTemplate;

                if (!string.IsNullOrWhiteSpace(template))
                {
                    var itemActionName = "New Customer";

                    if (notification.NotificationType == NotificationType.Approval)
                    {
                        itemActionName += " Approval";
                    }

                    if (template.Contains(@"{ItemActionName}"))
                    {
                        template = template.Replace(@"{ItemActionName}", itemActionName);
                    }
                }

                template = await GenerateAssetDetails(notification, template);
                template = await GenerateInitiatorDetailsForApproval(initiator/*, initiatorLocation*/, initiatedDate, template);
                var linkText = "";
                var ViewApprovalLink = applicationDbContext.Configuration.Where(a => a.Name == "ViewApprovalLink").Select(a => a.Value).FirstOrDefault();
                var link = "";
                if (notification.NotificationType == NotificationType.Approval)
                {
                    link = ViewApprovalLink;
                    if (link.Contains(@"{id}"))
                    {
                        link = link.Replace(@"{id}", asset?.FormId);
                    }
                }
                else
                {
                    link = ViewApprovalLink;
                    if (link.Contains(@"{id}"))
                    {
                        link = link.Replace(@"{id}", asset?.FormId);
                    }
                }
                template = await GenerateViewLink(notification, template, link, linkText="Tst");

                generated = await GenerateRecipientDetailsAndSave(notification, template);
            }
            catch (Exception ex)
            {
            }

            return generated;
        }

        //public async Task<bool> AssetCheckOut(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var asset = notification.MainEntity as Asset;

        //        var assetCheckout = notification.SubEntity as AssetCheckOut;

        //        var initiator = await userManager.FindByNameAsync(assetCheckout?.CreatedBy);

        //        var initiatorLocation = await applicationDbContext.Locations
        //            .Where(l => l.Id == asset.LocationId).FirstOrDefaultAsync();

        //        var initiatedDate = asset.DateCreated;

        //        var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
        //            .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
        //            .FirstOrDefaultAsync();

        //        var template = notificationMessageTemplate?.MessageTemplate;

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            var itemActionName = "Asset Checkout";

        //            if (notification.NotificationType == NotificationType.Approval)
        //            {
        //                itemActionName += " Approval";
        //            }

        //            if (template.Contains(@"{ItemActionName}"))
        //            {
        //                template = template.Replace(@"{ItemActionName}", itemActionName);
        //            }
        //        }

        //        template = await GenerateAssetDetails(notification, template);

        //        template = await GenerateAssetCheckOutDetails(notification, template);

        //        template = await GenerateInitiatorDetails(initiator, initiatorLocation, initiatedDate, template);

        //        var linkText = "";

        //        var link = "";
        //        if (notification.NotificationType == NotificationType.Approval)
        //        {
        //            link = assetUrls.ViewAssetCheckOutApproval;
        //        }
        //        else
        //        {
        //            link = assetUrls.ViewAssetCheckOut;
        //        }
        //        template = await GenerateViewLink(notification, template, link, linkText);

        //        generated = await GenerateRecipientDetailsAndSave(notification, template);
        //        bool initiatorDetailsGenerated = await GenerateInitiatorDetailsAndSave(notification, template, initiator);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //public async Task<bool> AssetReservation(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var asset = notification.MainEntity as Asset;
        //        var assetReservation = notification.SubEntity as ReservedAsset;

        //        var initiator = await userManager.FindByNameAsync(assetReservation?.CreatedBy);

        //        var initiatorLocation = await applicationDbContext.Locations
        //            .Where(l => l.Id == asset.LocationId).FirstOrDefaultAsync();

        //        var initiatedDate = asset.DateCreated;

        //        var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
        //            .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
        //            .FirstOrDefaultAsync();

        //        var template = notificationMessageTemplate?.MessageTemplate;

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            var itemActionName = "Asset Reservation";

        //            if (notification.NotificationType == NotificationType.Approval)
        //            {
        //                itemActionName += " Approval";
        //            }

        //            if (template.Contains(@"{ItemActionName}"))
        //            {
        //                template = template.Replace(@"{ItemActionName}", itemActionName);
        //            }
        //        }

        //        template = await GenerateAssetDetails(notification, template);

        //        template = await GenerateAssetReservationDetails(notification, template);

        //        template = await GenerateInitiatorDetails(initiator, initiatorLocation, initiatedDate, template);

        //        var linkText = "";

        //        var link = "";
        //        if (notification.NotificationType == NotificationType.Approval)
        //        {
        //            link = assetUrls.ViewAssetReservationApproval;
        //        }
        //        else
        //        {
        //            link = assetUrls.ViewAssetReservation;
        //        }
        //        template = await GenerateViewLink(notification, template, link, linkText);

        //        generated = await GenerateRecipientDetailsAndSave(notification, template);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //public async Task<bool> AssetService(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var asset = notification.MainEntity as Asset;
        //        var assetService = notification.SubEntity as ServiceAsset;

        //        var initiator = await userManager.FindByNameAsync(assetService?.CreatedBy);

        //        var initiatorLocation = await applicationDbContext.Locations
        //            .Where(l => l.Id == asset.LocationId).FirstOrDefaultAsync();

        //        var initiatedDate = asset.DateCreated;

        //        var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
        //            .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
        //            .FirstOrDefaultAsync();

        //        var template = notificationMessageTemplate?.MessageTemplate;

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            var itemActionName = "Asset Service";

        //            if (notification.NotificationType == NotificationType.Approval)
        //            {
        //                itemActionName += " Approval";
        //            }

        //            if (template.Contains(@"{ItemActionName}"))
        //            {
        //                template = template.Replace(@"{ItemActionName}", itemActionName);
        //            }
        //        }

        //        template = await GenerateAssetDetails(notification, template);

        //        template = await GenerateAssetServiceDetails(notification, template);

        //        template = await GenerateInitiatorDetails(initiator, initiatorLocation, initiatedDate, template);

        //        var linkText = "";

        //        var link = "";
        //        if (notification.NotificationType == NotificationType.Approval)
        //        {
        //            link = assetUrls.ViewAssetServiceApproval;
        //        }
        //        else
        //        {
        //            link = assetUrls.ViewAssetService;
        //        }
        //        template = await GenerateViewLink(notification, template, link, linkText);

        //        generated = await GenerateRecipientDetailsAndSave(notification, template);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //public async Task<bool> AssetCheckIn(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var asset = notification.MainEntity as Asset;

        //        var assetCheckin = notification.SubEntity as AssetCheckIn;

        //        var initiator = await userManager.FindByNameAsync(assetCheckin?.CreatedBy);

        //        var initiatorLocation = await applicationDbContext.Locations
        //            .Where(l => l.Id == asset.LocationId).FirstOrDefaultAsync();

        //        var initiatedDate = assetCheckin.DateCreated;

        //        var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
        //            .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
        //            .FirstOrDefaultAsync();

        //        var template = notificationMessageTemplate?.MessageTemplate;

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            var itemActionName = "Asset Checkin";

        //            if (notification.NotificationType == NotificationType.Approval)
        //            {
        //                itemActionName += " Approval";
        //            }

        //            if (template.Contains(@"{ItemActionName}"))
        //            {
        //                template = template.Replace(@"{ItemActionName}", itemActionName);
        //            }
        //        }

        //        template = await GenerateAssetDetails(notification, template);

        //        template = await GenerateAssetCheckInDetails(notification, template);

        //        template = await GenerateInitiatorDetails(initiator, initiatorLocation, initiatedDate, template);

        //        var linkText = "";

        //        var link = "";
        //        if (notification.NotificationType == NotificationType.Approval)
        //        {
        //            link = assetUrls.ViewAssetCheckInApproval;
        //        }
        //        else
        //        {
        //            link = assetUrls.ViewAssetCheckIn;
        //        }
        //        template = await GenerateViewLink(notification, template, link, linkText);

        //        generated = await GenerateRecipientDetailsAndSave(notification, template);
        //        bool initiatorDetailsGenerated = await GenerateInitiatorDetailsAndSave(notification, template, initiator);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //public async Task<bool> AssetCheckOutDisbursement(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var asset = notification.MainEntity as Asset;

        //        var assetCheckout = notification.SubEntity as AssetCheckOut;

        //        var initiator = await userManager.FindByNameAsync(assetCheckout?.CreatedBy);

        //        var initiatorLocation = await applicationDbContext.Locations
        //            .Where(l => l.Id == asset.LocationId).FirstOrDefaultAsync();

        //        var initiatedDate = asset.DateCreated;

        //        var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
        //            .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
        //            .FirstOrDefaultAsync();

        //        var template = notificationMessageTemplate?.MessageTemplate;

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            var itemActionName = "Asset Checkout";

        //            if (notification.NotificationType == NotificationType.Disbursement)
        //            {
        //                itemActionName += " Disbursement";
        //            }

        //            if (template.Contains(@"{ItemActionName}"))
        //            {
        //                template = template.Replace(@"{ItemActionName}", itemActionName);
        //            }
        //        }

        //        template = await GenerateAssetDetails(notification, template);

        //        template = await GenerateAssetCheckOutDetails(notification, template);

        //        template = await GenerateInitiatorDetails(initiator, initiatorLocation, initiatedDate, template);
        //        template = await GenerateAssetDisbursementDetails(notification, template);

        //        var linkText = "";

        //        template = await GenerateViewLink(notification, template, assetUrls.ViewAssetCheckOutDisbursement, linkText);
        //        generated = await GenerateRecipientDetailsAndSave(notification, template);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}

        //public async Task<bool> AssetReservationDisbursement(NotificationMessage notification)
        //{
        //    bool generated = false;
        //    try
        //    {
        //        var asset = notification.MainEntity as Asset;
        //        var assetReservation = notification.SubEntity as ReservedAsset;

        //        var initiator = await userManager.FindByNameAsync(assetReservation?.CreatedBy);

        //        var initiatorLocation = await applicationDbContext.Locations
        //            .Where(l => l.Id == asset.LocationId).FirstOrDefaultAsync();

        //        var initiatedDate = asset.DateCreated;

        //        var notificationMessageTemplate = await applicationDbContext.NotificationMessageTemplates
        //            .Where(n => n.NotificationActionType == notification.NotificationActionType && n.IsDeleted == false)
        //            .FirstOrDefaultAsync();

        //        var template = notificationMessageTemplate?.MessageTemplate;

        //        if (!string.IsNullOrWhiteSpace(template))
        //        {
        //            var itemActionName = "Asset Reservation";

        //            if (notification.NotificationType == NotificationType.Disbursement)
        //            {
        //                itemActionName += " Disbursement";
        //            }

        //            if (template.Contains(@"{ItemActionName}"))
        //            {
        //                template = template.Replace(@"{ItemActionName}", itemActionName);
        //            }
        //        }

        //        template = await GenerateAssetDetails(notification, template);

        //        template = await GenerateAssetReservationDetails(notification, template);

        //        template = await GenerateInitiatorDetails(initiator, initiatorLocation, initiatedDate, template);
        //        template = await GenerateAssetDisbursementDetails(notification, template);

        //        var linkText = "";
        //        template = await GenerateViewLink(notification, template, assetUrls.ViewAssetReservationDisbursement, linkText);
        //        generated = await GenerateRecipientDetailsAndSave(notification, template);
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return generated;
        //}
    }
}