namespace BikeRental.Registration.Features

open BikeRental.Registration

[<RequireQualifiedAccess>]
module QueryInitials =
    let query (getUserEvents: Username -> Async<UserEvent list>) username =
        async {
            let! userEvents = getUserEvents username
            let userState = User.projectState userEvents

            return
                match userState with
                | Active user ->
                    let firstNameFirstLetter = user.FirstName.Substring(0, 1)
                    let lastNameFirstLetter = user.LastName.Substring(0, 1)
                    Some $"{firstNameFirstLetter}{lastNameFirstLetter}"
                | _ -> None
        }
