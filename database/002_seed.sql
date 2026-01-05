-- Demo customer
DECLARE @customerId NVARCHAR(50) = 'cust_demo_01';

-- Accounts
INSERT INTO accounts (id, customer_id, currency, available_balance)
VALUES
('acc_demo_bob_01', @customerId, 'BOB', 50000),
('acc_demo_usd_01', @customerId, 'USD', 10000);

-- Beneficiaries
INSERT INTO beneficiaries (
    id, customer_id, alias, bank_code, account_number, currency, is_active
)
VALUES
(NEWID(), @customerId, 'Juan Pérez', 'BISA', '1234567890', 'BOB', 1),
(NEWID(), @customerId, 'María López', 'BNB', '9876543210', 'USD', 1);
