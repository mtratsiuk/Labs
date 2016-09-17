import 'babel-polyfill'
import createRenderer from '../canvas/renderer'
import createTool from '../tools/tool'
import { fromRGBA } from '../utils/color'
import './style.css'

export default function init (app) {
  const appContainer = document.createElement('div')
  appContainer.setAttribute('class', 'mg')
  const canvas = document.createElement('canvas')
  canvas.setAttribute('class', 'mg-canvas')
  canvas.width = app.clientWidth
  canvas.height = app.clientHeight - 100

  const renderer = createRenderer(canvas)
  let currentTool

  const tools = [
    {
      title: 'Line',
      tool: createTool(renderer, { shape: 'line', color: fromRGBA(0, 0, 0, 255) })
    }
  ]

  const actions = [
    {
      title: 'Undo',
      action: renderer.undo
    },
    {
      title: 'Redo',
      action: renderer.redo
    }
  ]

  const toolBtns = tools.map(x => {
    const btn = document.createElement('button')
    btn.setAttribute('class', 'mg-tools__btn')
    btn.innerText = x.title
    btn.onclick = () => {
      const prev = document.querySelectorAll('.mg-tools__btn_active')[0]
      if (prev) prev.classList.toggle('mg-tools__btn_active')
      btn.classList.add('mg-tools__btn_active')
      if (currentTool) currentTool.off()
      currentTool = x.tool
      currentTool.on()
    }
    return btn
  })

  const toolBtnsContainer = document.createElement('div')
  toolBtnsContainer.setAttribute('class', 'mg-tools')
  toolBtns.forEach(x => toolBtnsContainer.appendChild(x))

  const actionBtns = actions.map(x => {
    const btn = document.createElement('button')
    btn.setAttribute('class', 'mg-actions__btn')
    btn.innerText = x.title
    btn.onclick = x.action
    return btn
  })

  const actionBtnsContainer = document.createElement('div')
  actionBtnsContainer.setAttribute('class', 'mg-actions')
  actionBtns.forEach(x => actionBtnsContainer.appendChild(x))

  appContainer.appendChild(toolBtnsContainer)
  appContainer.appendChild(canvas)
  appContainer.appendChild(actionBtnsContainer)
  app.appendChild(appContainer)
}
