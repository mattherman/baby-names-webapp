SELECT
    user.Id,
    user.EmailAddress,
    user.FullName,
    user.PictureUri
FROM Users as user
WHERE
    user.EmailAddress = @Email;
