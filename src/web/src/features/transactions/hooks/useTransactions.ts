import { useEffect, useState } from "react";
import { getTransactions } from "../api/transactions";
import type { Transaction } from "../types/transaction";

export function useTransactions(){
    const [transactions, setTransactions] = useState<Transaction[]>([])
    const [error, setError] = useState<string | null>(null)
    const [isLoading, setIsLoading] = useState(true)

    async function refreshTransactions(): Promise<void> {
        try {
            setIsLoading(true)
            setError(null)

            const loadedTransactions = await getTransactions()

            setTransactions(loadedTransactions)
        } catch (requestError) {
            setError(requestError instanceof Error ? requestError.message : "Something went wrong.")
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        let ignoreResult = false

        void getTransactions()
        .then((data) => {
            if (!ignoreResult) setTransactions(data)
        })
        .catch((requestError: unknown) => {
            if(!ignoreResult) {
                setError(requestError instanceof Error ? requestError.message : "Something went wrong.")
            }
        })
        .finally(() => {
            if (!ignoreResult) setIsLoading(false)
        })
        
        return () => {ignoreResult = true}
    }, [])

    return { transactions, isLoading, error, refreshTransactions}
}