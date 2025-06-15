-- Script de criação das tabelas para o sistema de mármore e granito

-- Tabela de Usuários
CREATE TABLE IF NOT EXISTS usuarios (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL,
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ativo BOOLEAN DEFAULT true
);

-- Tabela de Blocos
CREATE TABLE IF NOT EXISTS blocos (
    id SERIAL PRIMARY KEY,
    codigo VARCHAR(50) NOT NULL UNIQUE,
    pedreira_origem VARCHAR(100) NOT NULL,
    metragem_m3 DECIMAL(10,3) NOT NULL,
    tipo_material VARCHAR(100) NOT NULL,
    valor_compra DECIMAL(10,2) NOT NULL,
    nota_fiscal_entrada VARCHAR(50) NOT NULL,
    data_cadastro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    disponivel BOOLEAN DEFAULT true
);

-- Tabela de Chapas
CREATE TABLE IF NOT EXISTS chapas (
    id SERIAL PRIMARY KEY,
    bloco_id INTEGER REFERENCES blocos(id),
    tipo_material VARCHAR(100) NOT NULL,
    comprimento DECIMAL(10,2) NOT NULL,
    largura DECIMAL(10,2) NOT NULL,
    espessura DECIMAL(10,2) NOT NULL,
    valor_unitario DECIMAL(10,2) NOT NULL,
    data_cadastro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    disponivel BOOLEAN DEFAULT true
);

-- Tabela de Processo de Serragem
CREATE TABLE IF NOT EXISTS processo_serragem (
    id SERIAL PRIMARY KEY,
    bloco_id INTEGER REFERENCES blocos(id),
    data_inicio TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    data_conclusao TIMESTAMP,
    quantidade_chapas INTEGER NOT NULL,
    observacoes TEXT,
    status VARCHAR(50) DEFAULT 'Em Andamento'
); 