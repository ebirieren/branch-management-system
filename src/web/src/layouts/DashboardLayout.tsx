import type { ReactNode } from 'react'
import { Sidebar  } from '../components/navigation/Sidebar'
import { Topbar } from '../components/navigation/Topbar'

type DashboardLayoutProps = {
    branchCount: number
    search: string
    onSearchChange: (value: string) => void
    children: ReactNode
}

export function DashboardLayout({branchCount, search, onSearchChange, children}: DashboardLayoutProps) {
    return (
        <div className="app-shell">
            <Sidebar branchCount={branchCount} />

            <main className='main-content' id="top">
                <Topbar search={search} onSearchChange={onSearchChange}/>

                {children}
            </main>
        </div>
    )
}