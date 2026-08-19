# Functional Specification - Vault
### Personal & Business Banking Platform

| | |
|---|---|
| **Project name** | Vault |
| **Document type** | Functional specification |
| **Version** | 1.0 |
| **Status** | Draft |
| **Date** | August 2026 |

---

## 1. Purpose

This document describes what the Vault platform does, from the point of view of its users and the business. Vault lets customers hold money in accounts, move money between accounts, spend with cards, and see a complete and accurate history of everything that happens to their money.

Because money is involved, the platform's central promise is **correctness**: every movement of money must be complete or not happen at all, an account's balance must always reflect reality, and no operation may ever create or destroy money by accident. The system must guarantee this even when many operations happen at the same time.

---

## 2. Scope

### 2.1 In scope
- User accounts and authentication
- Customer profiles and identity verification (KYC)
- Bank accounts, balances, and transaction history
- Money transfers between accounts (internal and to external beneficiaries)
- Scheduled and recurring payments (standing orders)
- Payment cards, spending limits, and card controls
- Account statements and reporting
- Customer notifications

### 2.2 Out of scope (initial release)
- Loans, credit facilities, and mortgages
- Investment, trading, and savings products with interest calculation
- Foreign-exchange conversion between currencies
- Physical branch operations and cash handling
- Cheque processing
- Third-party open-banking integrations
- Fraud-detection scoring engine (basic limits only)

---

## 3. Actors and roles

| Actor | Description |
|---|---|
| **Customer** | Holds accounts, moves money, spends with cards, views their own history and statements. |
| **Bank staff** | Assists customers, opens and freezes accounts, reviews identity verification. |
| **Administrator** | Manages users, roles, and platform oversight. |

---

## 4. Functional requirements

### 4.1 Identity

- **Registration** - a visitor can create an account.
- **Login** - a registered user can authenticate to access their account.
- **Logout** - a user can end their session.
- **Manage roles / permissions** - the system assigns permissions that determine what a user may do (customer, bank staff, administrator).
- **Email confirmation** - a new user must verify ownership of their email address.
- **Reset password** - a user who has forgotten their password can securely set a new one.

### 4.2 Onboarding

- **Create a customer profile** - capture the person the bank is dealing with (name, date of birth, contact details, address).
- **Submit identity documents** - a customer provides the documents needed to verify who they are.
- **Verify identity (KYC)** - bank staff review submitted documents and mark the customer as verified.
- **Update contact details** - a customer keeps their address and contact methods current.
- **A customer profile is distinct from a login** - one person has a single login, but their profile holds the banking-relevant details about them.

### 4.3 Ledger

- **Open an account** - a verified customer is given a new account with a unique account number and a starting balance of zero.
- **View account details** - see an account's number, currency, current balance, and status.
- **View transaction history** - see every movement of money in and out of the account, in order.
- **Freeze / unfreeze an account** - bank staff can suspend activity on an account and later restore it.
- **Close an account** - an account with a zero balance can be permanently closed.
- **Track the running balance** - the balance always reflects the sum of every transaction on the account.

### 4.4 Payments

- **Transfer money between own accounts** - move funds from one of a customer's accounts to another.
- **Transfer money to a beneficiary** - send funds to another account, saved as a beneficiary for reuse.
- **Manage beneficiaries** - add, view, and remove saved payees.
- **Guarantee atomic transfers** - a transfer either fully completes (source debited *and* destination credited) or does not happen at all; a partial transfer is never left behind.
- **Reverse a failed transfer** - if a transfer cannot be completed after the source has been debited, the funds are automatically returned to the source.
- **Set up a standing order** - schedule a recurring transfer of a fixed amount on a fixed schedule.
- **Cancel a standing order** - stop future recurring transfers.
- **View transfer status** - see whether a transfer is pending, completed, or failed.

### 4.5 Issuing

- **Issue a card** - link a payment card to an account.
- **Set spending limits** - cap how much can be spent on a card per transaction or per period.
- **Freeze / unfreeze a card** - temporarily block a card and later restore it.
- **Record a card transaction** - capture a purchase made with the card against its account.
- **Decline over-limit spending** - a card transaction that would breach a limit or the account's available funds is refused.
- **Cancel a card** - permanently deactivate a card.

### 4.6 Reporting

- **Generate a statement** - produce a summary of an account's activity over a chosen period.
- **View past statements** - retrieve previously generated statements.
- **Show opening and closing balances** - each statement states the balance at the start and end of its period.

