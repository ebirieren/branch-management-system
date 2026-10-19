import type { ReactNode } from 'react'
import { Sidebar  } from '../components/navigation/Sidebar'
import { Topbar } from '../components/navigation/Topbar'

type DashboardLayoutProps = {
    branchCount: number
    productCount: number
    search: string
    onSearchChange: (value: string) => void
    children: ReactNode
}

export function DashboardLayout({branchCount, productCount, search, onSearchChange, children}: DashboardLayoutProps) {
    return (
        <div className="app-shell">
            <Sidebar branchCount={branchCount} productCount={productCount} />

            <main className='main-content' id="top">
                <Topbar search={search} onSearchChange={onSearchChange}/>

                {children}
            </main>
        </div>
    )
}