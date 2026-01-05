-- ============================
-- ACCOUNTS
-- ============================
CREATE TABLE accounts (
    id NVARCHAR(50) NOT NULL PRIMARY KEY,
    customer_id NVARCHAR(50) NOT NULL,
    currency NVARCHAR(3) NOT NULL,
    available_balance DECIMAL(18,2) NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    updated_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE INDEX idx_accounts_customer
ON accounts(customer_id);


-- ============================
-- BENEFICIARIES
-- ============================
CREATE TABLE beneficiaries (
    id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    customer_id NVARCHAR(50) NOT NULL,
    alias NVARCHAR(30) NOT NULL,
    bank_code NVARCHAR(10) NOT NULL,
    account_number NVARCHAR(20) NOT NULL,
    currency NVARCHAR(3) NOT NULL,
    is_active BIT NOT NULL DEFAULT 1,
    created_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    updated_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE INDEX idx_beneficiaries_customer
ON beneficiaries(customer_id);


-- ============================
-- TRANSFERS
-- ============================
CREATE TABLE transfers (
    id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    customer_id NVARCHAR(50) NOT NULL,
    from_account_id NVARCHAR(50) NOT NULL,
    beneficiary_id UNIQUEIDENTIFIER NOT NULL,
    amount DECIMAL(18,2) NOT NULL,
    currency NVARCHAR(3) NOT NULL,
    description NVARCHAR(140) NULL,
    status NVARCHAR(20) NOT NULL,
    client_request_id NVARCHAR(100) NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    CONSTRAINT fk_transfers_account
        FOREIGN KEY (from_account_id) REFERENCES accounts(id),

    CONSTRAINT fk_transfers_beneficiary
        FOREIGN KEY (beneficiary_id) REFERENCES beneficiaries(id)
);

-- Idempotency support
CREATE UNIQUE INDEX ux_transfers_customer_request
ON transfers(customer_id, client_request_id);

CREATE INDEX idx_transfers_account_date
ON transfers(from_account_id, created_at);
