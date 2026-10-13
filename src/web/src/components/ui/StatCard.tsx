import type { ReactNode } from 'react'

type StatCardProps = {
  label: string
  value: string
  helper: string
  icon: ReactNode
  tone: 'blue' | 'violet' | 'orange' | 'green'
}

export function StatCard({ label, value, helper, icon, tone }: StatCardProps) {
  return (
    <article className={`stat-card tone-${tone}`}>
      <div className="stat-top">
        <span className="stat-label">{label}</span>
        <span className="stat-icon">{icon}</span>
      </div>
      <strong>{value}</strong>
      <span className="stat-helper">{helper}</span>
    </article>
  )
}
