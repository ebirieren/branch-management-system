import type { Transaction  } from "../types/transaction";

const TRANSACTIONS_URL = '/api/transactions'

export async function getTransactions(): Promise<Transaction[]> {
    const response = await fetch(TRANSACTIONS_URL)

    if (!response.ok){
        throw new Error(`Request failed with status ${response.status}. Is the backend running?`)
    }

    return response.json() as Promise<Transaction[]>
}