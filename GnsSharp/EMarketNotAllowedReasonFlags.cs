// SPDX-FileCopyrightText: Copyright 2025 Guyeon Yu <copyrat90@gmail.com>
// SPDX-License-Identifier: MIT

namespace GnsSharp;

using System;

[Flags]
public enum EMarketNotAllowedReasonFlags : int
{
    None = 0,

    /// <summary>
    /// A back-end call failed or something that might work again on retry
    /// </summary>
    TemporaryFailure = 1 << 0,

    /// <summary>
    /// Disabled account
    /// </summary>
    AccountDisabled = 1 << 1,

    /// <summary>
    /// Locked account
    /// </summary>
    AccountLockedDown = 1 << 2,

    /// <summary>
    /// Limited account no purchases
    /// </summary>
    AccountLimited = 1 << 3,

    /// <summary>
    /// The account is banned from trading items
    /// </summary>
    TradeBanned = 1 << 4,

    /// <summary>
    /// Wallet funds aren't tradable because the user has had no purchase
    /// activity in the last year or has had no purchases prior to last month
    /// </summary>
    AccountNotTrusted = 1 << 5,

    /// <summary>
    /// The user doesn't have Steam Guard enabled
    /// </summary>
    SteamGuardNotEnabled = 1 << 6,

    /// <summary>
    /// The user has Steam Guard, but it hasn't been enabled for the required
    /// number of days
    /// </summary>
    SteamGuardOnlyRecentlyEnabled = 1 << 7,

    /// <summary>
    /// The user has recently forgotten their password and reset it
    /// </summary>
    RecentPasswordReset = 1 << 8,

    /// <summary>
    /// The user has recently funded his or her wallet with a new payment method
    /// </summary>
    NewPaymentMethod = 1 << 9,

    /// <summary>
    /// An invalid cookie was sent by the user
    /// </summary>
    InvalidCookie = 1 << 10,

    /// <summary>
    /// The user has Steam Guard, but is using a new computer or web browser
    /// </summary>
    UsingNewDevice = 1 << 11,

    /// <summary>
    /// The user has recently refunded a store purchase by his or herself
    /// </summary>
    RecentSelfRefund = 1 << 12,

    /// <summary>
    /// The user has recently funded his or her wallet with a new payment method that cannot be verified
    /// </summary>
    NewPaymentMethodCannotBeVerified = 1 << 13,

    /// <summary>
    /// Not only is the account not trusted, but they have no recent purchases at all
    /// </summary>
    NoRecentPurchases = 1 << 14,

    /// <summary>
    /// User accepted a wallet gift that was recently purchased
    /// </summary>
    AcceptedWalletGift = 1 << 15,
}