### 4.7 Notifications

- **Notify on money received** - inform a customer when their account is credited.
- **Notify on money sent** - confirm to a customer when a transfer leaves their account.
- **Notify on low balance** - alert a customer when a balance falls below a chosen threshold.
- **Notify on card activity** - inform a customer of card transactions and declines.
- **Notify on account changes** - inform a customer when an account is frozen, unfrozen, or closed.
- **Send statement-ready alerts** - tell a customer when a new statement is available.

---

## 5. Business rules : Domain constraints

- An account's balance must always equal the sum of all its transactions; the two can never disagree.
- An account may not be debited below its permitted limit (zero, or an agreed overdraft limit where one exists). Attempts to spend beyond this are refused.
- A transfer is **atomic**: both the debit from the source and the credit to the destination succeed together, or neither is applied.
- If a transfer's debit succeeds but its credit cannot be completed, the debit is automatically reversed so no money is lost.
- Simultaneous operations on the same account are handled so the balance is never left incorrect, and the account is never taken below its permitted limit by a race between two operations.
- Money is always tied to a currency; amounts in different currencies are never combined.
- A frozen account rejects all money movements until it is unfrozen.
- An account can only be closed when its balance is zero.
- A card transaction is refused if it would breach the card's spending limit or the account's available funds.
- A frozen or cancelled card rejects all transactions.
- Only verified customers may open accounts; only bank staff may freeze accounts or verify identities; only administrators may manage users and roles.
- Every movement of money is permanently recorded and can never be edited or deleted - corrections are made by adding a new offsetting movement, never by altering history.

---

## 6. Key user journeys

### 6.1 Opening an account (customer)
1. A verified customer requests a new account and chooses its currency.
2. The system creates the account with a unique number and a zero balance.
3. The account is immediately ready to receive and send money.

### 6.2 Transferring money between accounts (customer)
1. The customer selects a source account, a destination, and an amount.
2. The system checks the source has sufficient available funds.
3. The source is debited and the destination is credited as a single, indivisible operation.
4. Both accounts' balances and histories are updated, and the customer is notified.

### 6.3 A transfer that cannot complete
1. The customer starts a transfer and the source account is debited.
2. The destination cannot accept the credit (for example, it has been closed).
3. The system automatically reverses the debit, returning the funds to the source.
4. The transfer is marked failed and the customer is informed; no money is lost.

### 6.4 Concurrent spending on the last available funds
1. Two operations try to draw on the same account's remaining funds at the same time.
2. The system applies them in a way that keeps the balance correct.
3. Only the operations the balance can support succeed; the rest are refused for insufficient funds.
4. The account is never taken below its permitted limit.

### 6.5 Setting up a recurring payment (customer)
1. The customer defines an amount, a destination, and a schedule.
2. On each scheduled date, the system performs the transfer automatically.
3. Each occurrence follows the same atomic transfer rules as a manual transfer.
4. The customer can cancel the standing order at any time, stopping future occurrences.

### 6.6 Spending with a card
1. The customer makes a purchase with their card.
2. The system checks the card's limits and the account's available funds.
3. If within limits, the account is debited and the transaction is recorded.
4. If it would breach a limit or the available funds, the transaction is declined and the customer is notified.

### 6.7 Generating a statement (customer)
1. The customer chooses an account and a period.
2. The system produces a statement listing every movement in that period, with opening and closing balances.
3. The statement is stored and can be viewed again later.

---

## 7. Acceptance criteria : 

The platform is functionally complete when:

- A customer can register, confirm their email, log in, and log out.
- A customer profile can be created, identity documents submitted, and the customer marked verified by staff.
- A verified customer can open an account, view its details and full transaction history, and close it when its balance is zero.
- A customer can transfer money between accounts, and every transfer is atomic - never leaving a debit without its matching credit.
- A transfer whose credit cannot complete is automatically reversed, with no money created or lost.
- Simultaneous operations on one account never leave the balance incorrect and never breach its permitted limit.
- Amounts in different currencies are never combined.
- A customer can issue a card, set limits, freeze it, and have over-limit or insufficient-funds transactions declined.
- A customer can set up and cancel a recurring payment, and scheduled occurrences run automatically under the same rules as manual transfers.
- A customer can generate and re-view statements showing opening and closing balances for a period.
- All defined notifications are delivered for their corresponding events (money received, money sent, low balance, card activity, account changes, statement ready).

---
